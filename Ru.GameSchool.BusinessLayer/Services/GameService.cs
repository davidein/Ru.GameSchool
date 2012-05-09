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
        /// <param name="description"></param>
        public void AddPointsToLevel(int userInfoId, int levelId, int points, string description)
        {
            if (userInfoId > 0 && levelId > 0 && points > 0)
            {
                var query = GameSchoolEntities.Levels.Where(x => x.LevelId == levelId);

                var level = query.FirstOrDefault();

                var point = new Point();
                point.LevelId = levelId;
                point.UserInfoId = userInfoId;
                point.Description = description;
                point.Points = points;

                if (level != null)
                {
                    point.CourseId = level.CourseId;
                }

                GameSchoolEntities.Points.AddObject(point);
                Save();
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
            if (userInfoId <= 0 || levelId <= 0)
            {
                return 0;
            }
            // Get a point instance that has a given levelid and userinfoid
            var query = GameSchoolEntities.Points.Where(p => p.LevelId == levelId &&
                                                             p.UserInfoId == userInfoId);

            var points = query.Sum(x => x.Points);

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
            if (0 > courseId)
            {
                return null;
            }
            // Selecta course
            var query = GameSchoolEntities.Courses.Where(c => c.CourseId == courseId)
                                                  .AsEnumerable();

            // Fá collection af points miðað við courseid
            var queryPoints = query.SelectMany(x => x.Points);

            // Lista af groupby af ints og userinfo
            var userQuery = queryPoints.GroupBy(x => x.UserInfo);

            // Búa til lista sem er skilað
            var list = new List<Tuple<int, UserInfo>>();

            foreach (var item in userQuery)
            {
                var sum = item.Key.Points.Select(c => c.Points).Sum();

                var tuple = new Tuple<int, UserInfo>(sum, item.Select(x => x.UserInfo).FirstOrDefault());
                list.Add(tuple);
            }

            var usersWithTopTen = list.OrderByDescending(x => x.Item1).Take(10);

            return usersWithTopTen;
        }

        public IEnumerable<Tuple<int, UserInfo>> GetTopTenList()
        {
            // Selecta course
            var query = GameSchoolEntities.Courses;

            // Fá collection af points miðað við courseid
            var queryPoints = query.SelectMany(x => x.Points);

            // Lista af groupby af ints og userinfo
            var userQuery = queryPoints.GroupBy(x => x.UserInfo);

            // Búa til lista sem er skilað
            var list = new List<Tuple<int, UserInfo>>();

            foreach (var item in userQuery)
            {
                var sum = item.Key.Points.Select(c => c.Points).Sum();

                var tuple = new Tuple<int, UserInfo>(sum, item.Select(x => x.UserInfo).FirstOrDefault());
                list.Add(tuple);
            }

            var usersWithTopTen = list.OrderByDescending(x => x.Item1).Take(10);

            return usersWithTopTen;
        }
    }
}
