using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Web.Classes;
using Ru.GameSchool.Web.Classes.Helper;

namespace Ru.GameSchool.Web.Controllers
{
    public class JSonController : BaseController
    {
        //
        // GET: /JSon/

        [Authorize]
        public ActionResult GetNotifications()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = MembershipHelper.GetUser();

                if (user != null)
                {
                    var notificationList = NotificationService.GetNotifications(user.UserInfoId);
                    var list = from x in notificationList
                               select new
                                          {
                                              x.Description,
                                              x.Url,
                                              x.IsRead,
                                              x.CreateDateTime,
                                              id = x.NotificationId
                                          };
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult ClearNotifications()
        {
            if (User.Identity.IsAuthenticated)
            {
                
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LikeComment(int? id)
        {
            if (User.Identity.IsAuthenticated && id.HasValue)
            {
                var user = Membership.GetUser() as GameSchoolMembershipUser;

                if (user != null)
                {
                    var commentLike = new CommentLike();
                    commentLike.CommentLikeId = id.Value;
                    commentLike.UserInfoId = user.UserInfoId;

                    SocialService.CreateLike(commentLike);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
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
