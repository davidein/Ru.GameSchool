using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ru.GameSchool.DataLayer.Repository
{
    
    [MetadataType(typeof(CourseMetadata))]
    public partial class Course
    {
    }

    public class CourseMetadata
    {

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = @"Nafn á Námskeiði")]
        public string Name;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = @"Lýsing")]
        public string Description;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = @"Auðkenni")]
        public string Identifier;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = @"Námskeið hefst")]
        public DateTime Start;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = @"Námskeiði lýkur")]
        public DateTime Stop;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = @"Einingar")]
        public int CreditAmount;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = @"Deild")]
        public int DepartmentId;

    }
}
