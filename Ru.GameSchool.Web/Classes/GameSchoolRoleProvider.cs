using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Ru.GameSchool.BusinessLayer.Enums;
using Ru.GameSchool.BusinessLayer.Services;

namespace Ru.GameSchool.Web.Classes
{
    public class GameSchoolRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            UserService userService = new UserService();

            var user = userService.GetUser(username);

            if (user != null)
            {
                if (user.UserTypeId == (int)UserTypeResolver.Get(roleName))
                { 
                    return true;
                }
            }
            return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            UserService userService = new UserService();

            var user = userService.GetUser(username);

            return new[] {((UserType) user.UserTypeId).ToString()};
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get; set; }
    }
}