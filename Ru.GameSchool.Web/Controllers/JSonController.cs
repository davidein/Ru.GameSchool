using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ru.GameSchool.Web.Controllers
{
    public class JSonController : BaseController
    {
        //
        // GET: /JSon/

        [HttpPost]
        public ActionResult GetNotifications(int id)
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LikeComment(int id)
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LikeMaterial(int id)
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CommentMaterial(int id)
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
