using System.Web.Mvc;
using Ru.GameSchool.Web.Classes.Helper;
using Ru.GameSchool.Web.Models;


namespace Ru.GameSchool.Web.Controllers
{
    public class CourseController : BaseController
    {
        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Index()
        {
            
            return View();
        }

        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Item(int? id)
        {
            if (id.HasValue)
            {
                var user = MembershipHelper.GetUser();

                var userlevel = CourseService.GetCurrentUserLevel(user.UserInfoId, id.Value);

                return RedirectToAction("Get", "Level", new {id = userlevel});
            }

            return RedirectToAction("NotFound","Home");
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
