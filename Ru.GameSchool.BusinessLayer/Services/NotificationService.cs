using System;
using System.Collections.Generic;
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
        /// 
        /// </summary>
        /// <param name="notification"></param>
        public void CreateNotification(Notification notification)
        {
            if (notification != null)
            {
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
                // TODO: Confirm update function works
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
            return userInfoId > 0 ? GameSchoolEntities.Notifications.Where(n => n.UserInfoId == userInfoId) : null;
        }
    }
}
