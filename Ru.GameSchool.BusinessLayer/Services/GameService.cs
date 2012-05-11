using System;
using System.Collections.Generic;
using Ru.GameSchool.BusinessLayer.Interfaces;
using Ru.GameSchool.DataLayer.Repository;
using System.Linq;

namespace Ru.GameSchool.BusinessLayer.Services
{
    public class UserAndPoints
    {
        public UserInfo UserInfo { get; set; }
        public int Points { get; set; }
    }
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

        public int GetPointsByUserInfoIdAndCourseId(int userInfoId, int courseId)
        {
            // If userinfoid or levelid is smaller then 0 then return 0
            if (userInfoId <= 0 || courseId <= 0)
            {
                return 0;
            }
            // Get a point instance that has a given levelid and userinfoid
            var query = GameSchoolEntities.Points.Where(p => p.CourseId == courseId &&
                                                             p.UserInfoId == userInfoId);
            int points = 0;

            if (query.Count() > 0)
                points = query.Sum(x => x.Points);


            return points;
        }

        public int GetTopPoints(int courseId)
        {
            if (courseId <= 0)
            {
                return 0;
            }

            var query = GameSchoolEntities.Points.Where(p => p.CourseId == courseId);

            var points = query.Max(x => x.Points);

            return points;
        }

        /// <summary>
        /// Fall sem skilar tuple af þínum stigum, þínu sæti, hvað þú þarft mörg stig til að komast sæti ofar og hvað keppandinn á eftir þér þarf mörg stig til að koamst yfir þig
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public Tuple<int, int, int, int> GetScoreComparedToUsers(int userInfoId, int courseId)
        {
            if (0 > userInfoId)
            {
                return null;
            }

            var usersAndTheirPoints = GetPointsByAndNotUserInfoIdCourseId(courseId).OrderByDescending(k => k.Points);
            var yourScore =
                usersAndTheirPoints.Where(s => s.UserInfo.UserInfoId == userInfoId).Select(s => s.Points).FirstOrDefault();

            var lowerPosition = -1;
            var upperPosition = -1;
            var position = 0;
            foreach (var usersAndTheirPoint in usersAndTheirPoints)
            {
                ++position;
                if (usersAndTheirPoint.UserInfo.UserInfoId == userInfoId)
                {
                    break;
                }
                upperPosition = usersAndTheirPoint.Points;
            }

            if (usersAndTheirPoints.Count() > position)
            {
                lowerPosition = usersAndTheirPoints.ElementAt(position).Points;
            }


            return Tuple.Create(yourScore, position, upperPosition, lowerPosition);
        }



        //private int GetUserPositionInGame(int userInfoId, int courseId)
        //{
        //    if (0 >= userInfoId | 0 >= courseId)
        //    {
        //        return 0;
        //    }

        //    var totalUsers = GameSchoolEntities.Points.Count(s => s.CourseId == courseId && s.UserInfoId != userInfoId);
        //    var allUsersWithPointsInThisCourse = GetPointsByAndNotUserInfoIdCourseId(courseId, userInfoId);
        //    int position = 0;

        //    var query = GameSchoolEntities.Points.Where(p => p.UserInfoId == userInfoId && p.CourseId == courseId);

        //    var userPoints = query.Any() ? query.Sum(s => s.Points) : 0;

        //    for (int i = 0; i < totalUsers; i++)
        //    {
        //        if (userPoints > allUsersWithPointsInThisCourse.ElementAtOrDefault(i))
        //        {
        //            position++;
        //        }
        //    }

        //    return (totalUsers - position) + 1;
        //}

        private IEnumerable<UserAndPoints> GetPointsByAndNotUserInfoIdCourseId(int courseId)
        {
            return from x in GameSchoolEntities.Points.Where(s => s.CourseId == courseId)
                   group x by x.UserInfo
                       into g
                       select new UserAndPoints
                                  {
                                      UserInfo = g.Key,
                                      Points = g.Sum(m => m.Points)
                                  };
        }

        /// <summary>
        /// Handles point calculations based on grade.
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="pointPerGrade"></param>
        /// <returns></returns>
        public int CalculatePointsByGrade(double grade, int pointPerGrade)
        {
            var result = grade * pointPerGrade;

            return (int)result;
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
