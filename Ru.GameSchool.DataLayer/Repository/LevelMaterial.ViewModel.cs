using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ru.GameSchool.DataLayer.Repository
{
    [MetadataType(typeof(LevelMaterialMetadata))]
    public partial class LevelMaterial
    {

    }
    public class LevelMaterialMetadata
    {

        [Required(ErrorMessage = @"Það vantar nafn")]
        [Display(Name = "Nafn")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required(ErrorMessage = @"Þú verður að velja borð")]
        [Display(Name = "Borð")]
        public int LevelId { get; set; }

        [Required(ErrorMessage = @"Þú verður að tilgreina tegund")]
        [Display(Name = "Tegund kennsluefnis")]
        public int ContentTypeId { get; set; }

        //[Required(ErrorMessage = "Það vantar skrá")]
        //[Display(Name = "Skrá")]
        //public IEnumerable<HttpPostedFileBase> File { get; set; }
        
        [Required]
        [Display(Name = "Lýsing")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

    }
}
