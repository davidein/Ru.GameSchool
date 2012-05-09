using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Web.Classes.Helper;

namespace Ru.GameSchool.Web.Controllers
{
    public class ProjectController : BaseController
    {

        #region Student & Teacher
        [Authorize(Roles = "Teacher, Student")]
        [HttpGet]
        public ActionResult Index(int? id)
        {
            var userInfoId = MembershipHelper.GetUser().UserInfoId;

            var courses = id.HasValue
                                  ? CourseService.GetCoursesByUserInfoIdAndCourseId(userInfoId, id.Value)
                                  : CourseService.GetCoursesByUserInfoId(userInfoId);

            ViewBag.Courses = courses;
            ViewBag.UserInfoId = userInfoId;
            var projects = id.HasValue
                ? LevelService.GetLevelProjectsByCourseIdAndUserInfoId(userInfoId, id.Value)
                : LevelService.GetLevelProjectsByUserId(userInfoId);

            ViewBag.CourseName = CourseService.GetCourse(id.Value).Name;
            ViewBag.CourseId = CourseService.GetCourse(id.Value).CourseId;
            ViewBag.Title = "Listi yfir verkefni";

            return View(projects);
        }

        [Authorize(Roles = "Student, Teacher")]
        [HttpGet]
        public ActionResult Get(int? id)
        {
            ViewBag.AllowedFileExtensions = GetAllowedFileExtensions();
            var user = MembershipHelper.GetUser().UserInfoId;

            if (id.HasValue)
            {
                var levelProject = LevelService.GetLevelProject(id.Value);

                ViewBag.LevelProject = levelProject;
                ViewBag.LevelProjectResult = levelProject.LevelProjectResults.Where(u => u.UserInfoId == user).ToList();
                ViewBag.CourseName = levelProject.Level.Course.Name;
                ViewBag.CourseId = levelProject.Level.CourseId;
                ViewBag.Title = "Verkefni";

                return View(levelProject);
            }

            return View();
        }
        [Authorize(Roles = "Student, Teacher")]
        [HttpPost]
        public ActionResult Get(int? id, FormCollection collection)
        {
            if (id.HasValue)
            {
                var levelProj = LevelService.GetLevelProject(id.Value);

                if (levelProj != null)
                {
                    var userInfoId = MembershipHelper.GetUser().UserInfoId;
                    levelProj.LevelProjectResults.Add(new LevelProjectResult
                    {
                        CreateDateTime = DateTime.Now,
                        UserFeedback = collection["UserFeedback"],
                        ContentID = collection["UserContent"],
                        UserInfoId = userInfoId,
                        LevelProjectId = id.Value,
                        Grade = Convert.ToInt32(collection["grade"])
                    });

                    LevelService.UpdateLevelProject(levelProj);
                }
            }
            return View("Index");
        }

        #endregion

        #region Teacher
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Create(LevelProject levelProject, int? courseId)
        {
            //ViewBag.LevelCount = GetLevelCounts(0);
            //ViewBag.GradePercentageValue = GetPercentageValue();

            if (ModelState.IsValid)
            {
                LevelService.CreateLevelProject(levelProject);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id.HasValue)
            {
                int courseId = id.Value;
                ViewBag.LevelCount = GetLevelCounts(courseId);
                ViewBag.GradePercentageValue = GetPercentageValue();
                ViewBag.CourseName = CourseService.GetCourse(courseId).Name;
                ViewBag.CourseId = CourseService.GetCourse(courseId).CourseId;
                ViewBag.Title = "Búa til verkefni";
                return View();
            }
            return RedirectToAction("NotFound","Home");
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {


            if (id.HasValue)
            {
                int levelProjectId = id.Value;
                int courseId = LevelService.GetLevelProject(levelProjectId).Level.CourseId;

                ViewBag.LevelCount = GetLevelCounts(courseId);
                ViewBag.GradePercentageValue = GetPercentageValue();
                var project = LevelService.GetLevelProject(levelProjectId);
                project.Start = Convert.ToDateTime(project.Start.ToString("u"));
                project.Stop = Convert.ToDateTime(project.Stop.ToString("u"));
                ViewBag.NameOfProject = project.Name;
                ViewBag.CourseName = LevelService.GetLevelProject(levelProjectId).Level.Course.Name;
                ViewBag.CourseId = courseId;
                ViewBag.Title = "Breyta verkefni";
                return View(project);
            }
            return View();
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Edit(int? levelProjectId, LevelProject levelProject)
        {
            int courseId = LevelService.GetLevelProject(levelProjectId.Value).Level.CourseId;
            ViewBag.LevelCount = GetLevelCounts(courseId);
            ViewBag.GradePercentageValue = GetPercentageValue();

            if (ModelState.IsValid)
            {
                LevelService.UpdateLevelProject(levelProject);
                ViewBag.SuccessMessage = "Upplýsingar um verkefni hafa verið uppfærðar";
                return View(levelProject);
            }
            else
            {
                ViewBag.ErrorMessage = "Náði ekki að skrá/uppfæra upplýsingar! Lagfærðu villur og reyndur aftur.";
                if (levelProjectId != null) return View(LevelService.GetLevelProject(levelProjectId.Value));
            }
            return View(levelProject);
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Delete(int? levelProjectId)
        {
            if (levelProjectId.HasValue)
            {

                ViewBag.CourseName = LevelService.GetLevelProject(levelProjectId.Value).Level.Course.Name;
                ViewBag.CourseId = LevelService.GetLevelProject(levelProjectId.Value).Level.CourseId;
                ViewBag.Title = "Eyða verkefni";

                if (LevelService.DeleteLevelProject(levelProjectId.Value))
                {
                    ViewBag.DeleteSuccessMessage = "Virkaði að eyða færslu!";
                }
                else
                {
                    ViewBag.DeleteSuccessMessage = "Ekki virkaði að eyða færslu!";
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region UnsortedHelperMethds
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

        public IEnumerable<SelectListItem> GetLevelCounts(int CourseId)
        {
            for (int j = 0; j <= LevelService.GetLevels(CourseId).Count(); j++)
            {
                var elementAtOrDefault = LevelService.GetLevels(CourseId).ElementAtOrDefault(j);
                if (elementAtOrDefault != null)
                    yield return new SelectListItem
                                     {
                                         Text = elementAtOrDefault.Name,
                                         Value = elementAtOrDefault.LevelId.ToString()
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
        #endregion

    }
}
