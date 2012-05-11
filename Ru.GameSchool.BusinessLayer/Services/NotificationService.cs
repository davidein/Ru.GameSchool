using System;
using System.Collections.Generic;
using Ru.GameSchool.BusinessLayer.Exceptions;
using Ru.GameSchool.BusinessLayer.Interfaces;
using Ru.GameSchool.DataLayer.Repository;
using System.Linq;

namespace Ru.GameSchool.BusinessLayer.Services
{
    /// <summary>
    /// Service class that abstracts the interraction around the notifications entity with the data layer.
    /// </summary> 
    public class NotificationService : BaseService, IExternalNotificationContainer
    {
        /// <summary>
        /// Create a new user notification.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        /// <param name="userInfoId"></param>
        public void CreateNotification(string text, string url, int userInfoId)
        {
            if (!string.IsNullOrEmpty(text) && userInfoId > 0)
            {
                var notification = new Notification();

                notification.Description = text;
                notification.IsRead = false;
                notification.UserInfoId = userInfoId;
                notification.Url = url;
                notification.CreateDateTime = DateTime.Now;

                GameSchoolEntities.Notifications.AddObject(notification);
                Save();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        public void UpdateNotification(Notification notification)
        {
            if (notification != null)
            {
                var query = GameSchoolEntities.Notifications.Where(n => n.NotificationId == notification.NotificationId);

                var notificationToUpdate = query.FirstOrDefault();
                notificationToUpdate.Description = notification.Description;
                notificationToUpdate.IsRead = notification.IsRead;
                notificationToUpdate.Url = notification.Url;
                Save();
            }
        }
        /// <summary>
        /// Returns a collection of notification objects that are related to a given userinfo instance id, if the userinfoid is equal to or larger then 1.
        /// </summary>
        /// <param name="userInfoId">Integer value to get notifications</param>
        /// <returns></returns>
        public IEnumerable<Notification> GetNotifications(int userInfoId)
        {
            if (userInfoId <= 0)
                return null;

            if (GameSchoolEntities.UserInfoes.Where(x => x.UserInfoId == userInfoId).Count() != 1)
                throw new GameSchoolException(string.Format("User does not exist. UserInfoId = {0}", userInfoId));

            var list = GameSchoolEntities.Notifications.Where(n => n.UserInfoId == userInfoId);

            return list.OrderByDescending(x=>x.NotificationId);
        }

        /// <summary>
        /// Clear notifications
        /// </summary>
        /// <param name="userInfoId"></param>
        public void ClearNotifications(int userInfoId)
        {
            if (userInfoId <= 0)
                return;

            var list = GameSchoolEntities.Notifications.Where(x => x.UserInfoId == userInfoId && !x.IsRead);

            foreach (var notification in list)
            {
                notification.IsRead = true;
            }
            Save();
        }
    }
}
