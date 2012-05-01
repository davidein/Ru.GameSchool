using Ru.GameSchool.DataLayer.Repository;

namespace Ru.GameSchool.BusinessLayer.Services
{
    /// <summary>
    /// Abstract base class that all services inherit from.
    /// </summary> 
    public abstract class BaseService
    {
        private IGameSchoolEntities _gameSchoolEntities;

        /// <summary>
        /// Gets an instance of the datasource. If no datasource is explicitly set, the default one will be setup.
        /// </summary>
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

        public void SetDatasource(IGameSchoolEntities dataSource)
        {
            _gameSchoolEntities = dataSource;
        }

        protected void Save()
        {
            GameSchoolEntities.SaveChanges();
        }
    }
}
