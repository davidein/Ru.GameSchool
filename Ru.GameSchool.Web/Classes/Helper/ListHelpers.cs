using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ru.GameSchool.Web.Classes.Helper
{
    public static class ListHelpers
    {
        public static IEnumerable<IEnumerable<dynamic>> NestedList(this IEnumerable<dynamic> list, int amount)
        {
            List<IEnumerable<dynamic>> newList = new List<IEnumerable<dynamic>>();

            for (int i=0;i< Math.Ceiling(list.Count() / (amount * 1.0)); i++)
            {
                var small = list.Skip(i*amount).Take(amount);
                newList.Add(small);
            }

            return newList;
        }

    }
}