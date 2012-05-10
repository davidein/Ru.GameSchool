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
        public ActionResult GetComments(int? id)
        {
            if (User.Identity.IsAuthenticated && id.HasValue)
            {
                var model = SocialService.GetComments(id.Value);


                var list = from x in model
                           select new
                           {
                               x.CommentId,
                               x.Comment1,
                               //x.CommentLikes, change into a subarray
                               CommentLikes = from y in x.CommentLikes
                                              select new {
                                                y.UserInfo.Fullname,
                                                y.UserInfoId
                                              },
                               x.UserInfoId,
                               x.LevelMaterialId,
                               x.CreateDateTime,
                               x.UserInfo.Fullname
                           };



                //return View(model);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LikeComment(int? id)
        {
            if (id.HasValue)
            {
                var cUser = MembershipHelper.GetUser();
                CommentLike c = new CommentLike();
                c.UserInfoId = cUser.UserInfoId;
                c.CommentId = id.Value;
                SocialService.CreateLike(c);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateComment(Comment comment, int? id)
        {

            String strComment = comment.Comment1;
            

            if (!String.IsNullOrEmpty(strComment) && id.HasValue)
            { 
                Comment c = new Comment();
                  var  cUser = MembershipHelper.GetUser();
                  c.UserInfoId = cUser.UserInfoId;
                  c.Comment1 = strComment;
                  c.CreateDateTime = DateTime.Now;
                  c.LevelMaterialId = id.Value;
                  c.DeletedByUser = "";
                  SocialService.CreateComment(c);
                  return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            
        }


        public ActionResult getLikes(int? id)
        {
            if (id.HasValue)
            {
                var model = SocialService.GetCommentLikes(id.Value);

                var list = from x in model
                           select new
                           {
                               x.CommentId,
                               x.UserInfo.Fullname,
                               x.CommentLikeId
                           };

                return Json(list, JsonRequestBehavior.AllowGet);
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
