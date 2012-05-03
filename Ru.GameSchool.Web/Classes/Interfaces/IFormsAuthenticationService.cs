using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.Web.Classes.Interfaces
{
    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }
}
