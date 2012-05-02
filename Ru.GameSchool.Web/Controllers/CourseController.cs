﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.DataLayer;
using Ru.GameSchool.Web.Models;

namespace Ru.GameSchool.Web.Controllers
{
    public class CourseController : BaseController
    {

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Item(int id)
        {
            var course = CourseService.GetCourse(id);
            ViewBag.Course = course;

            ViewBag.Title = course.Name;

            return View();
        }

        
        //TODO: Impliment Role Authentication
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(CourseModel model)
        {

            return View(model);
        }

        //TODO: Create Post ActionResult


        //TODO: Impliment Role Authentication
        public ActionResult Edit(int id)
        {

            return View();
        }

        [HttpPost]
        public ActionResult Edit(CourseModel model, int id)
        {

            return View();
        }

        public ActionResult LeaderBoard(int id)
        {

            return View();
        }
    }
}
