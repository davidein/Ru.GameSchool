using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Ru.GameSchool.BusinessLayer;
using System.Web;
using Ru.GameSchool.BusinessLayer.Services;
using Ru.GameSchool.Web.Classes.Helper;

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
                SetupConnectedClasses(_userService);
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
                SetupConnectedClasses(_courseService);
                return _courseService;
            }
        }
        internal LevelService LevelService
        {
            get
            {
                _levelService = LoadService(_levelService, HttpContext);
                SetupConnectedClasses(_levelService);
                return _levelService;
            }
        }
        internal SocialService SocialService
        {
            get
            {
                _socialService = LoadService(_socialService, HttpContext);
                SetupConnectedClasses(_socialService);
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

        private void SetupConnectedClasses(BaseService baseService)
        {
            baseService.ExternalPointContainer = GameService;
            baseService.ExternalNotificationContainer = NotificationService;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            
            if (User.Identity.IsAuthenticated)
            {
                var user = MembershipHelper.GetUser();
                var list = CourseService.GetCoursesByUserInfoId(user.UserInfoId);
                ViewBag.User = user;
                ViewBag.UserCourseList = list.NestedList(3);
                
                
            }
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (User.Identity.IsAuthenticated)
            {
                var user = MembershipHelper.GetUser();
                if (ViewBag.CourseId != null)
                {
                    ViewBag.CourseValue = CourseService.GetCourse(ViewBag.CourseId).Name;
                    ViewBag.GetScoreComparedToUsers = GameService.GetScoreComparedToUsers(user.UserInfoId, ViewBag.CourseId);
                }
            }
        }

    }
}
