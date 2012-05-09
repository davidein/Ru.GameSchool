using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Web.Classes;
using Ru.GameSchool.Web.Classes.Helper;
using Ru.GameSchool.BusinessLayer.Enums;

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

        [HttpGet]
        public ActionResult GetComments(int id)
        {

            var model = SocialService.GetComments(id);
            return Json(model, JsonRequestBehavior.AllowGet);
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
        public ActionResult CreateComment(Comment comment, int id)
        {

            String strComment = comment.Comment1;
            

            if (!String.IsNullOrEmpty(strComment))
            {
                Comment c = new Comment();
                  var  cUser = MembershipHelper.GetUser();
                  c.UserInfoId = cUser.UserInfoId;
                  c.Comment1 = strComment;
                  c.CreateDateTime = DateTime.Now;
                  c.LevelMaterialId = id;
                  c.DeletedByUser = "";
                  SocialService.CreateComment(c);
                  return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("CommentText", "Kjánaprik. Ætlarðu að setja inn tóma athugasemd?");
                return RedirectToAction("Get","Material");
            }

            
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

        [HttpPost]
        public ActionResult AddUserToCourse(int userId, int courseId)
        {
            string userAddedToCourse = "Villa: Unknown Failure!";
            ResponseStatus responseStatus = ResponseStatus.Error;

            if(userId > 0 && courseId > 0)
            {
                userAddedToCourse = CourseService.AddUserToCourse(userId, courseId, out responseStatus);
            }
            
            var userToCourseResponse = new {
                message = userAddedToCourse,
                status = responseStatus
            };

            return Json(userToCourseResponse, JsonRequestBehavior.AllowGet);
        }
    }

    
}
