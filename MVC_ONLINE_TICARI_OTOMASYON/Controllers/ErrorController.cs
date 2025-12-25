using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            Response.StatusCode = 500;
            return View("PageError");
        }

        public ActionResult PageError()
        {
            return View();
        }

        public ActionResult Page400()
        {
            Response.StatusCode = 400;
            return View("PageError");
        }

        public ActionResult Page403()
        {
            Response.StatusCode = 403;
            return View("PageError");
        }
        
        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            return View("PageError");
        }

        public ActionResult Page500() 
        {
            Response.StatusCode = 500;
            return View("PageError");
        }

        public new ActionResult NotFound()
        {
            return Page404();
        }

        public ActionResult ServerError()
        {
            return Page500();
        }

        public new ActionResult Unauthorized()
        {
            return Page403();
        }
    }
}


