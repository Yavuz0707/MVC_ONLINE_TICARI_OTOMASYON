using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using System.Linq;

namespace MVC_ONLINE_TICARI_OTOMASYON.Areas.Public.Controllers
{
    [Area("Public")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        Context c = new Context();

        public IActionResult Index()
        {
            var urunler = c.Uruns.Take(8).ToList();
            return View(urunler);
        }

        public IActionResult Hakkimizda()
        {
            return View();
        }

        public IActionResult Iletisim()
        {
            return View();
        }

        // Ürünler sayfası - Login ekranından erişilebilir
        public IActionResult Urunler(int yukle = 8)
        {
            var urunler = c.Uruns
                .Where(x => x.Durum == true)
                .OrderByDescending(x => x.Urunid)
                .Take(yukle)
                .ToList();
            
            var toplamUrun = c.Uruns.Count(x => x.Durum == true);
            
            ViewBag.ToplamUrun = toplamUrun;
            ViewBag.YuklenenAdet = yukle;
            ViewBag.DahaFazlaVar = yukle < toplamUrun;
            
            return View(urunler);
        }
    }
}
