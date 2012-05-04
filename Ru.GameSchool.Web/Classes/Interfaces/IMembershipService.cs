using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.Web.Classes.Interfaces
{
    public interface IMembershipService
    {
        bool ValidateUser(string userName, string password);
    }
}
