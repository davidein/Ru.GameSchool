using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.BusinessLayer.Exceptions;

namespace Ru.GameSchool.BusinessLayer.Services
{
    public class AnnouncementService : BaseService
    {
        public IEnumerable<Announcement> GetAnnouncementsByUserInfoId(int userInfoId)
        {
            if (userInfoId <= 0)
            {
                return null;
            }

            /*var courses = (from x in GameSchoolEntities.UserInfoes
                           where x.UserInfoId == userInfoId
                           select x).FirstOrDefault().Courses;*/

            var announcements = from x in GameSchoolEntities.Announcements
                                where x.Course.UserInfoes.Where(u => u.UserInfoId == userInfoId).Count() > 0
                                select x;

            announcements = announcements.OrderByDescending(d => d.DisplayDateTime);

            return announcements;
        }

        public IEnumerable<Announcement> GetAnnouncementsByCourseId(int courseId)
        {
            var announcements = (from x in GameSchoolEntities.Announcements
                                where x.CourseId == courseId && x.DisplayDateTime <= DateTime.Now
                                select x);

            return announcements.OrderByDescending(x => x.DisplayDateTime).Take(5);
        }

        public IEnumerable<Announcement> GetAnnouncementsByLevelId(int levelId)
        {
            var announcements = from x in GameSchoolEntities.Announcements
                                where x.LevelId == levelId && x.DisplayDateTime <= DateTime.Now
                                select x;

            return announcements.OrderByDescending(x => x.DisplayDateTime).Take(5);
        }

        public IEnumerable<Announcement> GetAnnouncementsOwnedByUserInfoId(int userInfoId)
        {
            var announcements = from x in GameSchoolEntities.Announcements
                                where x.CreatedByUserInfoId == userInfoId
                                select x;

            return announcements.OrderByDescending(x => x.DisplayDateTime);
        }

        public Announcement GetAnnouncementByAnnouncementId(int announcementId)
        {
            var announcement = (from x in GameSchoolEntities.Announcements
                                where x.AnnouncementId == announcementId && x.DisplayDateTime <= DateTime.Now
                                select x).FirstOrDefault();

            return announcement;
        }

        public void CreateAnnouncement(Announcement announcement, int userInfoId)
        {
            var user = GameSchoolEntities.UserInfoes.Where(u => u.UserInfoId == userInfoId).SingleOrDefault();

            if (user == null)
                throw new GameSchoolException(string.Format("User does not exist. UserInfoId = {0}", userInfoId));

            announcement.CreateDateTime = DateTime.Now;
            announcement.CreatedByUserInfoId = user.UserInfoId;
            announcement.CreatedBy = user.Username;

            GameSchoolEntities.Announcements.AddObject(announcement);

            Save();
        }

        public void UpdateAnnouncement(Announcement announcement, int userInfoId)
        {
            var user = GameSchoolEntities.UserInfoes.Where(u => u.UserInfoId == userInfoId).SingleOrDefault();

            if (user == null)
                throw new GameSchoolException(string.Format("User does not exist. UserInfoId = {0}", userInfoId));

            announcement.UpdateDateTime = DateTime.Now;
            announcement.UpdatedBy = user.Username;

            Save();
        }
    }
}
