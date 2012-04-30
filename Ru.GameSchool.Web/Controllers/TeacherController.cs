using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ru.GameSchool.Web.Controllers
{
    public class TeacherController : BaseController
    {
        //
        // GET: /Teacher/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditLevel()
        {
            return View();
        }

        public ActionResult EditMessage()
        {
            return View();
        }


         public ActionResult CommentList()
        {
            return View();
        }
        
        public ActionResult EditProject()
        {
            return View();
        }
        
        public ActionResult EditTest()
        {
            return View();
        }
        
        public ActionResult EditMaterial()
        {
            return View();
        }
        
        public ActionResult ProjectReturned()
        {
            return View();
        }
        
    }
}
