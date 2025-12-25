using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            // Dashboard istatistikleri
            try
            {
                ViewBag.ToplamUrun = c.Uruns.Count();
                ViewBag.ToplamCari = c.Carilers.Count(x => x.Durum);
                ViewBag.ToplamPersonel = c.Personels.Count();
                ViewBag.ToplamDepartman = c.Departmans.Count(x => x.Durum);
                ViewBag.BugunkuSatislar = c.SatisHarekets
                    .Count(x => DbFunctions.TruncateTime(x.Tarih) == DbFunctions.TruncateTime(DateTime.Now));
                ViewBag.ToplamSatis = c.SatisHarekets.Count();
                ViewBag.AktifKargolar = c.KargoDetays.Count();
                
                // Son iþlemler
                ViewBag.SonSatislar = c.SatisHarekets
                    .OrderByDescending(x => x.Tarih)
                    .Take(5)
                    .ToList();
            }
            catch (Exception)
            {
                // Hata durumunda default deðerler
                ViewBag.ToplamUrun = 0;
                ViewBag.ToplamCari = 0;
                ViewBag.ToplamPersonel = 0;
                ViewBag.ToplamDepartman = 0;
                ViewBag.BugunkuSatislar = 0;
                ViewBag.ToplamSatis = 0;
                ViewBag.AktifKargolar = 0;
                ViewBag.SonSatislar = new List<SatisHareket>();
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "MVC Online Ticari Otomasyon Sistemi";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ýletiþim Bilgileri";
            return View();
        }
    }
}


