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
        [HttpGet]
      //  [Authorize(Roles = "Student")]
        public ActionResult Index()
        {
            var exams = LevelService.GetLevelExams();

            ViewBag.Exams = exams.ToList();



            return View();
        }
        #region Student



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
        #endregion

        #region Teacher

       // [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

      //  [Authorize(Roles = "Teacher")]
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

      //  [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    var exam = LevelService.GetLevelExam(id.Value);
                    ViewBag.Exam = exam;
                }
            }
            return View();
        }

       // [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Edit(LevelExam levelExam)
        {
            if (ModelState.IsValid)
            {
                if (TryUpdateModel(levelExam))
                {
                    LevelService.UpdateLevelExam(levelExam);
                }
            }
            return View();
        }

        #endregion








        /*
        public ActionResult Statistics(int id)
        {
            return View();
        }*/

    }
}