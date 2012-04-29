using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ru.GameSchool.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Test Modify this template to jump-start your ASP.NET MVC application.";

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

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Tests()
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

    }
}
