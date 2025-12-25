using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CariPanelController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)HttpContext.Session.GetString("CariMail");
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

            var sehir = c.Carilers
                .Where(x => x.CariMail == mail)
                .Select(y => y.CariSehir)
                .FirstOrDefault();
            ViewBag.sehir = sehir;

            var profilFoto = c.Carilers
                .Where(x => x.CariMail == mail)
                .Select(y => y.ProfilFoto)
                .FirstOrDefault();
            ViewBag.profilFoto = profilFoto;

            return View(degerler);
        }

        public ActionResult Siparislerim()
        {
            var mail = (string)HttpContext.Session.GetString("CariMail");
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }

        public ActionResult GelenMesajlar()
        {
            var mail = (string)HttpContext.Session.GetString("CariMail");
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
            var mail = (string)HttpContext.Session.GetString("CariMail");
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
            var mail = (string)HttpContext.Session.GetString("CariMail");

            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)HttpContext.Session.GetString("CariMail");

            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            var mail = (string)HttpContext.Session.GetString("CariMail");
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gonderici = mail;
            c.mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }

        public PartialViewResult Partial1()
        {
            var mail = (string)HttpContext.Session.GetString("CariMail");
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
            if (cari == null) return NotFound();

            cari.CariAd = cr.CariAd;
            cari.CariSoyad = cr.CariSoyad;
            cari.CariMail = cr.CariMail;
            cari.CariSehir = cr.CariSehir;

            if (!string.IsNullOrWhiteSpace(cr.CariSifre))
            {
                cari.CariSifre = cr.CariSifre;
            }

            c.SaveChanges();
            
            TempData["Basari"] = "Profil bilgileriniz başarıyla güncellendi!";
            return RedirectToAction("Profilim");
        }

        [HttpGet]
        public ActionResult Profilim()
        {
            var mail = HttpContext.Session.GetString("CariMail");
            if (string.IsNullOrEmpty(mail))
            {
                return RedirectToAction("Index", "Login");
            }

            var cari = c.Carilers.FirstOrDefault(x => x.CariMail == mail);
            if (cari == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(cari);
        }

        [HttpPost]
        public async Task<ActionResult> ProfilFotoGuncelle(IFormFile profilFoto)
        {
            var mail = HttpContext.Session.GetString("CariMail");
            if (string.IsNullOrEmpty(mail))
            {
                return Json(new { success = false, message = "Oturum bulunamadı" });
            }

            var cari = c.Carilers.FirstOrDefault(x => x.CariMail == mail);
            if (cari == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı" });
            }

            if (profilFoto != null && profilFoto.Length > 0)
            {
                // Dosya uzantısı kontrolü
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(profilFoto.FileName).ToLower();
                
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return Json(new { success = false, message = "Sadece jpg, jpeg, png ve gif formatları desteklenir" });
                }

                // Dosya boyutu kontrolü (5MB)
                if (profilFoto.Length > 5 * 1024 * 1024)
                {
                    return Json(new { success = false, message = "Dosya boyutu 5MB'dan küçük olmalıdır" });
                }

                // Eski fotoğrafı sil
                if (!string.IsNullOrEmpty(cari.ProfilFoto))
                {
                    var eskiFotoPath = Path.Combine(_webHostEnvironment.WebRootPath, cari.ProfilFoto.TrimStart('/'));
                    if (System.IO.File.Exists(eskiFotoPath))
                    {
                        System.IO.File.Delete(eskiFotoPath);
                    }
                }

                // Yeni dosya adı oluştur
                var uniqueFileName = $"profil_{cari.Cariid}_{Guid.NewGuid()}{fileExtension}";
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profil");
                
                // Klasör yoksa oluştur
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Dosyayı kaydet
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profilFoto.CopyToAsync(fileStream);
                }

                // Veritabanını güncelle
                cari.ProfilFoto = $"/uploads/profil/{uniqueFileName}";
                c.SaveChanges();

                return Json(new { success = true, message = "Profil fotoğrafı güncellendi", photoUrl = cari.ProfilFoto });
            }

            return Json(new { success = false, message = "Dosya seçilmedi" });
        }

        public ActionResult KargoTakibi()
        {
            var mail = (string)HttpContext.Session.GetString("CariMail");
            if (string.IsNullOrEmpty(mail))
            {
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
                
                // M��teriye ait kargolar� getir
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
                TempData["Hata"] = "Kargo bilgileri y�klenirken bir hata olu�tu: " + ex.Message;
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
                ViewBag.Hata = "L�tfen takip kodu giriniz!";
                return View();
            }

            var kargo = c.KargoDetays.FirstOrDefault(x => x.TakipKodu == takipKodu.Trim());

            if (kargo == null)
            {
                ViewBag.Hata = "Girilen takip koduna ait kargo bulunamad�!";
                return View();
            }

            var takipBilgileri = c.KargoTakips
                .Where(x => x.TakipKodu == takipKodu.Trim())
                .OrderBy(x => x.TarihZaman)
                .ToList();

            ViewBag.KargoDetay = kargo;
            return View("KargoDetay", takipBilgileri);
        }

        public async Task<ActionResult> CikisYap()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
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




