using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.BusinessLayer.Enums
{
    public enum UserType
    {
        Anonymous = 0,
        Student = 1,
        Teacher = 2,
        Admin = 3
    }

    public class UserTypeResolver
    {
        public static UserType Get(string type)
        {
            UserType userType;
            if (Enum.TryParse(type, out userType))
                return userType;

            return UserType.Anonymous;
        }
    }
}
