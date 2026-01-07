using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class UrunController : Controller
    {
        Context c = new Context();

        // �r�n Listeleme
        public ActionResult Index(string p)
        {
            var urunler = from x in c.Uruns where x.Durum == true select x;

            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(y => y.UrunAd.Contains(p));
            }

            return View(urunler.ToList());
        }

        // Yeni �r�n Ekleme (GET)
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }

        // Yeni �r�n Ekleme (POST)
        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
            p.Durum = true; // yeni eklenen �r�n aktif olsun
            c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }
        [HttpPost]
        public ActionResult UrunGuncelle(Urun p, IFormFile UrunGorselFile)
        {
            var urn = c.Uruns.Find(p.Urunid);
            urn.AlisFiyat = p.AlisFiyat;
            urn.Durum = p.Durum;
            urn.Kategoriid = p.Kategoriid;
            urn.Marka = p.Marka;
            urn.SatisFiyat = p.SatisFiyat;
            urn.Stok = p.Stok;
            urn.UrunAd = p.UrunAd;

            // Dosya yükleme işlemi
            if (UrunGorselFile != null && UrunGorselFile.Length > 0)
            {
                // Dosya adını oluştur (güvenli bir isim)
                var dosyaAdi = Path.GetFileNameWithoutExtension(UrunGorselFile.FileName);
                var uzanti = Path.GetExtension(UrunGorselFile.FileName);
                var yeniDosyaAdi = $"{dosyaAdi}_{DateTime.Now.Ticks}{uzanti}";
                
                // Kayıt yolu
                var kayitYolu = Path.Combine("wwwroot", "Image", "urunler");
                var tamYol = Path.Combine(Directory.GetCurrentDirectory(), kayitYolu);
                
                // Klasör yoksa oluştur
                if (!Directory.Exists(tamYol))
                {
                    Directory.CreateDirectory(tamYol);
                }
                
                // Dosyayı kaydet
                var dosyaYolu = Path.Combine(tamYol, yeniDosyaAdi);
                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    UrunGorselFile.CopyTo(stream);
                }
                
                // Veritabanına kaydedilecek yol
                urn.UrunGorsel = $"/Image/urunler/{yeniDosyaAdi}";
            }
            else if (!string.IsNullOrEmpty(p.UrunGorsel))
            {
                // Dosya yüklenmemişse mevcut değeri koru
                urn.UrunGorsel = p.UrunGorsel;
            }

            c.SaveChanges();
            return RedirectToAction("Index");
        }

        // �r�n Listesi
        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }

        // Sat�� Yap (GET)
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();

            ViewBag.dgr3 = deger3;

            var deger1 = c.Uruns.Find(id);
            if (deger1 == null)
            {
                return NotFound();
            }

            ViewBag.dgr1 = deger1.Urunid;
            ViewBag.dgr2 = deger1.SatisFiyat;

            return View();
        }

        // Sat�� Yap (POST)
        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index", "Satis");
        }
    }
}



