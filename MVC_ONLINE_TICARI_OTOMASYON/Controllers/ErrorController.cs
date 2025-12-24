using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }

        public ActionResult PageError()
        {
            Response.TrySkipIisCustomErrors = true; //Error sayfasını göster.
            return View();
        }

        public ActionResult Page400()
        {
            Response.StatusCode = 400; //Error kodu
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }

        public ActionResult Page403()
        {
            Response.StatusCode = 403; //Error kodu
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }
        
        public ActionResult Page404()
        {
            Response.StatusCode = 404; //Error kodu
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }

        public ActionResult Page500() 
        {
            Response.StatusCode = 500; //Error kodu
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }

        public ActionResult NotFound()
        {
            return Page404();
        }

        public ActionResult ServerError()
        {
            return Page500();
        }

        public ActionResult Unauthorized()
        {
            return Page403();
        }
    }
}