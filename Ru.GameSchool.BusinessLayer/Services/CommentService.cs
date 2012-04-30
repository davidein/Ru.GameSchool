using System.Collections.Generic;
using Ru.GameSchool.DataLayer;

namespace Ru.GameSchool.BusinessLayer.Services
{
    /// <summary>
    /// Service class that abstracts the interraction around the comment entity with the data layer.
    /// </summary> 
    public class SocialService : BaseService
    {
        public void CreateLike(CommentLike commentLike)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<CommentLike> GetCommentLikes(int commentId)
        {
            return null;
        }
        public void CreateComment(Comment comment)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteComment(int commentId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteLike(int commentLikeId)
        {
            throw new System.NotImplementedException();
        }

        public void AddCommentGivesPoints(Comment comment)
        {
            throw new System.NotImplementedException();
        }
    }
}
