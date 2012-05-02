using System.Collections.Generic;
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
        public void CreateLike(CommentLike commentLike)
        {
            if (commentLike != null)
            {
                GameSchoolEntities.CommentLikes.AddObject(commentLike);
                Save();
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

                GameSchoolEntities.CommentLikes.DeleteObject(like);
                Save();
            }
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
