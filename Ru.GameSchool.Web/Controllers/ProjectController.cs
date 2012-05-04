using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ru.GameSchool.Web.Controllers
{
    public class ProjectController : BaseController
    {
        //
        // GET: /Project/
        [Authorize(Roles = "Student")]
        public ActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                
            }
            return View();
        }

        [Authorize(Roles = "Student")]
        public ActionResult Return(int id)
        {
            return View();
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int id)
        {
            return View();
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int id)
        {
            return View();
        }
        /*
        public ActionResult Statistics(int id)
        {
            return View();
        }*/
    }
}
