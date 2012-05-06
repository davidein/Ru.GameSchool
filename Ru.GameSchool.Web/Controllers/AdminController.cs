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

        public ActionResult UserEdit(int? id)
        {
            ViewBag.Departments = CourseService.GetDepartments();
            ViewBag.UserStatus = UserService.GetUserStatuses();
            ViewBag.UserTypes = UserService.GetUserTypes();

            if (id.HasValue)
            {
                var user = UserService.GetUser((int)id);
                if (user != null)
                {
                    var model = new Ru.GameSchool.DataLayer.Repository.UserInfo();
                    model.Username = user.Username;
                    model.Password = "#######";
                    model.Email = user.Email;
                    model.Fullname = user.Fullname;
                    model.DepartmentId = user.DepartmentId;
                    model.StatusId = user.StatusId;
                    model.UserTypeId = user.UserTypeId;

                    return View(model);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult UserEdit(UserInfo model, int? id)
        {
            ViewBag.Departments = CourseService.GetDepartments();
            ViewBag.UserStatus = UserService.GetUserStatuses();
            ViewBag.UserTypes = UserService.GetUserTypes();

            if (ModelState.IsValid)
            {
                //Update existing user
                if (id.HasValue)
                {
                    var user = UserService.GetUser((int)id);
                    user.Username = model.Username;
                    user.Email = model.Email;
                    user.Fullname = model.Fullname;
                    user.DepartmentId = model.DepartmentId;
                    user.StatusId = model.StatusId;
                    user.UserTypeId = model.UserTypeId;

                    UserService.UpdateUser(user);
                    ViewBag.SuccessMessage = "Upplýsingar um notenda hafa verið uppfærðar";
                }
                else //Insert new user
                {
                    var user = new UserInfo();

                    user.Username = model.Username;
                    user.Password = model.Password;
                    user.Email = model.Email;
                    user.Fullname = model.Fullname;
                    user.DepartmentId = model.DepartmentId;
                    user.StatusId = model.StatusId;
                    user.UserTypeId = model.UserTypeId;
                    user.CreateDateTime = DateTime.Now;

                    UserService.CreateUser(user);
                    ViewBag.SuccessMessage = "Nýr notandi hefur verið skráður í kerfið. Mundu að skrá notendann í námskeið.";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Náði ekki að skrá/uppfæra upplýsingar! Lagfærðu villur og reyndur aftur.";
            }

            return View(model);
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

            if (ModelState.IsValid)
            {
                if (id.HasValue) //Update existing Course
                {
                    var course = CourseService.GetCourse(id.Value);
                    if (course != null)
                    {
                        if (TryUpdateModel(course))
                        {
                            CourseService.UpdateCourse(course);
                            ViewBag.SuccessMessage = "Upplýsingar um námskeið uppfærðar.";
                            return View(model);
                        }
                    }

                    ViewBag.ErrorMessage = "Ekki tókst að uppfæra námskeið!";
                }
                else //Insert new Course
                {
                    var course = new Ru.GameSchool.DataLayer.Repository.Course();
                    course.Name = model.Name;
                    course.Description = model.Description;
                    course.Identifier = model.Identifier;
                    course.CreditAmount = model.CreditAmount;
                    course.Start = model.Start;
                    course.Stop = model.Stop;
                    course.DepartmentId = model.DepartmentId;
                    course.CreateDateTime = DateTime.Now;
                    

                    CourseService.CreateCourse(course);
                    ViewBag.SuccessMessage = "Nýtt námskeið skráð! Mundu að skrá nemendur og kennara á námskeið.";
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Náði ekki að skrá/uppfæra upplýsingar! Lagfærðu villur og reyndur aftur.";
            }

            return View(model);
        }

        public ActionResult UserCourse()
        {

            return View();
        }


    }
}
