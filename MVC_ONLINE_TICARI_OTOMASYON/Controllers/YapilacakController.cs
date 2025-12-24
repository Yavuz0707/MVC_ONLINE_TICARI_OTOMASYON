using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar; // Assuming you have a Models namespace

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    public class YapilacakController : Controller
    {
        // GET: Yapilacak

        Context c = new Context(); // Assuming you have a Context class for database access
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count().ToString();    // Count of Carilers - ViewBag'a aktarılıyor
            ViewBag.d1 = deger1; 
            var deger2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3 = c.Kategoris.Count().ToString();
            ViewBag.d3 = deger3;
            var deger4 = (from x in c.Carilers select x.CariSehir).Distinct().Count().ToString(); // Carilerin içerisinden şehir sayısı çekmek için böyle yaptık
            ViewBag.d4 = deger4;
         
            var yapilacaklar = c.Yapilacaks.ToList(); // Assuming Yapilacak is a model class
            return View(yapilacaklar); // Return the list of Yapilacak items to the view

        }
    }
}