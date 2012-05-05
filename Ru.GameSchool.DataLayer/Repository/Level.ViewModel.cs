using System;
using System.Collections.Generic;
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

        [Required]
        [Display(Name = "Heiti borðs")]
        public string Name;

        [Required(ErrorMessage = @"Vantar opnunartíma")]
        [Display(Name = "Opið frá")]
        public DateTime Start;

        [Required(ErrorMessage = @"Vantar lokunartíma")]
        [Display(Name = "Lokar")]
        public DateTime Stop;

    
    }
}
