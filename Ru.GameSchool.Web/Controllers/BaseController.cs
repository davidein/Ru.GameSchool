using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Ru.GameSchool.BusinessLayer;
using System.Web;
using Ru.GameSchool.BusinessLayer.Services;

namespace Ru.GameSchool.Web.Controllers
{
    public class BaseController : Controller
    {
        private UserService _userService;
        private CourseService _courseService;

        internal UserService UserService 
        { 
            get 
            { 
                _userService = LoadService(_userService, HttpContext); 
                return _userService; 
            } 
        }

        internal CourseService CourseService
        {
            get
            {
                _courseService = LoadService(_courseService, HttpContext);
                return _courseService;
            }
        }

        private static T LoadService<T>(T service, HttpContextBase contextBase) where T : new()
        {
            if (Equals(service, null))
            { 
                service = new T();
            }
            return service;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            
        }
    }
}
