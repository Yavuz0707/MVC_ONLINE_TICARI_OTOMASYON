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
    public class MesajController : Controller
    {
        // GET: Mesaj
        public ActionResult Index()
        {
            return View();
        }
    }
}


