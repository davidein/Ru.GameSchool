using Ru.GameSchool.DataLayer;
using System.Collections.Generic;
using System.Linq;
using Ru.GameSchool.DataLayer.Repository;

namespace Ru.GameSchool.BusinessLayer.Services
{
    /// <summary>
    /// Service class that abstracts the interraction around the course entity with the data layer.
    /// </summary> 
    public class CourseService : BaseService
    {
        public void CreateCourse(Course course)
        {
            if (course != null)
            {
                GameSchoolEntities.Courses.AddObject(course);
                Save();
            }
        }

        public void UpdateCourse(Course course)
        {
            if (course != null)
            {

            }
        }

        public IEnumerable<Course> GetCourses()
        {
            return GameSchoolEntities.Courses;
        }

        public void CreateUserToCourse(UserInfo user, Course course)
        {
            if (user != null && course != null)
            {
                // TODO: Finish initializing
                Save();
            }
        }

        public void CreateCourseGrade(CourseGrade courseGrade)
        {
            if (courseGrade != null)
            {
                GameSchoolEntities.CourseGrades.AddObject(courseGrade);
                Save();
            }
        }

        public void UpdateCourseGrade(CourseGrade courseGrade)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CourseGrade> GetCourseGrades()
        {
            return GameSchoolEntities.CourseGrades;
        }

        public void GetCurrentUserLevel()
        {
            
        }

        public void GetCourse()
        {
            throw new System.NotImplementedException();
        }
    }
}
