using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.DataLayer.Repository
{
    [MetadataType(typeof(LevelProjectMetadata))]
    public partial class LevelProject
    {

    }
    public class LevelProjectMetadata
    {
        [Required(ErrorMessage = "Vantar")]
        [Display(Name = "Verkefni byrjar")]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Vantar")]
        [Display(Name = "Verkefni endar")]
        [DataType(DataType.DateTime)]
        public DateTime Stop { get; set; }

        [Required]
        [Display(Name = "Lýsing")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        [Required]
        [Display(Name = "Vægi")]
        public double GradePercentageValue { get; set; }

        [Required]
        [Display(Name = "Nafn")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Borð")]
        public int LevelId { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000)]
        [Display(Name="Athugasemd til kennara")]
        public string UserComment { get; set; }

        
        public bool HasSubmitted { get; set; }

    }
}
