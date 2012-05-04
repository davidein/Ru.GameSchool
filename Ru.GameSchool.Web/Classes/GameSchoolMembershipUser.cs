using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Ru.GameSchool.Web.Classes
{
    public class GameSchoolMembershipUser : MembershipUser
    {
        public string Fullname { get; set; }

        public GameSchoolMembershipUser(string providerName, string name, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDateTime, DateTime lastAcctivityDate, DateTime lastPasswordChangeDate, DateTime lastLockoutDateTime, string fullname, int userRoleId)
            : base(providerName, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDateTime, lastAcctivityDate, lastPasswordChangeDate, lastLockoutDateTime)
        {
            Fullname = fullname;
        }
    }
}