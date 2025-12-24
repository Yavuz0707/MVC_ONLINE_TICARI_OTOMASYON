using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;

//İlk önce controller oluşturulur. Daha sonra ilgili view'lar oluşturulacaktır.


namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class CariController : BaseController
    {
        // Context artık BaseController'dan geliyor, yeniden tanımlamaya gerek yok
        // Context c = new Context();

        // GET: Cari
        public ActionResult Index()
        {

            var degerler = c.Carilers.Where(x=>x.Durum == true).ToList(); //Cariler tablosundaki tüm verileri listele
            //Durum alanı true olan carileri listele

            return View(degerler);
        }


        //Cari Ekleme Sayfası.

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
            c.SaveChanges(); //Değişiklikleri kaydet
            return RedirectToAction("Index"); //İndex sayfasına yönlendir
        }

        //Silme işlemi için
        public ActionResult CariSil(int id)

        {
            var car = c.Carilers.Find(id);
            car.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        //Carilerde güncelleme işlemi için cari getirme

        public ActionResult CariGetir(int id)
        {
            var car = c.Carilers.Find(id);
            return View("CariGetir", car); //CariGetir view'ına yönlendir
        }

        //Güncelleme işlemi için

        public ActionResult CariGuncelle(Cariler p)
        {
            if (!ModelState.IsValid) //Model doğrulama kontrolü
            {
                return View("CariGetir", p); //Hata varsa CariGetir view'ına geri dön
            }
            var car = c.Carilers.Find(p.Cariid);
            car.CariAd = p.CariAd;
            car.CariSoyad = p.CariSoyad;
            car.CariSehir = p.CariSehir;
            car.CariMail = p.CariMail;
            c.SaveChanges(); //Değişiklikleri kaydet
            return RedirectToAction("Index"); //İndex sayfasına yönlendir
        }

        //Carinin Yaptığı Satışları Listeleme
        public ActionResult MusteriSatis (int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList(); //Cariid'ye göre satışları listele
            var cr = c.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault(); // Cari adını ve soyadını al
            ViewBag.cari = cr; //Cari adını ViewBag'e ata
            return View(degerler); //Satışları view'a gönder
        }
    }
}