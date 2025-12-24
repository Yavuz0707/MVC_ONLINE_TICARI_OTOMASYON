using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;

            var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            ViewBag.mid = mailid;

            var toplamsatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.toplamsatis = toplamsatis;

            var toplamtutar = c.SatisHarekets
                .Where(x => x.Cariid == mailid)
                .Sum(y => (decimal?)y.ToplamTutar) ?? 0;
            ViewBag.toplamtutar = toplamtutar;

            var toplamurunsayisi = c.SatisHarekets
                .Where(x => x.Cariid == mailid)
                .Sum(y => (int?)y.Adet) ?? 0;
            ViewBag.toplamurunsayisi = toplamurunsayisi;

            var adsoyad = c.Carilers
                .Where(x => x.CariMail == mail)
                .Select(y => y.CariAd + " " + y.CariSoyad)
                .FirstOrDefault();
            ViewBag.adsoyad = adsoyad;

            return View(degerler);
        }

        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }

        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.mesajlars.Where(x => x.Alici == mail)
                .OrderByDescending(x => x.MesajID).ToList();

            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View(mesajlar);
        }

        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.mesajlars.Where(x => x.Gonderici == mail)
                .OrderByDescending(z => z.MesajID).ToList();

            var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            return View(mesajlar);
        }

        public ActionResult MesajDetay(int id)
        {
            var degerler = c.mesajlars.Where(x => x.MesajID == id).ToList();
            var mail = (string)Session["CariMail"];

            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];

            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(mesajlar m)
        {
            var mail = (string)Session["CariMail"];
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gonderici = mail;
            c.mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }

        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            var caribul = c.Carilers.Find(id);
            return PartialView("Partial1", caribul);
        }

        public PartialViewResult Partial2()
        {
            var veriler = c.mesajlars.Where(x => x.Gonderici == "admin").ToList();
            return PartialView(veriler);
        }

        [HttpPost]
        public ActionResult CariBilgiGuncelle(Cariler cr)
        {
            var cari = c.Carilers.Find(cr.Cariid);
            if (cari == null) return HttpNotFound();

            cari.CariAd = cr.CariAd;
            cari.CariSoyad = cr.CariSoyad;
            cari.CariMail = cr.CariMail;
            cari.CariSehir = cr.CariSehir;

            if (!string.IsNullOrWhiteSpace(cr.CariSifre))
            {
                cari.CariSifre = cr.CariSifre;
            }

            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KargoTakibi()
        {
            var mail = (string)Session["CariMail"];
            if (string.IsNullOrEmpty(mail))
            {
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
                
                // Müşteriye ait kargoları getir
                var kargolar = c.KargoDetays
                    .Where(x => x.Alici != null && (x.Alici == mail || x.Alici.Contains(mail)))
                    .ToList()
                    .OrderByDescending(x => x.Tarih)
                    .ToList();

                ViewBag.CariMail = mail;
                return View(kargolar);
            }
            catch (Exception ex)
            {
                TempData["Hata"] = "Kargo bilgileri yüklenirken bir hata oluştu: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult KargoSorgula()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KargoSorgula(string takipKodu)
        {
            if (string.IsNullOrEmpty(takipKodu))
            {
                ViewBag.Hata = "Lütfen takip kodu giriniz!";
                return View();
            }

            var kargo = c.KargoDetays.FirstOrDefault(x => x.TakipKodu == takipKodu.Trim());

            if (kargo == null)
            {
                ViewBag.Hata = "Girilen takip koduna ait kargo bulunamadı!";
                return View();
            }

            var takipBilgileri = c.KargoTakips
                .Where(x => x.TakipKodu == takipKodu.Trim())
                .OrderBy(x => x.TarihZaman)
                .ToList();

            ViewBag.KargoDetay = kargo;
            return View("KargoDetay", takipBilgileri);
        }

        public ActionResult CikisYap()
        {
            Session.Clear();
            Session.Abandon();
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                c?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
