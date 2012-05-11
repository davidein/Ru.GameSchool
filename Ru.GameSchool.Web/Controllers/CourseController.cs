
using System.Web.Mvc;
using Ru.GameSchool.Web.Classes.Helper;
using Ru.GameSchool.Web.Models;


namespace Ru.GameSchool.Web.Controllers
{
    public class CourseController : BaseController
    {
        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                return RedirectToAction("Item", new {id = id.Value});
            }
            else
            {
                return RedirectToAction("NotFound","Home");
            }
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

        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Announcements(int id)
        {
            ViewBag.Course = CourseService.GetCourse(id);
            var announcements = AnnouncementService.GetAnnouncementsByCourseId(id);
            ViewBag.Announcements = announcements;
            ViewBag.CourseId = id; 

            return View();
        }

        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Announcement(int id)
        {
            var announcement = AnnouncementService.GetAnnouncementByAnnouncementId(id);
            ViewBag.Announcement = announcement;

            ViewBag.CourseId = announcement.CourseId; 

            return View();
        }
    }
}
