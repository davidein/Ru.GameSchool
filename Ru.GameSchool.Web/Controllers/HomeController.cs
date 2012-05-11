using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ru.GameSchool.DataLayer.Interfaces;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Web.Classes;
using Ru.GameSchool.Web.Classes.Helper;

namespace Ru.GameSchool.Web.Controllers
{
    public class CourseJoin
    {
        public Course Course { get; set; }
        public IEnumerable<IListObject> Content { get; set; }
    }

    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {

                return RedirectToAction("Index", "Admin");

            }
            var user = MembershipHelper.GetUser();
            var courseList = from x in CourseService.GetCoursesByUserInfoId(user.UserInfoId)
                             select new CourseJoin()
                                        {
                                            Course = x,
                                            Content = CourseService.GetCourseNewestItems(x.CourseId, user.UserInfoId)
                                        };


            ViewBag.FrontCourseList = courseList.NestedList(3);


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
