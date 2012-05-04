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

        //
        // GET: /Project/
        [Authorize(Roles = "Student")]
        public ActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                var project = LevelService.GetLevelProject(id.Value);
                return View(project);
            }
            return View();
        }

        [Authorize(Roles = "Student")]
        [HttpGet]
        public ActionResult Index()
        {
            var projects = LevelService.GetLevelProjects();
            return View(projects.ToList());
        }

        [Authorize(Roles = "Student")]
        public ActionResult Return(int id)
        {
            return View();
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
