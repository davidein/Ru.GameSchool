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
        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Notandanafn")]
        public string Username;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Lykilorð")]
        public string Password;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Netfang")]
        public string Email;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Tegund notanda")]
        public int UserTypeId;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Fullt Nafn")]
        public string Fullname;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Deild")]
        public int DepartmentId;

        [Required(ErrorMessage = @"Vantar")]
        [Display(Name = "Staða")]
        public int StatusId;

    }

}
