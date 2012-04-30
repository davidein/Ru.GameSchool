using System.Linq;
using Ru.GameSchool.DataLayer;
using System.Collections.Generic;

namespace Ru.GameSchool.BusinessLayer.Services
{
    /// <summary>
    /// Dummy class for testing the database
    /// </summary>
    public class UserService : BaseService
    {
        /// <summary>
        /// Gets a user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUser(int userId)
        {
            var user = from x in GameSchoolEntities.Users
                       where x.UserId == userId
                       select x;
            
            return user.FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new System.NotImplementedException();
        }
        public User Login(string userName, string password)
        {
            return null;
        }
        public void DeleteUser(int userId)
        {
            Save();
        }
      
        public void UpdateUser(User u)
        {
            throw new System.NotImplementedException();
        }
    }
}
