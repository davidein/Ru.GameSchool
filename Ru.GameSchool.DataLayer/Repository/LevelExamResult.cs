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
    public partial class LevelExamResult
    {
        #region Primitive Properties
    
        public virtual int LevelExamId
        {
            get { return _levelExamId; }
            set
            {
                if (_levelExamId != value)
                {
                    if (LevelExam != null && LevelExam.LevelExamId != value)
                    {
                        LevelExam = null;
                    }
                    _levelExamId = value;
                }
            }
        }
        private int _levelExamId;
    
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
    
        public virtual double Grade
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
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
    
        public virtual LevelExam LevelExam
        {
            get { return _levelExam; }
            set
            {
                if (!ReferenceEquals(_levelExam, value))
                {
                    var previousValue = _levelExam;
                    _levelExam = value;
                    FixupLevelExam(previousValue);
                }
            }
        }
        private LevelExam _levelExam;

        #endregion
        #region Association Fixup
    
        private void FixupUserInfo(UserInfo previousValue)
        {
            if (previousValue != null && previousValue.LevelExamResults.Contains(this))
            {
                previousValue.LevelExamResults.Remove(this);
            }
    
            if (UserInfo != null)
            {
                if (!UserInfo.LevelExamResults.Contains(this))
                {
                    UserInfo.LevelExamResults.Add(this);
                }
                if (UserInfoId != UserInfo.UserInfoId)
                {
                    UserInfoId = UserInfo.UserInfoId;
                }
            }
        }
    
        private void FixupLevelExam(LevelExam previousValue)
        {
            if (previousValue != null && previousValue.LevelExamResults.Contains(this))
            {
                previousValue.LevelExamResults.Remove(this);
            }
    
            if (LevelExam != null)
            {
                if (!LevelExam.LevelExamResults.Contains(this))
                {
                    LevelExam.LevelExamResults.Add(this);
                }
                if (LevelExamId != LevelExam.LevelExamId)
                {
                    LevelExamId = LevelExam.LevelExamId;
                }
            }
        }

        #endregion
    }
}
