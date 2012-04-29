using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Ru.GameSchool.Web.Controllers
{
    public class CourseController : BaseController
    {

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Level(int id, int? levelId)
        {

            return View();
        }

    }
}
