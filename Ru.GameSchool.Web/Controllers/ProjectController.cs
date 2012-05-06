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
        #region Student
        [Authorize(Roles = "Student")]
        public ActionResult Get(int? LevelProjectId)
        {
            if (LevelProjectId.HasValue)
            {
                ViewBag.LevelProject = LevelService.GetLevelProject(LevelProjectId.Value);
                ViewBag.AllowedFileExtensions = GetAllowedFileExtensions();
            }
            return View();
        }
        [Authorize(Roles = "Student")]
        [HttpGet]
        public ActionResult Index()
        {
            var projects = LevelService.GetLevelProjects();

            ViewBag.Projects = projects.ToList();

            ViewBag.CourseName = "Vefforritun II";

            return View();
        }
        [Authorize(Roles = "Student")]
        public ActionResult Return(int id)
        {
            return View();
        }
        #endregion


        #region Teacher
        #endregion




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
  


        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Create(LevelProject levelProject)
        {
            if (ModelState.IsValid)
            {
                LevelService.CreateLevelProject(levelProject);
            }
            return View();
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var project = LevelService.GetLevelProject(id.Value);
                return View(project);
            }
            return View();
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Edit(LevelProject levelProject)
        {
            if (ModelState.IsValid)
            {
                LevelService.UpdateLevelProject(levelProject);
            }
            return View("Index");
        }
        /*
        public ActionResult Statistics(int id)
        {
            return View();
        }*/
    }
}
