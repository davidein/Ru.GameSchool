using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Mvc;

namespace Ru.GameSchool.Web.Controllers
{
    public class CourseController : BaseController
    {

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Item(int id)
        {

            return View();
        }

        
        //TODO: Impliment Role Authentication
        public ActionResult Create(int id)
        {

            return View();
        }

        //TODO: Create Post ActionResult


        //TODO: Impliment Role Authentication
        public ActionResult Edit(int id)
        {

            return View();
        }
    }
}
