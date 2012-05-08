using System.Web;
using System.Web.Security;

namespace Ru.GameSchool.Web.Classes.Helper
{
    public class MembershipHelper
    {
        public static GameSchoolMembershipUser GetUser()
        {
            const string key = "UserPerRequest";

            if (HttpContext.Current.Items[key] == null)
            { 
                HttpContext.Current.Items[key] = Membership.GetUser() as GameSchoolMembershipUser;
            }

            return HttpContext.Current.Items[key] as GameSchoolMembershipUser;
        }
    }
}