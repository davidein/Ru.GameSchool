using System.Diagnostics.CodeAnalysis;
using Ru.GameSchool.BusinessLayer.Interfaces;
using Ru.GameSchool.DataLayer.Repository;


namespace Ru.GameSchool.BusinessLayer.Services

{
    /// <summary>
    /// Abstract base class that all services inherit from.
    /// </summary> 
    public abstract class BaseService
    {
        private IGameSchoolEntities _gameSchoolEntities;
        private IExternalPointContainer _externalPointContainer;
        private IExternalNotificationContainer _notificationService;

        /// <summary>
        /// Gets an instance of the datasource. If no datasource is explicitly set, the default one will be setup.
        /// </summary>
        internal IGameSchoolEntities GameSchoolEntities
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

        /// <summary>
        /// Sets up a new datasource.
        /// </summary>
        /// <param name="dataSource">A datasource that implements the IGameSchoolEntities interface.</param>
        public void SetDatasource(IGameSchoolEntities dataSource)
        {
            _gameSchoolEntities = dataSource;
        }

        public IExternalPointContainer ExternalPointContainer
        {
            get { return _externalPointContainer; }
            set { _externalPointContainer = value; }
        }

        public IExternalNotificationContainer ExternalNotificationContainer
        {
            get { return _notificationService; }
            set { _notificationService = value; }
        }

        /// <summary>
        /// Save all changes to the datasource.
        /// </summary>
        protected void Save()
        {
            GameSchoolEntities.SaveChanges();
        }
    }
}
