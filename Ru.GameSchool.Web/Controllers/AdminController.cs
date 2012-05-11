using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.Web.Models;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Web.Classes.Helper;

namespace Ru.GameSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {

            ViewBag.Title = "Forsíða umsjónarmanns";
            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Users = UserService.GetUsers();
            ViewBag.Title = "Listi yfir notendur";

            return View();
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public ActionResult Search(SearchViewModel model )
        {
            if (ModelState.IsValid)
            {
                
            }
            return null;
        }

        [HttpGet]
        public ActionResult UserEdit(int? id)
        {
            ViewBag.Departments = CourseService.GetDepartments();
            ViewBag.UserStatus = UserService.GetUserStatuses();
            ViewBag.UserTypes = UserService.GetUserTypes();
            ViewBag.Title = "Skráning notenda";

            if (id.HasValue)
            {
                var user = UserService.GetUser((int)id);
                if (user != null)
                {
                    var model = new UserInfo();
                    model.Username = user.Username;
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
            ViewBag.Title = "Skráning notenda";
            ModelState.Remove("Password");
            
            
            if (ModelState.IsValid)
            {
                //Update existing user
                if (id.HasValue)
                {
                    var user = UserService.GetUser(id.Value);
                    if (TryUpdateModel(user))
                    {
                        UserService.UpdateUser(user);
                        ViewBag.SuccessMessage = "Upplýsingar um notenda hafa verið uppfærðar";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Náði ekki að skrá/uppfæra upplýsingar! Lagfærðu villur og reyndur aftur.";
                    }
                }
                else //Insert new user
                {
                    
                    UserService.CreateUser(model);
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
            ViewBag.Title = "Listi yfir námskeið";


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
                    ViewBag.Title = "Breyta námskeiði";
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
            ViewBag.Title = "Nýtt námskeið";
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
                    ViewBag.Title = "Breyta námskeiði";
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
                    ViewBag.Title = "Nýtt námskeið";
                    CourseService.CreateCourse(model);
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
            var list = UserService.GetUsers();
            ViewBag.Users = list.NestedList(6);
            ViewBag.Title = "Skrá notenda í Námskeið";

            var courses = CourseService.GetCourses();
            ViewBag.Courses = courses.NestedList(4);

            return View();
        }


    }
}
