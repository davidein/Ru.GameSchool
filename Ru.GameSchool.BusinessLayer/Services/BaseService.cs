using Ru.GameSchool.DataLayer;

namespace Ru.GameSchool.BusinessLayer.Services
{
    /// <summary>
    /// Abstract base class that all services inherit from.
    /// </summary> 
    public abstract class BaseService
    {
        private GameSchoolEntities _gameSchoolEntities = null;

        public GameSchoolEntities GameSchoolEntities
        {
            get
            {
                if (_gameSchoolEntities == null)
                { 
                    _gameSchoolEntities = new GameSchoolEntities();
                }
                return _gameSchoolEntities;
            }
        }

        protected void Save()
        {
            GameSchoolEntities.SaveChanges();
        }
    }
}
