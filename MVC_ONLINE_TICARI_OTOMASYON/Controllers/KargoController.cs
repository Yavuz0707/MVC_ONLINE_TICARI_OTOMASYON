using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();

        public ActionResult Index(String p)
        {
            // Kullanıcı bilgilerini ViewBag'e aktar
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
            return Content("<h1>KARGO CONTROLLER ÇALIŞIYOR!</h1><p>Bu mesajı görüyorsan controller'a erişebiliyorsun.</p>");
        }

        [HttpGet]
        public ActionResult YeniKargo()
        {
            System.Diagnostics.Debug.WriteLine("===== YeniKargo GET çağrıldı =====");
            try
            {
                ViewBag.takipkod = GenerateTakipKodu();
                System.Diagnostics.Debug.WriteLine($"Takip kodu üretildi: {ViewBag.takipkod}");
                
                // Aktif müşterileri getir
                var musteriler = c.Carilers
                    .Where(x => x.Durum == true)
                    .OrderBy(x => x.CariAd)
                    .ToList();
                
                System.Diagnostics.Debug.WriteLine($"Müşteri sayısı: {musteriler.Count}");
                
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
        
        // Müşteriye göre satışları getiren Ajax endpoint
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
                
                return Json(satislar, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
        //Yeni Kargo Eklemek İçin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniKargo(KargoDetay d, int? SatisId)
        {
            System.Diagnostics.Debug.WriteLine("===== YeniKargo POST çağrıldı =====");
            System.Diagnostics.Debug.WriteLine($"TakipKodu: {d?.TakipKodu}");
            System.Diagnostics.Debug.WriteLine($"SatisId: {SatisId}");
            System.Diagnostics.Debug.WriteLine($"Durum: {d?.Durum}");
            System.Diagnostics.Debug.WriteLine($"Personel: {d?.Personel}");
            
            try
            {
                if (!ModelState.IsValid || !SatisId.HasValue)
                {
                    ViewBag.takipkod = d.TakipKodu;
                    
                    // Müşteri listesini tekrar yükle
                    var musteriler = c.Carilers
                        .Where(x => x.Durum == true)
                        .OrderBy(x => x.CariAd)
                        .ToList();
                    
                    ViewBag.Musteriler = new SelectList(musteriler, "Cariid", "CariAd");
                    
                    TempData["Hata"] = "Lütfen tüm alanları doldurun ve bir satış seçin!";
                    return View(d);
                }

                // Bu satış için zaten kargo oluşturulmuş mu kontrol et
                var mevcutKargo = c.KargoDetays.FirstOrDefault(k => k.SatisId == SatisId.Value);
                if (mevcutKargo != null)
                {
                    TempData["Hata"] = $"Bu satış için zaten kargo oluşturulmuş! Takip Kodu: {mevcutKargo.TakipKodu}";
                    return RedirectToAction("Index");
                }

                // Satış bilgilerini al
                var satis = c.SatisHarekets
                    .Include(s => s.Cariler)
                    .Include(s => s.Urun)
                    .FirstOrDefault(s => s.Satisid == SatisId.Value);

                if (satis == null)
                {
                    TempData["Hata"] = "Seçilen satış bulunamadı!";
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
                    TempData["Hata"] = "Validation hatası: " + fullErrorMessage;
                    return RedirectToAction("Index");
                }
                
                TempData["Mesaj"] = $"Kargo başarıyla oluşturuldu! Takip Kodu: {d.TakipKodu} - Alıcı: {satis.Cariler.CariAd} {satis.Cariler.CariSoyad}";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Hata"] = "Kargo oluşturulurken hata: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult KargoDetay(int id)
        {
            var kargo = c.KargoDetays.Find(id);
            if (kargo == null)
            {
                return HttpNotFound();
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
                ViewBag.Hata = "Girilen takip koduna ait kargo bulunamadı!";
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
                return HttpNotFound();
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
                return HttpNotFound();
            }

            mevcutKargo.Aciklama = kargo.Aciklama;
            mevcutKargo.Personel = kargo.Personel;
            mevcutKargo.Alici = kargo.Alici;
            
            c.SaveChanges();
            
            TempData["Mesaj"] = "Kargo başarıyla güncellendi!";
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
