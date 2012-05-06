using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.DataLayer.Repository;

namespace Ru.GameSchool.Web.Controllers
{
    public class ProjectController : BaseController
    {
        //[Authorize(Roles = "Student")]
        //[Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Index(int? courseId)
        {
            IEnumerable<LevelProject> projects = null;
            if (courseId.HasValue)
            {
                projects = LevelService.GetLevelProjectsByCourseId(courseId.Value);
                var nameOfCourse = projects.Select(x => x.Level.Course.Name ?? string.Empty).FirstOrDefault();

                ViewBag.NameOfCourse = nameOfCourse;
            }
            else
            {
                projects = LevelService.GetLevelProjects();
            }
            ViewBag.Projects = projects;
            return View();
        }


        #region Student
        //   [Authorize(Roles = "Student")]
        [HttpGet]
        public ActionResult Get(int? LevelProjectId)
        {
            if (LevelProjectId.HasValue)
            {
                ViewBag.LevelProject = LevelService.GetLevelProject(LevelProjectId.Value);
                ViewBag.AllowedFileExtensions = GetAllowedFileExtensions();
            }
            return View();
        }

        //   [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult Get(LevelProject levelProject)
        {
            if (TryUpdateModel(levelProject))
            {
                LevelService.CreateLevelProject(levelProject);
            }
            return View();
        }


        //    [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult Return(LevelProject levelProject)
        {
            if (TryUpdateModel(levelProject))
            {
                LevelService.CreateLevelProject(levelProject);
            }

            return View();
        }
        #endregion


        #region Teacher
        //  [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Create(LevelProject levelProject)
        {
            levelProject.ProjectUrl = Request.Form["user_name"];

            if (ModelState.IsValid)
            {
                LevelService.CreateLevelProject(levelProject);
            }
            return View();
        }

        // [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.LevelCount = GetLevelCounts();
            ViewBag.GradePercentageValue = GetPercentageValue();
            return View();
        }


        // [Authorize(Roles = "Kennari")]
        [HttpGet]
        public ActionResult Edit(int? LevelProjectId)
        {
            if (LevelProjectId.HasValue)
            {
                var project = LevelService.GetLevelProject(LevelProjectId.Value);
                return View(project);
            }
            return View();
        }

        //[Authorize(Roles = "Kennari")]
        [HttpPost]
        public ActionResult Edit(LevelProject levelProject)
        {
            if (ModelState.IsValid)
            {
                LevelService.UpdateLevelProject(levelProject);
            }
            return View("Index");
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
        public IEnumerable<SelectListItem> GetLevelCounts()
        {
            for (int j = 1; j <= LevelService.GetLevels().Count(); j++)
            {
                yield return new SelectListItem
                {
                    Text = j.ToString(),
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
        #endregion





        /*
        public ActionResult Statistics(int id)
        {
            return View();
        }*/



    }
}
