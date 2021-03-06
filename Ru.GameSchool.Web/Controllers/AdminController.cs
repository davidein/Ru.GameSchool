﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.Web.Models;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Web.Classes.Helper;
using Ru.GameSchool.BusinessLayer.Enums;
using UserType = Ru.GameSchool.BusinessLayer.Enums.UserType;

namespace Ru.GameSchool.Web.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View("Search");
        }

        public ActionResult Users()
        {
            ViewBag.Users = UserService.GetUsers();
            ViewBag.Title = "Listi yfir notendur";

            return View();
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {
            IEnumerable<UserInfo> userSearchResults = null;
            IEnumerable<Course> courseSearchResults = null;
            if (collection != null)
            {
                var searchType = collection["searchtype"];
                var userType = collection["usertype"];
                var search = collection["search"];

                if (searchType == "Notandi") // verið að leita að notendum
                {
                    var userT = userType == "Nemandi"
                                    ? Ru.GameSchool.BusinessLayer.Enums.UserType.Student
                                    : userType == "Kennari"
                                          ? Ru.GameSchool.BusinessLayer.Enums.UserType.Teacher
                                          : userType == "Umsjónarmaður"
                                                ? Ru.GameSchool.BusinessLayer.Enums.UserType.Admin
                                                : Ru.GameSchool.BusinessLayer.Enums.UserType.Anonymous;

                    userSearchResults = UserService.Search(search, userT);
                    return View("UserSearchResult", userSearchResults);
                }
                else // verið að leita að námskeiðum
                {
                    courseSearchResults = CourseService.Search(search);
                    return View("CourseSearchResult", courseSearchResults);
                }
            }
            return RedirectToAction("NotFound", "Home");
        }

        [HttpGet]
        public ActionResult UserSearchResult()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CourseSearchResult()
        {
            return View();
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
            ViewBag.Title = "Breyta notenda";
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
                    return RedirectToAction("Index");
                    //UserService.CreateUser(model);
                    //ViewBag.SuccessMessage = "Nýr notandi hefur verið skráður í kerfið. Mundu að skrá notendann í námskeið.";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Náði ekki að skrá/uppfæra upplýsingar! Lagfærðu villur og reyndur aftur.";
            }

            return View(model);
        }



        [HttpGet]
        public ActionResult UserCreate()
        {
            ViewBag.Departments = CourseService.GetDepartments();
            ViewBag.UserStatus = UserService.GetUserStatuses();
            ViewBag.UserTypes = UserService.GetUserTypes();
            ViewBag.Title = "Skráning notenda";

            return View();
        }

        [HttpPost]
        public ActionResult UserCreate(UserInfo model, int? id)
        {
            ViewBag.Departments = CourseService.GetDepartments();
            ViewBag.UserStatus = UserService.GetUserStatuses();
            ViewBag.UserTypes = UserService.GetUserTypes();
            ViewBag.Title = "Skrá notenda";

            if (ModelState.IsValid)
            {
                UserService.CreateUser(model);
                ViewBag.SuccessMessage = "Nýr notandi hefur verið skráður í kerfið. Mundu að skrá notendann í námskeið.";
                return View("Search");
            }
            else
            {
                ViewBag.ErrorMessage = "Náði ekki að skrá upplýsingar! Lagfærðu villur og reyndur aftur.";
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
                ViewBag.Courses = course;
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
