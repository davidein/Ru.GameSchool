using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.BusinessLayer.Enums
{
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Casing is intentional")]
    public enum UserStatus
    {
        Active = 1,
        Banned = 2
    }
}
