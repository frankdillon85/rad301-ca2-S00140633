using System.Web;
using System.Web.Mvc;

namespace rad301_ca2_S00140633
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
