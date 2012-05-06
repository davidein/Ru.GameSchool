using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.Web.Models;
using Ru.GameSchool.DataLayer.Repository;

namespace Ru.GameSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {


            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Users = UserService.GetUsers();

            return View();
        }

        public ActionResult User(int? id)
        {
            ViewBag.Departments = CourseService.GetDepartments();
            ViewBag.UserStatus = UserService.GetUserStatuses();
            ViewBag.UserTypes = UserService.GetUserTypes();

            return View();
        }

        public ActionResult User(UserInfo model, int? id)
        {
            return View();
        }

        public ActionResult Courses()
        {
            ViewBag.Courses = CourseService.GetCourses();


            return View();
        }

        public ActionResult Course(int? id)
        {
            ViewBag.Departments = CourseService.GetDepartments();

            if (id.HasValue)
            {
                var course = CourseService.GetCourse((int)id);
                if (course != null)
                {
                    var model = new Course();
                    model.Name = course.Name;
                    model.Description = course.Description;
                    model.Identifier = course.Identifier;
                    model.CreditAmount = course.CreditAmount;
                    model.Start = course.Start;
                    model.Stop = course.Stop;
                    model.DepartmentId = course.DepartmentId;

                    return View(model);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Course(Course model, int? id)
        {
            ViewBag.Departments = CourseService.GetDepartments();

            return View();
        }

        public ActionResult CourseRegisterUser(int id)
        {
            return View();
        }


    }
}
