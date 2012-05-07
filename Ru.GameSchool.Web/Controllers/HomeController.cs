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

        public ActionResult NotFound()
        {
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
