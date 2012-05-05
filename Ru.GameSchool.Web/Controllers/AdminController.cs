using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.Web.Models;

namespace Ru.GameSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {


            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Users = UserService.GetUsers();

            return View();
        }

        public ActionResult User(int? id)
        {
            return View();
        }

        public ActionResult User(UserModel model, int? id)
        {
            return View();
        }

        public ActionResult Courses()
        {
            ViewBag.Courses = CourseService.GetCourses();


            return View();
        }

        public ActionResult Course(int? id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Course(CourseModel model, int? id)
        {
            return View();
        }

        public ActionResult CourseRegisterUser(int id)
        {
            return View();
        }


    }
}
