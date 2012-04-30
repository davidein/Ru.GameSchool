using Ru.GameSchool.DataLayer;

namespace Ru.GameSchool.BusinessLayer.Services
{
    public abstract class BaseService
    {
        private GameSchoolEntities _gameSchoolEntities = null;

        public GameSchoolEntities GameSchoolEntities
        {
            get
            {
                if (_gameSchoolEntities== null)
                    _gameSchoolEntities = new GameSchoolEntities();

                return _gameSchoolEntities;
            }
        }

        protected void Save()
        {
            GameSchoolEntities.SaveChanges();
        }
    }
}
