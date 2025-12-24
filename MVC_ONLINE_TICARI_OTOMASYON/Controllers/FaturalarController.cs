using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using System.Data.Entity;

using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;


namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class FaturalarController : Controller
    {
        // GET: Faturalar
        Context c = new Context();

        
        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();
            return View(liste);
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f) //Burada Faturalar sınıfından gelen f parametresi kullanılıyor.
        {
            c.Faturalars.Add(f); //Faturalar sınıfından gelen f parametresini c.Faturalars'a ekliyoruz.
                c.SaveChanges(); //Değişiklikleri kaydediyoruz.
             return RedirectToAction("Index"); //Faturalar listesini görüntülemek için Index metoduna yönlendiriyoruz.                    
        }

        public ActionResult FaturaGetir (int id)
        {
            var fatura = c.Faturalars.Find(id);

            return View("FaturaGetir", fatura);
        }

        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var fatura = c.Faturalars.Find(f.Faturaid); //Faturaid ile faturayı buluyoruz.
            fatura.FaturaSeriNo = f.FaturaSeriNo; //FaturaSeriNo'yu güncelliyoruz.
            fatura.FaturaSiraNo = f.FaturaSiraNo; //FaturaSiraNo'yu güncelliyoruz.
            fatura.Saat = f.Saat; //Saati güncelliyoruz.
            fatura.Tarih = f.Tarih; //Tarihi güncelliyoruz.
            fatura.VergiDairesi = f.VergiDairesi; //VergiDairesini güncelliyoruz.
           
            c.SaveChanges(); //Değişiklikleri kaydediyoruz.
            return RedirectToAction("Index"); //Faturalar listesini görüntülemek için Index metoduna yönlendiriyoruz.
        }

        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
           
            return View(degerler);
        }


        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }

        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalems.Add(p); //FaturaKalem sınıfından gelen p parametresini c.FaturaKalems'e ekliyoruz.
            c.SaveChanges(); //Değişiklikleri kaydediyoruz.
            return RedirectToAction("Index");
        }

        public ActionResult Dinamik()
        {
            Class4 cs = new Class4();
            cs.deger1 = c.Faturalars
                         .Include(x => x.FaturaKalems) // ilişkili kalemleri de yükle
                         .ToList();

            return View(cs);
        }
        public  ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo, DateTime Tarih, string VergiDairesi, string saat, string TeslimEden , string TeslimAlan , string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSiraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = saat;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            f.Toplam = Convert.ToDecimal(Toplam);
            c.Faturalars.Add(f);
            foreach(var x in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;
                fk.Miktar = x.Miktar;
                fk.BiriFiyat = x.BiriFiyat;
                fk.Tutar = x.Tutar;
                fk.Faturaid = f.Faturaid;
                c.FaturaKalems.Add(fk);

            }
            c.SaveChanges();

            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
            
        }
    }
}