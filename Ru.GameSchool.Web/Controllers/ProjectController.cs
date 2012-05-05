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
        public ActionResult Index()
        {
            var user = UserService.GetUser(User.Identity.Name);
            var projects = LevelService.GetUserLevelProject(user.UserInfoId).ToList();

            ViewBag.Projects = projects;

            ViewBag.CourseName = projects.ElementAt(0).Level.Course.Name;

            return View();
        }


        #region Student
     //   [Authorize(Roles = "Student")]
        public ActionResult Get(int? LevelProjectId)
        {
            if (LevelProjectId.HasValue)
            {
                ViewBag.LevelProject = LevelService.GetLevelProject(LevelProjectId.Value);
                ViewBag.AllowedFileExtensions = GetAllowedFileExtensions();
            }
            return View();
        }



        //    [Authorize(Roles = "Student")]
        public ActionResult Return(int id)
        {
            return View();
        }
        #endregion


        #region Teacher
      //  [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Create(LevelProject levelProject)
        {
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


        //       [Authorize(Roles = "Teacher")]
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

        //        [Authorize(Roles = "Teacher")]
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




        /*
        public ActionResult Statistics(int id)
        {
            return View();
        }*/
    }
}
