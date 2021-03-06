﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Ru.GameSchool.DataLayer.Repository
{
    [MetadataType(typeof(LevelExamAnswerMetadata))]
    public partial class LevelExamAnswer
    {

    }
    public class LevelExamAnswerMetadata
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(200)]
        [Display(Name = "Svar")]
        public string Answer { get; set; }

        [Required]
        [Display(Name = "Rétt svar?")]
        public bool Correct { get; set; }

        public int LevelExamQuestionId { get; set; }
    }
}
