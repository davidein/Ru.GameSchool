using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.DataLayer.Repository
{
    [MetadataType(typeof(LevelExamQuestionMetadata))]
    public partial class LevelExamQuestion
    {
    }
    public class LevelExamQuestionMetadata
    {
        [StringLength(200, ErrorMessage = "Spurning er of löng")]
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Spurning")]
        public string Question { get; set; }


        [Required]
        [Display(Name = "Svör")]
        public List<LevelExamAnswer> LevelExamAnswers { get; set; } 
    }
}
