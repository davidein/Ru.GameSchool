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
            if (User.IsInRole("Admin"))
            {
<<<<<<< HEAD
               return RedirectToAction("Index", "Admin");
            }
            else
            {
            var user = MembershipHelper.GetUser();
            var courseList = CourseService.GetCoursesByUserInfoId(user.UserInfoId);

            foreach (var item in courseList)
            {   
                var stuff = CourseService.GetCourseNewestItems(item.CourseId,user.UserInfoId);

                ViewData.Add("Course" + item.CourseId, CourseService.GetCourseNewestItems(item.CourseId,user.UserInfoId));
            }

            return View();
=======
                return RedirectToAction("Index", "Admin");
>>>>>>> 5e8c0203d81ba2cdc4ecf17236245ae8c9e9eb78
            }

            var user = MembershipHelper.GetUser();
            ViewBag.AnnouncementList = AnnouncementService.GetAnnouncementsByUserInfoId(user.UserInfoId).Take(3);

            return View();
        }

        public ActionResult About()
        {

            ViewBag.Title = "Um GameSchool";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Hafðu samband";

            return View();
        }

        public ActionResult NotFound()
        {
            ViewBag.Title = "Hluturinn fannst ekki";
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
