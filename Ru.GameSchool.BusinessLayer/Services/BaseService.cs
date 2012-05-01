using Ru.GameSchool.DataLayer;
using Ru.GameSchool.DataLayer.Repository;

namespace Ru.GameSchool.BusinessLayer.Services
{
    /// <summary>
    /// Abstract base class that all services inherit from.
    /// </summary> 
    public abstract class BaseService
    {
        private IGameSchoolEntities _gameSchoolEntities;

        public IGameSchoolEntities GameSchoolEntities
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
