using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Ru.GameSchool.DataLayer.Interfaces;

namespace Ru.GameSchool.DataLayer.Repository
{
    [MetadataType(typeof(LevelExamMetadata))]
    public partial class LevelExam : IListObject
    {
        public bool IsNew()
        {
            return Start.AddHours(23) >= DateTime.Now;
        }

        public string ItemName()
        {
            return Name;
        }

        public DateTime Date()
        {
            return Start;
        }

        public string ItemUrl()
        {
            return string.Format("/Exam/Index/{0}", Level.CourseId);
        }

        public bool IsReturned(int userInfoId)
        {
            return _levelExamResults.Where(x => x.UserInfoId == userInfoId).Count() > 0;
        }
    }
    public class LevelExamMetadata
    {
        [Required]
        [Display(Name = "Lýsing")]
        [DataType(DataType.Text)]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Nafn")]
        [DataType(DataType.Text)]
        [StringLength(200)]
        public string Name { get; set; }

        [Display(Name = "Hlutfall")]
        [DataType(DataType.Text)]
        public double GradePercentageValue { get; set; }

        [Display(Name = "Borð")]
        [Required]
        /*[MaxLength(8)]*/
        public int LevelId { get; set; }
   
        [Required]
        [Display(Name = "Birtingar dags.")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "Skila dags.")]
        public DateTime Stop { get; set; }
        
        //[Required]
        //[DataType(DataType.Custom)]
        //[Display(Name = "Spurning")]
        //public ICollection<LevelExamQuestion> Questions { get; set; }
    }
}
