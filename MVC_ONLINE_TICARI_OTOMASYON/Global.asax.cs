using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVC_ONLINE_TICARI_OTOMASYON.Migrations;

namespace MVC_ONLINE_TICARI_OTOMASYON
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Veritabanı migration'larını otomatik çalıştır
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.Siniflar.Context, Configuration>());
            
            // .NET 9'da bu yapılandırmalar Program.cs'de yapılacak
            // AreaRegistration.RegisterAllAreas();
            // FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // RouteConfig.RegisterRoutes(RouteTable.Routes);
            // BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            // Geçici olarak devre dışı - hataları görmek için
            /*
            var exception = Server.GetLastError();
            
            // Log the exception (you can add logging here)
            System.Diagnostics.Debug.WriteLine($"Application Error: {exception?.Message}");
            
            Response.Clear();
            Server.ClearError();
            
            var httpException = exception as HttpException;
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        routeData.Values["action"] = "Page404";
                        break;
                    case 403:
                        routeData.Values["action"] = "Page403";
                        break;
                    case 500:
                        routeData.Values["action"] = "Page500";
                        break;
                    default:
                        routeData.Values["action"] = "Index";
                        break;
                }
            }
            else
            {
                routeData.Values["action"] = "Index";
            }
            
            IController controller = new Controllers.ErrorController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
            */
        }
    }
}
