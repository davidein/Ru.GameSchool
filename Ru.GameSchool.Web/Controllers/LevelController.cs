using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Web.Classes.Helper;

namespace Ru.GameSchool.Web.Controllers
{
    public class LevelController : BaseController
    {
        //
        // GET: /Level/

        [Authorize(Roles = "Student,Teacher")]
        public ActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                var user = MembershipHelper.GetUser();

                int levelId = id.Value;

                var model = LevelService.GetLevel(levelId);
                if (model != null)
                {

                    int courseId = model.CourseId;
                    ViewBag.CourseName = model.Course.Name;
                    ViewBag.Levels = LevelService.GetLevels(courseId);
                    ViewBag.Projects = LevelService.GetLevelProjects();
                    ViewBag.MaterialsVideo = LevelService.GetLevelMaterials(levelId,1);
                    ViewBag.MaterialsSlides = LevelService.GetLevelMaterials(levelId,2);
                    ViewBag.MaterialsMessages = LevelService.GetLevelMaterials(levelId);
                    ViewBag.MaterialsDocs = LevelService.GetLevelMaterials(levelId,3);
                    ViewBag.Exams = LevelService.GetLevelExams(courseId, user.UserInfoId);

                    return View(model);
                }
                
            }

            return RedirectToAction("NotFound","Home");
        }


        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        //public ActionResult Create(int CoureId)
        {
            ViewBag.CourseList = new SelectList(CourseService.GetCourses(), "CourseId", "Name"); 
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(Level level)
        {
            if (ModelState.IsValid)
            {

                level.CreateDateTime = DateTime.Now;

                LevelService.CreateLevel(level);
                
            }

            return RedirectToAction("Edit", new { Id = level.LevelId });
        }



        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var model = LevelService.GetLevel(id.Value);
                return View(model);
            }

            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(Level model, int? id)
        {
            if (ModelState.IsValid)
            {
                var level = LevelService.GetLevel(model.LevelId);

                if (TryUpdateModel(level))
                {
                    LevelService.UpdateLevel(level);
                }
            }

            return View(model);
        }
        /*
        public ActionResult Statistics(int id)
        {
            return View();
        }*/

    }
}
