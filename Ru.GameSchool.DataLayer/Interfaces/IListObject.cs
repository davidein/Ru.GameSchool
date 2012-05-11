using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.DataLayer.Interfaces
{
    public interface IListObject
    {
        string ItemName();
        string ItemUrl();
        bool IsNew();
        DateTime Date();
    }
}
