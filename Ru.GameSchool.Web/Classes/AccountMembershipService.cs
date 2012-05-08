using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Ru.GameSchool.Web.Classes.Interfaces;

namespace Ru.GameSchool.Web.Classes
{
    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", "userName");
            }
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", "password");
            }

            return _provider.ValidateUser(userName, password);
        }
    }
}