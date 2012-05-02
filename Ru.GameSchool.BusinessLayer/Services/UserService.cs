using System.Linq;
using Ru.GameSchool.DataLayer;
using System.Collections.Generic;
using Ru.GameSchool.DataLayer.Repository;

namespace Ru.GameSchool.BusinessLayer.Services
{
    /// <summary>
    /// Service class that abstracts the interraction around the user entity with the data layer.
    /// </summary> 
    public class UserService : BaseService
    {
        /// <summary>
        /// Gets a user by userId
        /// </summary>
        /// <param name="userId">Id of the user to get.</param>
        /// <returns>A new user instance.</returns>
        public UserInfo GetUser(int userId)
        {
            var user = from x in GameSchoolEntities.UserInfoes
                       where x.UserInfoId == userId
                       select x;
            
            return user.FirstOrDefault();
        }

        public IEnumerable<UserInfo> GetUsers()
        {
            var userInfoList = from x in GameSchoolEntities.UserInfoes
                               select x;

            return userInfoList;
        }

        /// <summary>
        /// Confirms that user credentials are authentic
        /// </summary>
        /// <param name="userName">The username reserved for the user to login to GameSchool.</param>
        /// <param name="password">Password to confirm the user is allowed to log into given username.</param>
        /// <returns>A new user instance.</returns>
        public UserInfo Login(string userName, string password)
        {
            if ((string.IsNullOrWhiteSpace(userName)) || (string.IsNullOrWhiteSpace(password)))
            {
                return null;
            }

            var userQuery = from x in GameSchoolEntities.UserInfoes
                       where x.Username == userName
                       select x;

            var userInfo = userQuery.FirstOrDefault();

            //User not found, return null;
            if (userInfo == null)
            {
                return null;
            }

            //User password is incorrect, return null;
            if (userInfo.Password != password)
            {
                return null;
            }


            return userInfo;
        }
      
        public void UpdateUser(UserInfo userInfo)
        {
/*            var items = GameSchoolEntities.UserInfoes.Where(u => u.UserInfoId == userInfo.UserInfoId);

            var item = items.FirstOrDefault();

            if (item != null)
            {
                item. = userInfo;
                Save();
            }*/
        }
    }
}
