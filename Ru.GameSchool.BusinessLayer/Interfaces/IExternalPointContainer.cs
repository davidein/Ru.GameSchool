namespace Ru.GameSchool.BusinessLayer.Interfaces
{
    public interface IExternalPointContainer
    {
        void AddPointsToLevel(int userId, int levelId, int points, string description);
    }
}