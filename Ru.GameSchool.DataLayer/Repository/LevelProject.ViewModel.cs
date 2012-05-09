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
        [Required(ErrorMessage = "Vantar byrjunardagsetningu.")]
        [Display(Name = "Verkefni byrjar")]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Vantar endadagsetningu.")]
        [Display(Name = "Verkefni endar")]
        [DataType(DataType.DateTime)]
        public DateTime Stop { get; set; }

        [Required(ErrorMessage = "Vantar lýsingu.")]
        [Display(Name = "Lýsing")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Verður að velja vægi.")]
        [Display(Name = "Vægi")]
        public double GradePercentageValue { get; set; }

        [Required(ErrorMessage = "Vantar nafn.")]
        [Display(Name = "Nafn")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Verður að velja borð")]
        [Display(Name = "Borð")]
        public int LevelId { get; set; }

        [Display(Name = "Athugasemd til kennara")]
        public string UserFeedback { get; set; }

        [Display(Name = "Viðhengi")]
        public string ContentID { get; set; }

    }
}
