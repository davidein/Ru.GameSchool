using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.Web.Models;

namespace Ru.GameSchool.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditUser(int id)
        {
            return View();
        }

        public ActionResult EditUser(UserModel model)
        {
            return View();
        }

        public ActionResult Course()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Course(CourseModel model)
        {
            return View();
        }

        public ActionResult UserList()
        {
            return View();
        }

        public ActionResult CourseList()
        {
            return View();
        }

        public ActionResult CourseRegisterUser(int id)
        {
            return View();
        }


    }
}
