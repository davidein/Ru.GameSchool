using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        [Authorize(Roles = "Student")]
        [HttpGet]
        public ActionResult Get(int? id)
        {
            var userInfoId = ViewBag.UserInfoId = MembershipHelper.GetUser().UserInfoId;

            ViewBag.AllowedFileExtensions = GetAllowedFileExtensions();
            if (id.HasValue && id.Value > 0)
            {
                var levelProject = LevelService.GetLevelProject(id.Value);
                var course = CourseService.GetCourse(levelProject.Level.CourseId);
                var courseId = ViewBag.CourseId = course.CourseId;
                ViewBag.Title = levelProject.Name;
                var allowedUserLevel = ViewBag.AllowedLevelId = CourseService.GetCurrentUserLevel(userInfoId, courseId);

                if (levelProject.LevelId > allowedUserLevel)
                {
                    return RedirectToAction("Index", new { id = courseId });
                }
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
                var projectResults = LevelService.GetlevelProjectResultsByLevelProjectId(id.Value).OrderByDescending(x => x.GradeDate);
                return View(projectResults);
            }
            return RedirectToAction("Index", "Project");

        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult GradeProject(LevelProjectResult result)
        {
            if (result != null)
            {
                LevelService.UpdateLevelProjectResult(result);
                ViewBag.SuccessMessage = "Verkefni hefur verið uppfært";
                return View(result);
            }
            ViewBag.ErrorMessage = "Gat ekki uppfært kennslugagn! Lagfærðu villur og reyndur aftur.";
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult GradeProject(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var projectResults = LevelService.GetlevelProjectResultsByLevelProjectResultId(id.Value);
                return View(projectResults);
            }
            return RedirectToAction("Index", "Project");
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult ReturnProject(LevelProject levelProject)
        {
            var user = MembershipHelper.GetUser().UserInfoId;

            levelProject.LevelProjectResults.Add(CreateLevelProjectResult(levelProject, user));
            LevelService.UpdateLevelProjectFromResult(levelProject, user);

            return RedirectToAction("Get", new { id = levelProject.LevelProjectId });
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
                ViewBag.AllowedLevelId = CourseService.GetCurrentUserLevel(userInfoId, id.Value);
                var course = CourseService.GetCourse(id.Value);
                ViewBag.CourseId = course.CourseId;
                levelProjects =
                    CourseService.GetCoursesByUserInfoIdAndCourseId(userInfoId, id.Value).OrderByDescending(
                        x => x.CreateDateTime);

                ViewBag.CourseId = id.Value;
            }
            else // Sækja alla courses sem þessi nemandi er í
            {
                //levelProjects = CourseService.GetCoursesByUserInfoId(userInfoId).OrderByDescending(x => x.CreateDateTime);
                return RedirectToAction("NotFound", "Home");
            }
            return View(levelProjects.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? id)
        {
            ViewBag.Title = "Búa til verkefni";
            ViewBag.GradePercentageValue = GetPercentageValue();

            if (id.HasValue)
            {
                ViewBag.LevelCount = GetLevelCounts(id.Value);
                ViewBag.CourseId = id.Value;
                ViewBag.CourseName = CourseService.GetCourse(id.Value).Name;
                ViewBag.LevelId = new SelectList(LevelService.GetLevelsByCourseId(id.Value), "LevelId", "Name");
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? id, LevelProject levelproject)
        {
            ViewBag.Title = "Búa til nýtt verkefni";

            if (id.HasValue && id.Value > 0)
            {
                var course = CourseService.GetCourse(id.Value);
                ViewBag.CourseId = course.CourseId;

                if (levelproject.File != null)
                {
                    foreach (var file in levelproject.File)
                    {
                        Guid contentId = Guid.NewGuid();
                        var path = Server.MapPath("~/Upload") + contentId.ToString();
                        ViewBag.ContentId = contentId;
                        file.SaveAs(path);
                        levelproject.ContentID = contentId.ToString();
                    }
                }
                ViewBag.LevelCount = GetLevelCounts(id.Value);
                ViewBag.CourseId = id.Value;
                ViewBag.GradePercentageValue = GetPercentageValue();

                LevelService.AddLevelProjectToCourseAndLevel(levelproject, id.Value);
                return RedirectToAction("Index", "Project", new { id = id.Value });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            ViewBag.GradePercentageValue = GetPercentageValue();
            if (id.HasValue && id.Value > 0)
            {
                var course = CourseService.GetCourse(id.Value);
                ViewBag.CourseId = course.CourseId;
                var levelProject = LevelService.GetLevelProject(id.Value);
                ViewBag.LevelId = new SelectList(LevelService.GetLevels(), "LevelId", "Name", levelProject.LevelId);
                return View(levelProject);
            }
            return RedirectToAction("NotFound", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(LevelProject levelProject, int? id)
        {
            ViewBag.GradePercentageValue = GetPercentageValue();
            var material = LevelService.GetLevelProject(levelProject.LevelProjectId);
            var courseId = material.Level.CourseId;
            if (ModelState.IsValid)
            {
                if (TryUpdateModel(levelProject))
                {
                    if (levelProject.File != null)
                    {
                        foreach (var file in levelProject.File)
                        {
                            Guid contentId = Guid.NewGuid();
                            var path = Server.MapPath("~/Upload") + contentId.ToString();
                            ViewBag.ContentId = contentId;
                            file.SaveAs(path);
                            levelProject.ContentID = contentId.ToString();
                        }
                    }

                    ViewBag.CourseName = CourseService.GetCourse(courseId).Name;
                    ViewBag.Courseid = CourseService.GetCourse(courseId).CourseId;
                    ViewBag.LevelCount = GetLevelCounts(courseId);
                    ViewBag.SuccessMessage = "Verkefni hefur verið uppfært";
                    ViewBag.LevelId = new SelectList(LevelService.GetLevels(), "LevelId", "Name", levelProject.LevelId);

                    LevelService.UpdateLevelProject(levelProject);
                    return View(levelProject);
                }
                else
                {
                    ViewBag.ErrorMessage = "Gat ekki uppfært kennslugagn! Lagfærðu villur og reyndur aftur.";
                    if (id.HasValue)
                    {
                        return View(LevelService.GetLevelProject(id.Value));
                    }
                }
            }
            ViewBag.LevelCount = GetLevelCounts(levelProject.Level.CourseId);
            ViewBag.LevelProjectId = levelProject.LevelProjectId;
            ViewBag.ContentTypes = LevelService.GetContentTypes();
            return View();
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
                try
                {
                    LevelService.DeleteLevelProject(id.Value);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Ekki er hægt að eyða verkefni, yfirleitt er það útaf því að nemandi er þegar búinn að fá einkunn fyrir þetta verkefni";
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    var levelProject = LevelService.GetLevelProject(id.Value);
                    return View(levelProject);
                }
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

        public IEnumerable<SelectListItem> GetLevelCounts(int courseId)
        {
            for (int j = 0; j <= LevelService.GetLevels(courseId).Count(); j++)
            {
                var elementAtOrDefault = LevelService.GetLevels(courseId).ElementAtOrDefault(j);
                if (elementAtOrDefault != null)
                    yield return new SelectListItem
                    {
                        Text = (elementAtOrDefault.Name == null ? "None" : elementAtOrDefault.Name),
                        Value = (elementAtOrDefault.LevelId.ToString() == "" ? "0" : elementAtOrDefault.LevelId.ToString())
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