using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using System.Linq;

namespace MVC_ONLINE_TICARI_OTOMASYON.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "A")]
    public class DashboardController : Controller
    {
        Context c = new Context();

        public IActionResult Index()
        {
            // İstatistikler
            ViewBag.ToplamUrun = c.Uruns.Count();
            ViewBag.ToplamKategori = c.Kategoris.Count();
            ViewBag.ToplamPersonel = c.Personels.Count();
            ViewBag.ToplamCari = c.Carilers.Count();

            return View();
        }

        public IActionResult Raporlar()
        {
            return View();
        }

        public IActionResult Ayarlar()
        {
            return View();
        }
    }
}
