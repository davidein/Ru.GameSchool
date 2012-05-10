using System;
using System.Collections.Generic;
using Ru.GameSchool.BusinessLayer.Exceptions;
using Ru.GameSchool.DataLayer;
using Ru.GameSchool.DataLayer.Repository;
using System.Linq;

namespace Ru.GameSchool.BusinessLayer.Services
{
    /// <summary>
    /// Service class that abstracts the interraction around the comment entity with the data layer.
    /// </summary> 
    public class SocialService : BaseService
    {
        /// <summary>
        /// Gets an instance of a commentlike object through parameter of the function, if the object isn't null persist it to the datasource.
        /// </summary>
        /// <param name="commentLike">Instance of commentlike object.</param>
        /// <exception cref="GameSchoolException"></exception>
        public void CreateLike(CommentLike commentLike)
        {
            if (commentLike != null)
            {
                if (GameSchoolEntities.Comments.Where(x=>x.CommentId == commentLike.CommentId).Count() != 1)
                {
                    throw new GameSchoolException(string.Format("Comment does not exist. CommentId = {0}",commentLike.CommentId));
                }

                if (GameSchoolEntities.UserInfoes.Where(x => x.UserInfoId == commentLike.UserInfoId).Count() != 1)
                {
                    throw new GameSchoolException(string.Format("User does not exist. UserInfoId = {0}", commentLike.UserInfoId));
                }

                if (GameSchoolEntities.CommentLikes.Where(x => x.CommentId == commentLike.CommentId && x.UserInfoId == commentLike.UserInfoId).Count() > 0)
                {
                    //throw new GameSchoolException(string.Format("Notandi hefur nú þegar líkað við þetta comment. UserInfoId = {0}", commentLike.UserInfoId));

                }

                else
                {
                    Comment likeComment = GameSchoolEntities.Comments.Where(x => x.CommentId == commentLike.CommentId).FirstOrDefault();
                    UserInfo commentUser = GameSchoolEntities.UserInfoes.Where(x => x.UserInfoId == likeComment.UserInfoId).FirstOrDefault();
                    UserInfo likeUser = GameSchoolEntities.UserInfoes.Where(x => x.UserInfoId == commentLike.UserInfoId).FirstOrDefault();
                    LevelMaterial commentLevelMaterial = GameSchoolEntities.LevelMaterials.Where(x => x.LevelMaterialId == likeComment.LevelMaterialId).FirstOrDefault();

                    GameSchoolEntities.CommentLikes.AddObject(commentLike);
                    Save();



                    if (likeUser.UserTypeId == (int)Enums.UserType.Teacher && commentUser.UserTypeId != (int)Enums.UserType.Teacher)
                    {
                        int userId = likeComment.UserInfoId;
                        int levelId = commentLevelMaterial.LevelId;
                        int points = 5;
                        string pointtype = "Þú hefur fengið {0} stig fyrir ummælum um {1}";

                        ExternalPointContainer.AddPointsToLevel(userId,levelId,points,pointtype);
                        ExternalNotificationContainer.CreateNotification("Þú hefur fengið 5 stig fyrir að kennara hefur líkað við ummæli þin við: "+ commentLevelMaterial.Title,"/Material/Get/" + commentLevelMaterial.LevelMaterialId,userId);
                    
                    }
                }
            }
        }
        /// <summary>
        /// Gets a colllection of commentlike objects that are associated with a given commentid.
        /// </summary>
        /// <param name="commentId">Id of the comment associated with the commentlikes.</param>
        /// <returns>Collection of commentlike objects</returns>
        public IEnumerable<CommentLike> GetCommentLikes(int commentId)
        {
            return commentId > 0 ? GameSchoolEntities.CommentLikes.Where(c => c.CommentId == commentId) : null;
        }
        /// <summary>
        /// Gets an instance of a comment object through parameter of the function, if the object isn't null persist it to the datasource.
        /// </summary>
        /// <param name="comment">Instance of comment object.</param>
        public void CreateComment(Comment comment)
        {
            if (comment != null)
            {
                GameSchoolEntities.Comments.AddObject(comment);
                Save();
            }
        }
        /// <summary>
        /// Gets an integer value of a comment object to remove from the datasource, if the id is equal or larger then 1 then remove it.
        /// </summary>
        /// <param name="commentId">Id of a given comment in the datasource.</param>
        public void DeleteComment(int commentId)
        {
            if (commentId > 0)
            {
                var query = GameSchoolEntities.Comments.Where(c => c.CommentId == commentId);

                var comment = query.FirstOrDefault();

                GameSchoolEntities.Comments.DeleteObject(comment);
                Save();
            }
        }
        /// <summary>
        /// Gets an integer value of a commentlike object to remove from the datasource, if the id is equal or larger then 1 then remove it.
        /// </summary>
        /// <param name="commentLikeId">Id of a given commentlike in the datasource.</param>
        public void DeleteLike(int commentLikeId)
        {
            if (commentLikeId > 0)
            {
                var query = GameSchoolEntities.CommentLikes.Where(cl => cl.CommentLikeId == commentLikeId);

                var like = query.FirstOrDefault();

                if (like == null)
                    throw new GameSchoolException(string.Format("Like not found. CommentLikeId = {0}", commentLikeId));

                GameSchoolEntities.CommentLikes.DeleteObject(like);
                Save();
            }
        }
        /// <summary>
        /// Gets a lcollection of comment objects associated with this levelmaterial.
        /// </summary>
        /// <param name="levelMaterialId">The integer value of a levelmaterialid to find.</param>
        /// <returns>Collection of comment objects.</returns>
        public IEnumerable<Comment> GetComments(int levelMaterialId)
        {
            if (levelMaterialId > 0)
            {
               var list = GameSchoolEntities.Comments.Where(x => x.LevelMaterialId == levelMaterialId);

                list = list.OrderBy(x => x.CommentId);

                return list;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        public void AddCommentGivesPoints(Comment comment)
        {
            if (comment != null)
            {
                // TODO: Finish implementation and commenting.
            }
        }
    }
}
