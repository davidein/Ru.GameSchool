using System;
using System.Collections.Generic;
using Ru.GameSchool.BusinessLayer.Interfaces;
using Ru.GameSchool.DataLayer.Repository;
using System.Linq;

namespace Ru.GameSchool.BusinessLayer.Services
{
    public class GameService : BaseService, IExternalPointContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <param name="levelId"></param>
        /// <param name="points"></param>
        public void AddPointsToLevel(int userInfoId, int levelId, int points)
        {
            if ((userInfoId = levelId = points) > 0)
            {
                var query = GameSchoolEntities.Points.Where(p => p.UserInfoId == userInfoId &&
                                                             p.LevelId == levelId);
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entity.Points += points;
                    Save();
                }
            }
        }

        /// <summary>
        /// Gets all userinfos instance 
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <param name="levelId"></param>
        /// <returns></returns>
        public int GetPoints(int userInfoId, int levelId)
        {
            // If userinfoid or levelid is smaller then 0 then return 0
            if (userInfoId < 0 || levelId < 0)
            {
                return 0;
            }
            // Get a point instance that has a given levelid and userinfoid
            var query = GameSchoolEntities.Points.Where(p => p.LevelId == levelId &&
                                                             p.UserInfoId == userInfoId);

            var points = query.Select(p => p.Points)
                              .FirstOrDefault();
            return points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public int CalculatePoints(int userInfoId, int courseId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <param name="courseId"></param>
        public void GetPointsComparedToUsers(int userInfoId, int courseId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns a list of the top ranking users for the course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>List of Tuple first item is the rank then the user object.</returns>
        public IEnumerable<Tuple<int, UserInfo>> GetTopTenList(int courseId)
        {
            var query = GameSchoolEntities.Courses.Where(c => c.CourseId == courseId)
                                                  .AsEnumerable();

            var collection = query.Select(x =>
                Tuple.Create<int, UserInfo>(
                    x.Points.Select(z => z.Points)
                            .FirstOrDefault(),
                x.UserInfoes.Select(c => c)
                            .FirstOrDefault()))
                            .OrderBy(x => x.Item2.Points)
                            .Take(10);

            foreach (var item in collection)
            {
                yield return item;
            }
        }
    }
}
