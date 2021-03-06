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
    public partial class LevelExamQuestion
    {
        #region Primitive Properties
    
        public virtual int LevelExamQuestionId
        {
            get;
            set;
        }
    
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
    
        public virtual string Question
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<LevelExamAnswer> LevelExamAnswers
        {
            get
            {
                if (_levelExamAnswers == null)
                {
                    var newCollection = new FixupCollection<LevelExamAnswer>();
                    newCollection.CollectionChanged += FixupLevelExamAnswers;
                    _levelExamAnswers = newCollection;
                }
                return _levelExamAnswers;
            }
            set
            {
                if (!ReferenceEquals(_levelExamAnswers, value))
                {
                    var previousValue = _levelExamAnswers as FixupCollection<LevelExamAnswer>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupLevelExamAnswers;
                    }
                    _levelExamAnswers = value;
                    var newValue = value as FixupCollection<LevelExamAnswer>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupLevelExamAnswers;
                    }
                }
            }
        }
        private ICollection<LevelExamAnswer> _levelExamAnswers;
    
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
    
        private void FixupLevelExam(LevelExam previousValue)
        {
            if (previousValue != null && previousValue.LevelExamQuestions.Contains(this))
            {
                previousValue.LevelExamQuestions.Remove(this);
            }
    
            if (LevelExam != null)
            {
                if (!LevelExam.LevelExamQuestions.Contains(this))
                {
                    LevelExam.LevelExamQuestions.Add(this);
                }
                if (LevelExamId != LevelExam.LevelExamId)
                {
                    LevelExamId = LevelExam.LevelExamId;
                }
            }
        }
    
        private void FixupLevelExamAnswers(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (LevelExamAnswer item in e.NewItems)
                {
                    item.LevelExamQuestion = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (LevelExamAnswer item in e.OldItems)
                {
                    if (ReferenceEquals(item.LevelExamQuestion, this))
                    {
                        item.LevelExamQuestion = null;
                    }
                }
            }
        }

        #endregion
    }
}
