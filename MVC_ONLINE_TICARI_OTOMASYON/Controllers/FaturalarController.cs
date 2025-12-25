using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


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
        public ActionResult FaturaEkle(Faturalar f) //Burada Faturalar s�n�f�ndan gelen f parametresi kullan�l�yor.
        {
            c.Faturalars.Add(f); //Faturalar s�n�f�ndan gelen f parametresini c.Faturalars'a ekliyoruz.
                c.SaveChanges(); //De�i�iklikleri kaydediyoruz.
             return RedirectToAction("Index"); //Faturalar listesini g�r�nt�lemek i�in Index metoduna y�nlendiriyoruz.                    
        }

        public ActionResult FaturaGetir (int id)
        {
            var fatura = c.Faturalars.Find(id);

            return View("FaturaGetir", fatura);
        }

        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var fatura = c.Faturalars.Find(f.Faturaid); //Faturaid ile faturay� buluyoruz.
            fatura.FaturaSeriNo = f.FaturaSeriNo; //FaturaSeriNo'yu g�ncelliyoruz.
            fatura.FaturaSiraNo = f.FaturaSiraNo; //FaturaSiraNo'yu g�ncelliyoruz.
            fatura.Saat = f.Saat; //Saati g�ncelliyoruz.
            fatura.Tarih = f.Tarih; //Tarihi g�ncelliyoruz.
            fatura.VergiDairesi = f.VergiDairesi; //VergiDairesini g�ncelliyoruz.
           
            c.SaveChanges(); //De�i�iklikleri kaydediyoruz.
            return RedirectToAction("Index"); //Faturalar listesini g�r�nt�lemek i�in Index metoduna y�nlendiriyoruz.
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
            c.FaturaKalems.Add(p); //FaturaKalem s�n�f�ndan gelen p parametresini c.FaturaKalems'e ekliyoruz.
            c.SaveChanges(); //De�i�iklikleri kaydediyoruz.
            return RedirectToAction("Index");
        }

        public ActionResult Dinamik()
        {
            Class4 cs = new Class4();
            cs.deger1 = c.Faturalars
                         .Include(x => x.FaturaKalems) // ili�kili kalemleri de y�kle
                         .ToList();

            return View(cs);
        }
        public  ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSiraNo, DateTime Tarih, string VergiDairesi, string saat, string TeslimEden , string TeslimAlan , string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSiraNo = FaturaSiraNo;
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

            return Json("��lem Ba�ar�l�");
            
        }
    }
}


