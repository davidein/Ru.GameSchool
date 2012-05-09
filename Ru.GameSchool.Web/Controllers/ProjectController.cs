using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Web.Classes.Helper;

namespace Ru.GameSchool.Web.Controllers
{
    public class ProjectController : BaseController
    {
        [Authorize(Roles = "Teacher, Student")]
        [HttpGet]
        public ActionResult Get(int? id)
        {
            ViewBag.AllowedFileExtensions = GetAllowedFileExtensions();
            if (id.HasValue && id.Value > 0)
            {
                var levelProject = LevelService.GetLevelProject(id.Value);

                ViewBag.Title = levelProject.Name;

                return View(levelProject);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult TeacherGet(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var projectResults = LevelService.GetlevelProjectResultsByLevelProjectId(id.Value);
                return View(projectResults);
            }
            return RedirectToAction("Index", "Project");

        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult GradeProject(LevelProjectResult result)
        {
            LevelService.UpdateLevelProjectResult(result);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult GradeProject(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var project = LevelService.GetlevelProjectResultsByLevelProjectResultId(id.Value);
                return View(project);
            }
            return View();
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult ReturnProject(LevelProject levelProject)
        {
            var user = MembershipHelper.GetUser().UserInfoId;

            levelProject.LevelProjectResults.Add(CreateLevelProjectResult(levelProject, user));
            LevelService.UpdateLevelProjectFromResult(levelProject);
            ViewBag.CourseName = levelProject.Level.Course.Name;
            ViewBag.CourseId = levelProject.Level.CourseId;
            ViewBag.Title = "Verkefni";

            return RedirectToAction("Get");
        }

        private LevelProjectResult CreateLevelProjectResult(LevelProject levelProject, int id)
        {
            var result = new LevelProjectResult
                             {
                                 CreateDateTime = DateTime.Now,
                                 LevelProjectId = levelProject.LevelProjectId,
                                 UserInfoId = id,
                                 UserFeedback = levelProject.UserFeedback,
                                 ContentID = levelProject.ContentID,
                                 GradeDate = DateTime.Now // fæ annars datetime exception
                             };
            return result;
        }

        [Authorize(Roles = "Teacher, Student")]
        [HttpGet]
        public ActionResult Index(int? id)
        {
            var userInfoId = MembershipHelper.GetUser().UserInfoId;
            IEnumerable<Course> levelProjects = null;
            ViewBag.Title = "Verkefnin mín";
            ViewBag.UserInfoId = userInfoId;
            // Sækja spes course
            if (id.HasValue && id.Value > 0)
            {
                levelProjects = CourseService.GetCoursesByUserInfoIdAndCourseId(userInfoId, id.Value);
            }
            else // Sækja alla courses sem þessi nemandi er í
            {
                levelProjects = CourseService.GetCoursesByUserInfoId(userInfoId);
            }
            
            return levelProjects == null ? View() : View(levelProjects.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.Title = "Búa til nýtt verkefni";
                ViewBag.GradePercentageValue = GetPercentageValue();
                ViewBag.LevelId = new SelectList(LevelService.GetLevelsByCourseId(id.Value), "LevelId", "Name");
            }

            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? id, LevelProject levelproject)
        {
            ViewBag.Title = "Búa til nýtt verkefni";
            if (ModelState.IsValid)
            {
                if (id.HasValue && id.Value > 0)
                {
                    ViewBag.LevelId = new SelectList(LevelService.GetLevelsByCourseId(id.Value), "LevelId", "Name", levelproject.LevelId);
                    LevelService.AddLevelProjectToCourseAndLevel(levelproject, id.Value);
                }
                return RedirectToAction("Index");
            }
            ViewBag.GradePercentageValue = GetPercentageValue();
            return View(levelproject);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            ViewBag.GradePercentageValue = GetPercentageValue();
            if (id.HasValue && id.Value > 0)
            {
                var levelProject = LevelService.GetLevelProject(id.Value);
                ViewBag.LevelId = new SelectList(LevelService.GetLevels(), "LevelId", "Name", levelProject.LevelId);
                return View(levelProject);
            }
            return RedirectToAction("NotFound", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(LevelProject levelProject)
        {
            ViewBag.GradePercentageValue = GetPercentageValue();
            if (ModelState.IsValid)
            {
                LevelService.UpdateLevelProject(levelProject);
                ViewBag.LevelId = new SelectList(LevelService.GetLevels(), "LevelId", "Name", levelProject.LevelId);
            }
            return View(levelProject);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var levelProject = LevelService.GetLevelProject(id.Value);
                return View(levelProject);
            }
            return RedirectToAction("NotFound", "Home");
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                LevelService.DeleteLevelProject(id.Value);
            }
            return RedirectToAction("Index");
        }

        public IEnumerable<SelectListItem> GetPercentageValue()
        {
            for (int j = 1; j <= 100; j++)
            {
                yield return new SelectListItem
                {
                    Text = j.ToString() + " %",
                    Value = j.ToString()
                };
            }
        }

        private List<string> GetAllowedFileExtensions()
        {
            return new string[]
                       {
                           ".doc ",
                           ".pdf ",
                           ".zip ",
                           ".rar "
                       }.ToList();
        }



    }
}