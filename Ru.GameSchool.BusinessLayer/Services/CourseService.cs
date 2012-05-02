using Ru.GameSchool.DataLayer;
using System.Collections.Generic;
using Ru.GameSchool.DataLayer.Repository;

namespace Ru.GameSchool.BusinessLayer.Services
{
    /// <summary>
    /// Service class that abstracts the interraction around the course entity with the data layer.
    /// </summary> 
    public class CourseService : BaseService
    {
        public void AddCourse(Course course)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCourse(Course course)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Course> GetCourses()
        {
            throw new System.NotImplementedException();
        }

        public void AddUserToCourse(UserInfo user, Course course)
        {
            throw new System.NotImplementedException();
        }

        public void AddCourseGrade(CourseGrade courseGrade)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCourseGrade(CourseGrade courseGrade)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CourseGrade> GetCourseGrades()
        {
            throw new System.NotImplementedException();
        }

        public void GetCurrentUserLevel()
        {
            throw new System.NotImplementedException();
        }

        public Course GetCourse(int courseId)
        {
            throw new System.NotImplementedException();
        }
    }
}
