//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Ru.GameSchool.DataLayer.Repository
{
    public partial class CommentLike
    {
        #region Primitive Properties
    
        public virtual int CommentLikeId
        {
            get;
            set;
        }
    
        public virtual int CommentId
        {
            get { return _commentId; }
            set
            {
                if (_commentId != value)
                {
                    if (Comment != null && Comment.CommentId != value)
                    {
                        Comment = null;
                    }
                    _commentId = value;
                }
            }
        }
        private int _commentId;
    
        public virtual int UserInfoId
        {
            get { return _userInfoId; }
            set
            {
                if (_userInfoId != value)
                {
                    if (UserInfo != null && UserInfo.UserInfoId != value)
                    {
                        UserInfo = null;
                    }
                    _userInfoId = value;
                }
            }
        }
        private int _userInfoId;

        #endregion
        #region Navigation Properties
    
        public virtual Comment Comment
        {
            get { return _comment; }
            set
            {
                if (!ReferenceEquals(_comment, value))
                {
                    var previousValue = _comment;
                    _comment = value;
                    FixupComment(previousValue);
                }
            }
        }
        private Comment _comment;
    
        public virtual UserInfo UserInfo
        {
            get { return _userInfo; }
            set
            {
                if (!ReferenceEquals(_userInfo, value))
                {
                    var previousValue = _userInfo;
                    _userInfo = value;
                    FixupUserInfo(previousValue);
                }
            }
        }
        private UserInfo _userInfo;

        #endregion
        #region Association Fixup
    
        private void FixupComment(Comment previousValue)
        {
            if (previousValue != null && previousValue.CommentLikes.Contains(this))
            {
                previousValue.CommentLikes.Remove(this);
            }
    
            if (Comment != null)
            {
                if (!Comment.CommentLikes.Contains(this))
                {
                    Comment.CommentLikes.Add(this);
                }
                if (CommentId != Comment.CommentId)
                {
                    CommentId = Comment.CommentId;
                }
            }
        }
    
        private void FixupUserInfo(UserInfo previousValue)
        {
            if (previousValue != null && previousValue.CommentLikes.Contains(this))
            {
                previousValue.CommentLikes.Remove(this);
            }
    
            if (UserInfo != null)
            {
                if (!UserInfo.CommentLikes.Contains(this))
                {
                    UserInfo.CommentLikes.Add(this);
                }
                if (UserInfoId != UserInfo.UserInfoId)
                {
                    UserInfoId = UserInfo.UserInfoId;
                }
            }
        }

        #endregion
    }
}
