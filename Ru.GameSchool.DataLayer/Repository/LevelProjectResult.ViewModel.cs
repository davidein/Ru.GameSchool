using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.DataLayer.Repository
{
    [MetadataType(typeof(LevelProjectResultMetadata))]
    public partial class LevelProjectResult
    {
    }
    
    public class LevelProjectResultMetadata
    {
        [Display(Name = "Athugasemdir frá kennara")]
        [DataType(DataType.MultilineText)]
        public string TeacherFeedback { get; set; }

        [Display(Name = "Viðhengi")]
        public string ContentID{ get; set; }

        [Display(Name = "Athugasemdir frá nemanda")]
        [DataType(DataType.MultilineText)]
        public string UserFeedback { get; set; }

        [Display(Name = "Einkunn")]
        [DataType(DataType.Text)]
        public double Grade { get; set; }
       
    }
}
