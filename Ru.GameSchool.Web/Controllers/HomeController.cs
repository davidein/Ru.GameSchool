using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ru.GameSchool.Web.Classes;
using Ru.GameSchool.Web.Classes.Helper;

namespace Ru.GameSchool.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Test Modify this template to jump-start your ASP.NET MVC application.";

            var user = Membership.GetUser() as GameSchoolMembershipUser;
            if (user != null)
            {
                //var list = CourseService.GetCourses();
                var list = CourseService.GetCoursesByUserInfoId(user.UserInfoId);
                //list.
                //ViewBag.ItemCounter = list.NestedList(3);
                ViewBag.CourseList = list.NestedList(3);
                //ViewBag.CourseList = CourseService.GetCoursesByUserInfoId(user.UserInfoId);
            }
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }

        // Temp - left in so as to view prototype
        public ActionResult Level()
        {
            return View();
        }

        public ActionResult Message()
        {
            return View();
        }

        public ActionResult Messages()
        {
            return View();
        }


        public ActionResult Project()
        {
            return View();
        }

        public ActionResult Projects()
        {
            return View();
        }

        public ActionResult Exam()
        {
            return View();
        }

        public ActionResult Exams()
        {
            return View();
        }

        public ActionResult Material()
        {
            return View();
        }

        public ActionResult MaterialList()
        {
            return View();
        }

        //remove to here.

    }
}
