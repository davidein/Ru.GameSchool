using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.BusinessLayer.Exceptions
{
    public class GameSchoolException : Exception
    {
        public GameSchoolException(string message): base(message)
        {
            
        }
    }
}
