using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.DataLayer.Repository
{
    public class SearchViewModel
    {
        public string Keyword { get; set; }
        public IEnumerable<UserInfo> UserSearchResult { get; set; }
    }
}
