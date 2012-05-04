using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ru.GameSchool.DataLayer.Repository
{
    [MetadataType(typeof(UserInfoMetadata))]
    public partial class UserInfo
	{
	}

    public class UserInfoMetadata
    {
        [Required]
        [Display(Name = "Notandanafn")]
        public string Username;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Netfang")]
        public int Email;
    }

}
