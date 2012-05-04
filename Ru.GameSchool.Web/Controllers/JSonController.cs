using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ru.GameSchool.Web.Classes;

namespace Ru.GameSchool.Web.Controllers
{
    public class JSonController : BaseController
    {
        //
        // GET: /JSon/

        //[HttpPost]
        public ActionResult GetNotifications(int? id)
        {
            if (id.HasValue)
            {
                var notificationList = NotificationService.GetNotifications(id.Value);
                var list = from x in notificationList
                           select new
                                      {
                                          x.Description,
                                          x.Url,
                                          x.IsRead,
                                          x.CreateDateTime
                                      };
                return Json(list, JsonRequestBehavior.AllowGet);
            }

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
        public ActionResult UnLikeMaterial(int id)
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
