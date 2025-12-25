using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

//Ýlk önce controller oluþturulur. Daha sonra ilgili view'lar oluþturulacaktýr.


namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class CariController : BaseController
    {
        // Context artýk BaseController'dan geliyor, yeniden tanýmlamaya gerek yok
        // Context c = new Context();

        // GET: Cari
        public ActionResult Index()
        {

            var degerler = c.Carilers.Where(x=>x.Durum == true).ToList(); //Cariler tablosundaki tüm verileri listele
            //Durum alaný true olan carileri listele

            return View(degerler);
        }


        //Cari Ekleme Sayfasý.

        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CariEkle(Cariler p)
        {
            p.Durum = true; //Cari aktif olarak ekleniyor
            c.Carilers.Add(p); //Yeni cari ekleme
            c.SaveChanges(); //Deðiþiklikleri kaydet
            return RedirectToAction("Index"); //Ýndex sayfasýna yönlendir
        }

        //Silme iþlemi için
        public ActionResult CariSil(int id)

        {
            var car = c.Carilers.Find(id);
            car.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        //Carilerde güncelleme iþlemi için cari getirme

        public ActionResult CariGetir(int id)
        {
            var car = c.Carilers.Find(id);
            return View("CariGetir", car); //CariGetir view'ýna yönlendir
        }

        //Güncelleme iþlemi için

        public ActionResult CariGuncelle(Cariler p)
        {
            if (!ModelState.IsValid) //Model doðrulama kontrolü
            {
                return View("CariGetir", p); //Hata varsa CariGetir view'ýna geri dön
            }
            var car = c.Carilers.Find(p.Cariid);
            car.CariAd = p.CariAd;
            car.CariSoyad = p.CariSoyad;
            car.CariSehir = p.CariSehir;
            car.CariMail = p.CariMail;
            c.SaveChanges(); //Deðiþiklikleri kaydet
            return RedirectToAction("Index"); //Ýndex sayfasýna yönlendir
        }

        //Carinin Yaptýðý Satýþlarý Listeleme
        public ActionResult MusteriSatis (int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList(); //Cariid'ye göre satýþlarý listele
            var cr = c.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault(); // Cari adýný ve soyadýný al
            ViewBag.cari = cr; //Cari adýný ViewBag'e ata
            return View(degerler); //Satýþlarý view'a gönder
        }
    }
}

