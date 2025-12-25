using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();

        public ActionResult Index(String p)
        {
            // Kullanýcý bilgilerini ViewBag'e aktar
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.KullaniciAd = User.Identity.Name;
            }

            var k = from x in c.KargoDetays select x;

            if (!string.IsNullOrEmpty(p))
            {
                k = k.Where(y => y.TakipKodu.Contains(p));
            }

            return View(k.ToList().OrderByDescending(x => x.Tarih).ToList());
        }

        public ActionResult Test()
        {
            return Content("<h1>KARGO CONTROLLER ÇALIÞIYOR!</h1><p>Bu mesajý görüyorsan controller'a eriþebiliyorsun.</p>");
        }

        [HttpGet]
        public ActionResult YeniKargo()
        {
            System.Diagnostics.Debug.WriteLine("===== YeniKargo GET çaðrýldý =====");
            try
            {
                ViewBag.takipkod = GenerateTakipKodu();
                System.Diagnostics.Debug.WriteLine($"Takip kodu üretildi: {ViewBag.takipkod}");
                
                // Aktif müþterileri getir
                var musteriler = c.Carilers
                    .Where(x => x.Durum == true)
                    .OrderBy(x => x.CariAd)
                    .ToList();
                
                System.Diagnostics.Debug.WriteLine($"Müþteri sayýsý: {musteriler.Count}");
                
                ViewBag.Musteriler = new SelectList(musteriler, "Cariid", "CariAd");
                System.Diagnostics.Debug.WriteLine("View döndürülüyor...");
                return View();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"HATA: {ex.Message}");
                return Content("<h1>VIEW HATASI</h1><p>" + ex.Message + "</p><pre>" + ex.StackTrace + "</pre>");
            }
        }
        
        // Müþteriye göre satýþlarý getiren Ajax endpoint
        [HttpGet]
        public JsonResult GetMusteriSatislari(int musteriId)
        {
            try
            {
                var satislar = c.SatisHarekets
                    .Include(s => s.Urun)
                    .Include(s => s.Cariler)
                    .Where(s => s.Cariid == musteriId && !c.KargoDetays.Any(k => k.SatisId == s.Satisid))
                    .OrderByDescending(s => s.Tarih)
                    .Select(s => new
                    {
                        satisId = s.Satisid,
                        urunAd = s.Urun.UrunAd,
                        adet = s.Adet,
                        tarih = s.Tarih,
                        tutar = s.ToplamTutar
                    })
                    .ToList();
                
                return Json(satislar);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        
        //Yeni Kargo Eklemek Ýçin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniKargo(KargoDetay d, int? SatisId)
        {
            System.Diagnostics.Debug.WriteLine("===== YeniKargo POST çaðrýldý =====");
            System.Diagnostics.Debug.WriteLine($"TakipKodu: {d?.TakipKodu}");
            System.Diagnostics.Debug.WriteLine($"SatisId: {SatisId}");
            System.Diagnostics.Debug.WriteLine($"Durum: {d?.Durum}");
            System.Diagnostics.Debug.WriteLine($"Personel: {d?.Personel}");
            
            try
            {
                if (!ModelState.IsValid || !SatisId.HasValue)
                {
                    ViewBag.takipkod = d.TakipKodu;
                    
                    // Müþteri listesini tekrar yükle
                    var musteriler = c.Carilers
                        .Where(x => x.Durum == true)
                        .OrderBy(x => x.CariAd)
                        .ToList();
                    
                    ViewBag.Musteriler = new SelectList(musteriler, "Cariid", "CariAd");
                    
                    TempData["Hata"] = "Lütfen tüm alanlarý doldurun ve bir satýþ seçin!";
                    return View(d);
                }

                // Bu satýþ için zaten kargo oluþturulmuþ mu kontrol et
                var mevcutKargo = c.KargoDetays.FirstOrDefault(k => k.SatisId == SatisId.Value);
                if (mevcutKargo != null)
                {
                    TempData["Hata"] = $"Bu satýþ için zaten kargo oluþturulmuþ! Takip Kodu: {mevcutKargo.TakipKodu}";
                    return RedirectToAction("Index");
                }

                // Satýþ bilgilerini al
                var satis = c.SatisHarekets
                    .Include(s => s.Cariler)
                    .Include(s => s.Urun)
                    .FirstOrDefault(s => s.Satisid == SatisId.Value);

                if (satis == null)
                {
                    TempData["Hata"] = "Seçilen satýþ bulunamadý!";
                    return RedirectToAction("Index");
                }

                d.Tarih = DateTime.Now;
                d.SatisId = SatisId.Value;
                d.Alici = satis.Cariler.CariMail ?? (satis.Cariler.CariAd + " " + satis.Cariler.CariSoyad);
                
                c.KargoDetays.Add(d);
                
                try
                {
                    c.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.PropertyName + ": " + x.ErrorMessage);
                    
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    TempData["Hata"] = "Validation hatasý: " + fullErrorMessage;
                    return RedirectToAction("Index");
                }
                
                TempData["Mesaj"] = $"Kargo baþarýyla oluþturuldu! Takip Kodu: {d.TakipKodu} - Alýcý: {satis.Cariler.CariAd} {satis.Cariler.CariSoyad}";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Hata"] = "Kargo oluþturulurken hata: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult KargoDetay(int id)
        {
            var kargo = c.KargoDetays.Find(id);
            if (kargo == null)
            {
                return NotFound();
            }
            return View(kargo);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult KargoTakip()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult KargoTakip(string takipKodu)
        {
            if (string.IsNullOrEmpty(takipKodu))
            {
                ViewBag.Hata = "Lütfen takip kodu giriniz!";
                return View();
            }

            var kargo = c.KargoDetays.FirstOrDefault(x => x.TakipKodu == takipKodu.Trim());

            if (kargo == null)
            {
                ViewBag.Hata = "Girilen takip koduna ait kargo bulunamadý!";
                return View();
            }

            var takipBilgileri = c.KargoTakips.Where(x => x.TakipKodu == takipKodu).OrderBy(x => x.TarihZaman).ToList();
            ViewBag.KargoDetay = kargo;
            
            return View("KargoTakipSonuc", takipBilgileri);
        }

        [HttpGet]
        public ActionResult KargoGuncelle(int id)
        {
            var kargo = c.KargoDetays.Find(id);
            if (kargo == null)
            {
                return NotFound();
            }
            return View(kargo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KargoGuncelle(KargoDetay kargo)
        {
            if (!ModelState.IsValid)
            {
                return View(kargo);
            }

            var mevcutKargo = c.KargoDetays.Find(kargo.KargoDetayid);
            if (mevcutKargo == null)
            {
                return NotFound();
            }

            mevcutKargo.Aciklama = kargo.Aciklama;
            mevcutKargo.Personel = kargo.Personel;
            mevcutKargo.Alici = kargo.Alici;
            
            c.SaveChanges();
            
            TempData["Mesaj"] = "Kargo baþarýyla güncellendi!";
            return RedirectToAction("Index");
        }

        private string GenerateTakipKodu()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F", "G", "H", "K", "L", "M", "N", "P", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string takipKodu = "";
            
            do
            {
                int k1 = rnd.Next(0, karakterler.Length);
                int k2 = rnd.Next(0, karakterler.Length);
                int k3 = rnd.Next(0, karakterler.Length);

                int s1 = rnd.Next(100, 1000);
                int s2 = rnd.Next(10, 99);
                int s3 = rnd.Next(10, 99);

                takipKodu = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            }
            while (c.KargoDetays.Any(x => x.TakipKodu == takipKodu));

            return takipKodu;
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


