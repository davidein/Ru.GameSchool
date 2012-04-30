using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Ru.GameSchool.BusinessLayer;
using System.Web;

namespace Ru.GameSchool.Web.Controllers
{
    public class BaseController : Controller
    {

        private UserService _userService;
        internal UserService UserService { get { _userService = LoadService(_userService, HttpContext); return _userService; } }

        private T LoadService<T>(T service, HttpContextBase contextBase) where T : new()
        {
            if (Equals(service, null))
            {
                service = new T();
            }

            return service;
        }
    }
}
