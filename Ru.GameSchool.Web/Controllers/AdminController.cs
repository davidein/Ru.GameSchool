using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ru.GameSchool.Web.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Teacher()
        {
            return View();
        }

                public ActionResult Student()
        {
            return View();
        }

                public ActionResult Course()
        {
            return View();
        }

                public ActionResult TeacherList()
        {
            return View();
        }

                public ActionResult StudentList()
        {
            return View();
        }

                public ActionResult CourseList()
        {
            return View();
        }

                public ActionResult CourseRegisterTeacher()
        {
            return View();
        }

                public ActionResult CourseRegisterStudent()
        {
            return View();
        }
    }
}
