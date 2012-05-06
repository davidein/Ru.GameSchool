using Ru.GameSchool.DataLayer.Repository;

namespace Ru.GameSchool.BusinessLayer.Interfaces
{
    public interface IExternalNotificationContainer
    {
        void CreateNotification(string text, string url, int userInfoId);
        //void UpdateNotification(Notification notification);
    }
}