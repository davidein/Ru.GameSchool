using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.DataLayer.Repository;
using System.Web.Security;
using Ru.GameSchool.Web.Classes;

namespace Ru.GameSchool.Web.Controllers
{
    public class ExamController : BaseController
    {
        //
        // GET: /Exam/

        [HttpGet]
        [Authorize(Roles ="Student")]
        public ActionResult Index()
        {
            var exams = LevelService.GetLevelExams();

            ViewBag.Exams = exams.ToList();
            


            return View();
        }
       

        [HttpGet]
        [Authorize(Roles = "Student")]
        public ActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                var exam = LevelService.GetLevelExam(id.Value);
                return View(exam);
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public ActionResult Return(LevelExam levelExam)
        {
            if (ModelState.IsValid)
            {

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
        [HttpPost]
        public ActionResult Create(LevelExam levelExam)
        {
            if (ModelState.IsValid)
            {
                LevelService.CreateLevelExam(levelExam);
                return View("Index");
            }
            var user = Membership.GetUser() as GameSchoolMembershipUser;

            return View();
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Edit(int? id, string temp)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    var exam = LevelService.GetLevelExam(id.Value);
                    return View(exam);
                }
            }
            return View();
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Edit(LevelExam levelExam)
        {
            if (ModelState.IsValid)
            {
                var id = levelExam.LevelExamId;
                var exam = LevelService.GetLevelExam(id);
                if (TryUpdateModel(exam))
                {
                    LevelService.UpdateLevelExam(exam);
                }
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