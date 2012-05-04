using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ru.GameSchool.Web.Controllers
{
    public class ExamController : BaseController
    {
        //
        // GET: /Exam/

        [Authorize(Roles = "Student")]
        public ActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                
            }
            return View("Exam");
        }

        [Authorize(Roles = "Student")]
        public ActionResult Return(int id)
        {
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
        public ActionResult Create(int id)
        {
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
        public ActionResult Edit(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    var exam = LevelService.GetLevelExam(id.Value);
                    if (TryUpdateModel(exam))
                    {
                        LevelService.UpdateLevelExam(exam);
                    }   
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
