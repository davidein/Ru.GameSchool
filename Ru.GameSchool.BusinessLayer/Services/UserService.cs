using System.Linq;
using Ru.GameSchool.DataLayer;
using System.Collections.Generic;

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
            throw new System.NotImplementedException();
        }
        public UserInfo Login(string userName, string password)
        {
            return null;
        }
      
        public void UpdateUser(UserInfo u)
        {
            throw new System.NotImplementedException();
        }
    }
}
