using System.Linq;
using Ru.GameSchool.DataLayer;

namespace Ru.GameSchool.BusinessLayer
{
    /// <summary>
    /// Dummy class for testing the database
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Gets a user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserByUserId(int userId)
        {
            var gameSchoolEntities = new GameSchoolEntities();

            var user = from x in gameSchoolEntities.Users
                       where x.UserId == userId
                       select x;

            return user.FirstOrDefault();
        }
    }
}
