using System.Data.Entity;
using System.Linq;
using Ru.GameSchool.BusinessLayer.Exceptions;
using Ru.GameSchool.DataLayer;
using System.Collections.Generic;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Utilities;
using System;


namespace Ru.GameSchool.BusinessLayer.Services
{
    /// <summary>
    /// Service class that abstracts the interraction around the course entity with the data layer.
    /// </summary> 
    public class CourseService : BaseService
    {
        /// <summary>
        /// Gets a course instance through a parameter of this function and if it isn't null persist it to the database.
        /// </summary>
        /// <param name="course">Instance of a course</param>
        public void CreateCourse(Course course)
        {
            if (course != null)
            {
                course.CreateDateTime = DateTime.Now;
                GameSchoolEntities.Courses.AddObject(course);
                Save();
            }
        }

        public void UpdateCourse(Course course)
        {
            /*var query = GameSchoolEntities.Courses.Where(n => n.CourseId == course.CourseId);

            var courseToUpdate = query.FirstOrDefault();
            courseToUpdate.Name = course.Name;
            courseToUpdate.Description = course.Description;
            courseToUpdate.Identifier = course.Identifier;
            courseToUpdate.CreditAmount = course.CreditAmount;
            courseToUpdate.DepartmentId = course.DepartmentId;
            courseToUpdate.Start = course.Start;
            courseToUpdate.Stop = course.Stop;*/

            Save();
        }

        /// <summary>
        /// Gets all registered Courses
        /// </summary>
        /// <returns>A list of all courses.</returns>
        public IEnumerable<Course> GetCourses()
        {
            var courses = (from x in GameSchoolEntities.Courses
                           select x).Include("Levels");

            return courses;
        }

        public IEnumerable<Course> GetCoursesByUserInfoId(int userInfoId)
        {
            if (userInfoId <= 0)
            {
                return null;
            }

            var courses = (from x in GameSchoolEntities.UserInfoes
                           where x.UserInfoId == userInfoId
                           select x).FirstOrDefault().Courses;

            var filteredCourses = from y in courses
                                  where y.Start <= DateTime.Now && y.Stop >= DateTime.Now
                                  select y;

            return filteredCourses;
        }


        /// <summary>
        /// Gets all registered Departments
        /// </summary>
        /// <returns>A list of all Departments.</returns>
        public IEnumerable<Department> GetDepartments()
        {
            var departments = from x in GameSchoolEntities.Departments
                              select x;

            return departments;
        }

        public string AddUserToCourse(int userInfoId, int courseId)
        {
            if (userInfoId > 0 && courseId > 0)
            {
                var userQuery = GameSchoolEntities.UserInfoes.Where(u => u.UserInfoId == userInfoId);
                var courseQuery = GameSchoolEntities.Courses.Where(c => c.CourseId == courseId).FirstOrDefault();

                if(userQuery == null)
                    throw new GameSchoolException(string.Format("User not found. UserInfoId = {0}", userInfoId));

                if (courseQuery == null)
                    throw new GameSchoolException(string.Format("Course not found. CourseId = {0}", courseId));

                var isInCourse = GetCoursesByUserInfoIdAndCourseId(userInfoId, courseId);

                if (isInCourse.Count() > 0) //User Already in course
                    return string.Format("User is already registered in course! Returns {0} results.", isInCourse.Count());

                courseQuery.UserInfoes.Add(userQuery.FirstOrDefault());
                Save();

                if (ExternalNotificationContainer != null)
                    ExternalNotificationContainer.CreateNotification(
                        string.Format("Þú hefur verið skráður í {0}", courseQuery.Name),
                        string.Format("/Course/Item/{0}", courseQuery.CourseId), userInfoId);

                return "User added to course!";
            }

            return "Invalid userId or CourseId";
        }

        /// <summary>
        /// Gets a courseGrade instance through a parameter of this function and if it isn't null persist it to the database.
        /// </summary>
        /// <param name="userInfo">Instance of a courseGrade</param>
        public void AddCourseGrade(CourseGrade courseGrade)
        {
            if (courseGrade != null)
            {
                GameSchoolEntities.CourseGrades.AddObject(courseGrade);
                Save();
            }
            throw new System.NotImplementedException();
        }

        public void UpdateCourseGrade(CourseGrade courseGrade)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets a list of all grades in a course
        /// </summary>
        /// <param name="courseId">Id of the course to get grades for.</param>
        /// <returns>A list of all grades in a given course.</returns>
        public IEnumerable<CourseGrade> GetCourseGrades(int courseId)
        {
            var courseGrades = from x in GameSchoolEntities.CourseGrades
                               where x.CourseId == courseId
                               select x;


            return courseGrades;
        }

        /// <summary>
        /// Gets the grade of a certain user in a course
        /// </summary>
        /// <param name="courseId">Id of the Course to get the grade for.</param>
        /// <param name="userInfo">Id of the User to get the grade for.</param>
        /// <returns>CourseGrade entity of the user for the given course.</returns>
        public CourseGrade GetCourseGradeByCourseIdAndUserInfoId(int courseId, int userInfoId)
        {
            var courseGrade = from x in GameSchoolEntities.CourseGrades
                              where x.CourseId == courseId && x.UserInfoId == userInfoId
                              select x;


            return courseGrade.FirstOrDefault();
        }

        public void GetCurrentUserLevel()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets a Course entity by CourseId
        /// </summary>
        /// <param name="courseId">Id of the course to get.</param>
        /// <returns>A course entity.</returns>
        public Course GetCourse(int courseId)
        {
            var course = from x in GameSchoolEntities.Courses
                         where x.CourseId == courseId
                         select x;

            return course.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public IEnumerable<Course> GetCoursesByUserInfoIdAndCourseId(int userInfoId, int courseId)
        {
            if (0 > userInfoId | 0 > courseId)
            {
                return null;
            }

            var course = from x in GameSchoolEntities.Courses
                       where x.CourseId == courseId
                           select x;

            var userCourse = from x in course
                       where x.UserInfoes.Where(p => p.UserInfoId == userInfoId).Count() > 0
                       select x;

            /*var query = GameSchoolEntities.UserInfoes.Join(GameSchoolEntities.Courses,
                                                           u => u.DepartmentId, c => c.DepartmentId,
                                                           (u, c) => new
                                                                         {
                                                                             u,
                                                                             c
                                                                         }).Where(
                                                                             x =>
                                                                             x.c.CourseId == courseId &&
                                                                             x.u.UserInfoId == userInfoId)
                                                                           .Select(m => m.c);*/

            return userCourse;
        }

        /// <summary>
        /// Gets current level by UserInfoId and CourseId
        /// </summary>
        /// <param name="courseId">Id of the Course to get current level for.</param>
        /// <param name="userInfoId">Id of the User to get current level for.</param>
        /// <returns>Current level of the user for the given course.</returns>
        public int CurrentUserLevel(int userInfoId, int courseId)
        {
            var course = (from x in GameSchoolEntities.Courses
                          where x.CourseId == courseId
                          select x).SingleOrDefault();

            var levels = course.Levels;

            Level first = null;
            foreach (Level y in levels)
            {
                if (y.Stop > DateTime.Now)
                {
                    first = y;
                    break;
                }
            }

            return first.LevelId;
        }

    }
}
