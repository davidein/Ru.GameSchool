using Ru.GameSchool.DataLayer.Repository;

namespace Ru.GameSchool.BusinessLayer.Interfaces
{
    public interface IExternalNotificationContainer
    {
        void CreateNotification(Notification notification);
        void UpdateNotification(Notification notification);
    }
}