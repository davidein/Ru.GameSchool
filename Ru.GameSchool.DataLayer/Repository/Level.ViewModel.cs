using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ru.GameSchool.DataLayer.Repository
{
    [MetadataType(typeof(LevelMetadata))]
    public partial class Level 
    {
    }

    public class LevelMetadata
    {
        [Required(ErrorMessage = @"Það verður að gefa borðinu nafn")]
        [Display(Name = "Heiti borðs")]
        public string Name;

        [Required(ErrorMessage = @"Vantar opnunartíma")]
        [Display(Name = "Opið frá")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Start;

        [Required(ErrorMessage = @"Vantar lokunartíma")]
        [Display(Name = "Lokar")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy hh:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime Stop;

        [Required]
        [Display(Name="Námskeið")]
        public int CourseId;
    }
}
