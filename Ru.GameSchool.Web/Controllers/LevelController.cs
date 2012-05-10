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

                    int userId = ViewBag.User.UserInfoId;
                    int courseId = model.CourseId;

                    ViewBag.Title = "Borð";
                    ViewBag.CourseId = courseId;
                    ViewBag.CourseName = model.Course.Name;
                    //ViewBag.Levels = LevelService.GetLevels(courseId);
                    ViewBag.LevelTabs = LevelService.GetLevelTabsByCourseIdAndUserInfoId(courseId, userId);
                    ViewBag.Projects = LevelService.GetLevelProjectsByLevelId(levelId);
                    ViewBag.MaterialsVideo = LevelService.GetLevelMaterials(levelId,1);
                    ViewBag.MaterialsSlides = LevelService.GetLevelMaterials(levelId,2);
                    ViewBag.MaterialsMessages = LevelService.GetLevelMaterials(levelId);
                    ViewBag.MaterialsDocs = LevelService.GetLevelMaterials(levelId,3);
                    ViewBag.Exams = LevelService.GetLevelExamsByLevelId(levelId, userId);

                    return View(model);
                }
                
            }

            return RedirectToAction("NotFound","Home");
        }


        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? id)
        //public ActionResult Create(int CoureId)
        {
            if (id.HasValue)
            {
                ViewBag.Title = "Búa til  borð";
                ViewBag.CourseId = id.Value;
                ViewBag.CourseName = CourseService.GetCourse(id.Value).Name;
                ViewBag.CourseList = new SelectList(CourseService.GetCourses(), "CourseId", "Name");

                Level model = new Level();

                model.CourseId = id.Value;
                model.Start = DateTime.Now;
                model.Stop = DateTime.Now.AddMonths(3);

                return View(model);
            }
            return RedirectToAction("NotFound", "Home");
        }


        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(Level level, int? id)
        {
            if (ModelState.IsValid)
            {

                level.CreateDateTime = DateTime.Now;

                LevelService.CreateLevel(level);
                return RedirectToAction("Edit", new { Id = level.LevelId });
                
            }

            ViewBag.Title = "Búa til  borð";
            ViewBag.CourseId = id.Value;
            ViewBag.CourseName = CourseService.GetCourse(id.Value).Name;
            ViewBag.CourseList = new SelectList(CourseService.GetCourses(), "CourseId", "Name");
            level.CourseId = id.Value;

            return View(level);
            
        }



        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {

                var model = LevelService.GetLevel(id.Value);

                ViewBag.Title = "Breyta borði";
                ViewBag.CourseId = model.CourseId;
                ViewBag.CourseName = model.Course.Name;

                return View(model);
            }


            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(Level model, int? id)
        {
            var level = LevelService.GetLevel(model.LevelId);

            if (ModelState.IsValid)
            {

                if (TryUpdateModel(level))
                {
                    LevelService.UpdateLevel(level);
                }
            }

            ViewBag.Title = "Breyta borði";
            ViewBag.CourseId = model.CourseId;
            ViewBag.CourseName = level.Course.Name;

            return View(model);
        }
        /*
        public ActionResult Statistics(int id)
        {
            return View();
        }*/

    }
}
