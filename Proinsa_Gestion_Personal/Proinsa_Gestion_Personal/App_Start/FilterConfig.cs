using System.Web;
using System.Web.Mvc;

namespace Proinsa_Gestion_Personal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
