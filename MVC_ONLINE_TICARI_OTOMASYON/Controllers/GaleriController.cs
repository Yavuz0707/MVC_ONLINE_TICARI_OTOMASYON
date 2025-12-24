using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    public class GaleriController : Controller
    {
        // GET: Galeri
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Uruns.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }
    }
}
