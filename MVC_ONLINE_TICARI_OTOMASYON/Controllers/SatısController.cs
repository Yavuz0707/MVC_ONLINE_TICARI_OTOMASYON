using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class SatisController : Controller
    {
        // Veritabanı bağlamını (context) oluşturalım.
        Context c = new Context();

        // GET: Satis (Satışları listeleme sayfası)
        public ActionResult Index()
        {
            // SatisHarekets tablosundaki tüm kayıtları listeleyip view'e gönderiyoruz.
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        }

        // GET: YeniSatis (Yeni satış ekleme formu sayfası)
        [HttpGet]
        public ActionResult YeniSatis()
        {
            // Dropdown listeler için gerekli verileri hazırlıyoruz ve ViewBag ile View'e taşıyoruz.

            // 1. Ürünler için SelectListItem listesi
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd, // Dropdown'da görünecek metin (Ürün adı)
                                               Value = x.Urunid.ToString() // Seçildiğinde alınacak değer (Ürün ID)
                                           }).ToList();

            // 2. Cariler (Müşteriler) için SelectListItem listesi
            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad, // Müşteri adı ve soyadı birleşik
                                               Value = x.Cariid.ToString() // Müşteri ID
                                           }).ToList();

            // 3. Personeller için SelectListItem listesi
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad, // Personel adı ve soyadı birleşik
                                               Value = x.Personelid.ToString() // Personel ID
                                           }).ToList();

            // Bu listeleri ViewBag'e atayarak View'de erişilebilir hale getiriyoruz.
            ViewBag.dgr1 = deger1; // Ürünler
            ViewBag.dgr2 = deger2; // Cariler
            ViewBag.dgr3 = deger3; // Personeller

            // Yeni bir satış hareketi nesnesi oluşturup View'e gönderiyoruz.
            return View(new SatisHareket());
        }

        // POST: YeniSatis (Formdan gönderilen yeni satış verilerini işleme)
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {
            try
            {
                // Satış tarihini otomatik olarak güncel tarih olarak ayarlıyoruz.
                s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());

                // Foreign Key kontrolleri
                // 1. Cari kontrolü (EN ÖNEMLİ!)
                var cari = c.Carilers.Find(s.Cariid);
                if (cari == null)
                {
                    TempData["Hata"] = "Seçilen cari bulunamadı! Lütfen geçerli bir cari seçiniz.";
                    return RedirectToAction("YeniSatis");
                }

                // 2. Ürün kontrolü
                var urun = c.Uruns.Find(s.Urunid);
                if (urun == null)
                {
                    TempData["Hata"] = "Seçilen ürün bulunamadı! Lütfen geçerli bir ürün seçiniz.";
                    return RedirectToAction("YeniSatis");
                }

                // 3. Personel kontrolü
                if (s.Personelid > 0)
                {
                    var personel = c.Personels.Find(s.Personelid);
                    if (personel == null)
                    {
                        TempData["Hata"] = "Seçilen personel bulunamadı! Lütfen geçerli bir personel seçiniz.";
                        return RedirectToAction("YeniSatis");
                    }
                }

                // Yeni satış hareketini veritabanı bağlamına ekliyoruz.
                c.SatisHarekets.Add(s);

                // Değişiklikleri veritabanına kaydediyoruz.
                c.SaveChanges();

                TempData["Mesaj"] = "Satış başarıyla kaydedildi!";
                // İşlem tamamlandıktan sonra kullanıcıyı Satışlar listeleme sayfasına yönlendiriyoruz.
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                var innerException = ex.InnerException?.InnerException?.Message ?? ex.Message;
                TempData["Hata"] = $"Veritabanı Hatası: {innerException}";
                return RedirectToAction("YeniSatis");
            }
            catch (Exception ex)
            {
                TempData["Hata"] = $"Beklenmeyen Hata: {ex.Message}";
                return RedirectToAction("YeniSatis");
            }
        }

public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.Urunid.ToString()
                                           }).ToList();


            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();


            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();


            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            var deger = c.SatisHarekets.Find(id); // Veritabanından ID'ye göre satış hareketini buluyoruz.
            return View("SatisGetir", deger); // Bulunan satış hareketini "SatisGetir" view'ine gönderiyoruz.
        }

        public ActionResult SatisGuncelle(SatisHareket p)
        {
            var deger = c.SatisHarekets.Find(p.Satisid); // Güncellenecek satış hareketini ID'sine göre buluyoruz.
            deger.Cariid = p.Cariid;
            deger.Adet = p.Adet;
            deger.Fiyat = p.Fiyat;
            deger.Personelid = p.Personelid;
            deger.Tarih = p.Tarih;
            deger.ToplamTutar = p.ToplamTutar;
            deger.Urunid = p.Urunid;

            c.SaveChanges(); // Güncellenen satış hareketini veritabanına kaydediyoruz.
            return RedirectToAction("Index"); // Güncelleme işleminden sonra kullanıcıyı Satışlar listeleme sayfasına yönlendiriyoruz.

        }

        public ActionResult SatisDetay(int id)
        {
            var deger = c.SatisHarekets.Where(x => x.Satisid == id).ToList(); // Belirli bir satış ID'sine göre satış detaylarını alıyoruz.
            return View(deger); // Alınan satış detaylarını "SatisDetay" view'ine gönderiyoruz.
        }
    }
}


