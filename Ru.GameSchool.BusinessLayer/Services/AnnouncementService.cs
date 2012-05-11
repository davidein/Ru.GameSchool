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

            var courses = (from x in GameSchoolEntities.UserInfoes
                           where x.UserInfoId == userInfoId
                           select x).FirstOrDefault().Courses;



            throw new GameSchoolException("Not Implimented Exception");

            //return announcements;
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

    }
}
