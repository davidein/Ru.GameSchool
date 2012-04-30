using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ru.GameSchool.DataLayer;

namespace Ru.GameSchool.BusinessLayer
{
    public abstract class BaseService
    {
        protected GameSchoolEntities _gameSchoolEntities = new GameSchoolEntities();

        protected void Save()
        {
            _gameSchoolEntities.SaveChanges();
        }
    }
}
