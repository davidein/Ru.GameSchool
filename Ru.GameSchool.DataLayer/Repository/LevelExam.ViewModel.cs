using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ru.GameSchool.DataLayer.Repository
{
    [MetadataType(typeof(LevelExamMetadata))]
    public partial class LevelExam
    {
    }
    public class LevelExamMetadata
    {
        [Required]
        [Display(Name = "Lýsing")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [Display(Name ="Nafn")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        
        [Display(Name = "Hlutfall prósentu")]
        [DataType(DataType.Text)]
        public double GradePercentageLevel { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTest { get; set; }
    }
}
