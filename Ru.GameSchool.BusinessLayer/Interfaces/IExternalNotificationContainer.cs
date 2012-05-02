using Ru.GameSchool.DataLayer.Repository;

namespace Ru.GameSchool.BusinessLayer.Interfaces
{
    public interface IExternalNotificationContainer
    {
        void AddNotification(Notification notification);
        void UpdateNotification(Notification notification);
    }
}