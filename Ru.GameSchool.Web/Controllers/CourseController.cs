using System.Web.Mvc;
using Ru.GameSchool.Web.Models;


namespace Ru.GameSchool.Web.Controllers
{
    public class CourseController : BaseController
    {
        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Index()
        {
            ViewBag.Courses = CourseService.GetCourses();
            return View();
        }

        [Authorize(Roles = "Student")]
        public ActionResult Item(int id)
        {
            var course = CourseService.GetCourse(id);
            ViewBag.Course = course;

            ViewBag.Title = course.Name;

            return View();
        }


        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {

            return View();
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Create(CourseModel model)
        {

            return View(model);
        }

        //TODO: Create Post ActionResult


        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int id)
        {

            return View();
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Edit(CourseModel model, int id)
        {

            return View();
        }

        [Authorize(Roles = "Student")]
        public ActionResult LeaderBoard(int id)
        {

            return View();
        }
    }
}
