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
        [Authorize]
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
        [Authorize(Roles = "Student")]
        [HttpGet]
        public ActionResult Get(int? LevelProjectId)
        {
            if (LevelProjectId.HasValue)
            {
                var LevelProject = LevelService.GetLevelProject(LevelProjectId.Value);
                ViewBag.LevelProject = LevelProject;
                ViewBag.AllowedFileExtensions = GetAllowedFileExtensions();
                return View(LevelProject);
            }
            return View();
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult Get(int? LevelProjectId, LevelProject levelProject)
        {
            if (LevelProjectId.HasValue)
            {
                var levelProj = LevelService.GetLevelProject(LevelProjectId.Value);

                if (levelProj != null)
                {
                    levelProj.ProjectUrl = levelProject.ProjectUrl;
                    LevelService.UpdateLevelProject(levelProj);
                }
            }

            return View("Index");
        }


        [Authorize(Roles = "Student")]
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
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Create(LevelProject levelProject)
        {
            if (ModelState.IsValid)
            {
                ViewBag.LevelCount = GetLevelCounts();
                ViewBag.GradePercentageValue = GetPercentageValue();
                LevelService.CreateLevelProject(levelProject);
            }
            return View();
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.LevelCount = GetLevelCounts();
            ViewBag.GradePercentageValue = GetPercentageValue();
            return View();
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Edit(int? levelProjectId)
        {
            ViewBag.LevelCount = GetLevelCounts();
            ViewBag.GradePercentageValue = GetPercentageValue();

            if (levelProjectId.HasValue)
            {
                var project = LevelService.GetLevelProject(levelProjectId.Value);
                project.Start = Convert.ToDateTime(project.Start.ToString("u"));
                project.Stop = Convert.ToDateTime(project.Stop.ToString("u"));
                ViewBag.NameOfProject = project.Name;
                return View(project);
            }

            return View();
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Edit(int? levelProjectId, LevelProject levelProject)
        {
            ViewBag.LevelCount = GetLevelCounts();
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
            for (int j = 0; j <= LevelService.GetLevels().Count(); j++)
            {
                var elementAtOrDefault = LevelService.GetLevels().ElementAtOrDefault(j);
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





        /*
        public ActionResult Statistics(int id)
        {
            return View();
        }*/



    }
}
