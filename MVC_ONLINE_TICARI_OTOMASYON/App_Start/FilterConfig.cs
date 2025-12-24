using System.Web;
using System.Web.Mvc;

namespace MVC_ONLINE_TICARI_OTOMASYON
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
