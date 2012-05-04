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
        private GameService _gameService;
        private LevelService _levelService;
        private SocialService _socialService;
        private NotificationService _notificationService;


        internal UserService UserService 
        { 
            get 
            { 
                _userService = LoadService(_userService, HttpContext); 
                return _userService; 
            } 
        }
        internal GameService GameService
        {
            get 
            { 
                _gameService = LoadService(_gameService, HttpContext);
                return _gameService;
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
        internal LevelService LevelService
        {
            get
            {
                _levelService = LoadService(_levelService, HttpContext);
                return _levelService;
            }
        }
        internal SocialService SocialService
        {
            get
            {
                _socialService = LoadService(_socialService, HttpContext);
                return _socialService;
            }
        }
        internal NotificationService NotificationService
        {
            get
            {
                _notificationService = LoadService(_notificationService, HttpContext);
                return _notificationService;
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
