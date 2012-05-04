using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ru.GameSchool.Web.Controllers
{
    public class MaterialController : BaseController
    {
        //
        // GET: /Material/
        [Authorize(Roles = "Student")]
        public ActionResult Get(int id)
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

    }
}