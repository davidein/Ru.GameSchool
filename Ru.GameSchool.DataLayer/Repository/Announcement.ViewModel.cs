using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.DataLayer.Repository
{
    [MetadataType(typeof(AnnouncementMetadata))]
    public partial class Announcement
    {
        [Display(Name = "Senda tilkynningu")]
        public bool SendNotification { get; set; }
    }

    public class AnnouncementMetadata
    {
        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Titill")]
        public string Title;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Texti")]
        public string Article;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Borð")]
        public int LevelId;

        [Required(ErrorMessage = @"Vantar birtingartíma")]
        [Display(Name = "Birtist")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DisplayDateTime;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Námskeið")]
        public int CourseId;
    }
}
