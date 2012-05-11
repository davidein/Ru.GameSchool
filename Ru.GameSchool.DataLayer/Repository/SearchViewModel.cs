using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.DataLayer.Repository
{
    public class SearchViewModel
    {
        public IEnumerable<UserInfo> UserSearch { get; set; }
        public IEnumerable<Course> CourseSearch{ get; set; }
        public string Search { get; set; }
        public UserType UserType { get; set; }
    }
}
