using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar; // Models namespace'i projenizin modellerini içerir
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class SatýsController : Controller // 'SatýsController' yerine 'SatisController' olarak düzeltildi
    {
        // Veritabaný baðlamýný (context) oluþturalým.
        Context c = new Context();

        // GET: Satis (Satýþlarý listeleme sayfasý)
        public ActionResult Index()
        {
            // SatisHarekets tablosundaki tüm kayýtlarý listeleyip view'e gönderiyoruz.
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        }

        // GET: YeniSatis (Yeni satýþ ekleme formu sayfasý)
        [HttpGet]
        public ActionResult YeniSatis()
        {
            // Dropdown listeler için gerekli verileri hazýrlýyoruz ve ViewBag ile View'e taþýyoruz.

            // 1. Ürünler için SelectListItem listesi
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd, // Dropdown'da görünecek metin (ürün adý)
                                               Value = x.Urunid.ToString() // Seçildiðinde alýnacak deðer (ürün ID)
                                           }).ToList();

            // 2. Cariler (Müþteriler) için SelectListItem listesi
            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad, // Müþteri adý ve soyadý birleþik
                                               Value = x.Cariid.ToString() // Müþteri ID
                                           }).ToList();

            // 3. Personeller için SelectListItem listesi
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad, // Personel adý ve soyadý birleþik
                                               Value = x.Personelid.ToString() // Personel ID
                                           }).ToList();

            // Bu listeleri ViewBag'e atayarak View'de eriþilebilir hale getiriyoruz.
            ViewBag.dgr1 = deger1; // Ürünler
            ViewBag.dgr2 = deger2; // Cariler
            ViewBag.dgr3 = deger3; // Personeller

            // Yeni bir satýþ hareketi nesnesi oluþturup View'e gönderiyoruz.
            return View(new SatisHareket());
        }

        // POST: YeniSatis (Formdan gönderilen yeni satýþ verilerini iþleme)
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {
            // Satýþ tarihini otomatik olarak güncel tarih olarak ayarlýyoruz.
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());

            // Yeni satýþ hareketini veritabaný baðlamýna ekliyoruz.
            c.SatisHarekets.Add(s);

            // Deðiþiklikleri veritabanýna kaydediyoruz.
            c.SaveChanges();

            // Ýþlem tamamlandýktan sonra kullanýcýyý Satýþlar listeleme sayfasýna yönlendiriyoruz.
            return RedirectToAction("Index");
        }

        public ActionResult SatisGetir (int id)
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
            var deger = c.SatisHarekets.Find(id); // Veritabanýndan ID'ye göre satýþ hareketini buluyoruz.
            return View("SatisGetir", deger); // Bulunan satýþ hareketini "SatisGetir" view'ine gönderiyoruz.
        }

        public ActionResult SatisGuncelle (SatisHareket p)
        {
            var deger = c.SatisHarekets.Find(p.Satisid); // Güncellenecek satýþ hareketini ID'sine göre buluyoruz.
            deger.Cariid = p.Cariid; 
            deger.Adet = p.Adet;
            deger.Fiyat = p.Fiyat;
            deger.Personelid = p.Personelid;
            deger.Tarih = p.Tarih;
            deger.ToplamTutar = p.ToplamTutar;
            deger.Urunid = p.Urunid;

            c.SaveChanges(); // Güncellenen satýþ hareketini veritabanýna kaydediyoruz.
            return RedirectToAction("Index"); // Güncelleme iþleminden sonra kullanýcýyý Satýþlar listeleme sayfasýna yönlendiriyoruz.

        }

        public ActionResult SatisDetay(int id)
        {
            var deger = c.SatisHarekets.Where(x => x.Satisid == id).ToList(); // Belirli bir satýþ ID'sine göre satýþ detaylarýný alýyoruz.
            return View(deger); // Alýnan satýþ detaylarýný "SatisDetay" view'ine gönderiyoruz.
        }
    }
}


