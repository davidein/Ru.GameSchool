using System;
using System.Collections.Generic;
using Ru.GameSchool.BusinessLayer.Interfaces;
using Ru.GameSchool.DataLayer.Repository;
using System.Linq;

namespace Ru.GameSchool.BusinessLayer.Services
{
    public class GameService : BaseService, IExternalPointContainer
    {
        // TODO: Validate ids and point
        public void AddPointsToLevel(int userInfoId, int levelId, int points)
        {
            var query = GameSchoolEntities.Points.Where(p => p.UserInfoId == userInfoId &&
                                                             p.LevelId == levelId);
            var entity = query.FirstOrDefault();
            entity.Points += points;
            Save();
        }
        // TODO: Validate ids
        public int GetPoints(int userInfoId, int levelId)
        {
            var query = GameSchoolEntities.Points.Where(p => p.LevelId == levelId && 
                                                             p.UserInfoId == userInfoId);

            var points = query.Select(p => p.Points)
                              .FirstOrDefault();
            return points;
        }

        public void CalculatePoints()
        {
            throw new System.NotImplementedException();
        }


        public void GetPointsComparedToUsers(int userId, int courseId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns a list of the top ranking users for the course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>List of Tuple first item is the rank then the user object.</returns>
        public IEnumerable<Tuple<int,UserInfo>> GetTopTenList( int courseId )
        {
            throw new System.NotImplementedException();
        }
    }
}
