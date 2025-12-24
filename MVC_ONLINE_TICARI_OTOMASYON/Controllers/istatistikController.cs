using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class istatistikController : Controller
    {
        // GET: istatistik
        Context c =  new Context();
        public ActionResult Index()
        {

            var deger1 = c.Carilers.Count().ToString(); //Toplam müşteri sayısını alır.
            ViewBag.d1 = deger1; //Toplam müşteri sayısını ViewBag'e atar.
            var deger2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3 = c.Personels.Count().ToString();
            ViewBag.d3 = deger3;
            var deger4 = c.Kategoris.Count().ToString();
            ViewBag.d4 = deger4;
            var deger5 = c.Uruns.Sum(x => x.Stok).ToString(); //Toplam stok sayısını alır.
            ViewBag.d5 = deger5;
            var deger6 = (from x in c.Uruns select x.Marka).Distinct().Count().ToString(); //Farklı marka sayısını alır.Tekrarsız olarak.
            ViewBag.d6 = deger6;
            var deger7 = c.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.d7 = deger7;
            var deger8 = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault(); //En yüksek satış fiyatına sahip ürünün adını alır.
            ViewBag.d8 = deger8;
            var deger9 = (from x in c.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = deger9;
            var deger10 = c.Uruns.Count(x => x.UrunAd == "Buz Dolabı").ToString();
            ViewBag.d10 = deger10;
            var deger11 = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d11 = deger11;
            var deger12 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault(); //En çok ürüne sahip markayı alır.
            ViewBag.d12 = deger12;

            var deger13 = c.Uruns.Where(u => u.Urunid == (c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault(); //En çok satılan ürünün ID'sini alır.
            ViewBag.d13 = deger13;
            var deger14 = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString(); //Toplam satış tutarını alır.
            ViewBag.d14 = deger14;

            DateTime bugün = DateTime.Today; //Bugünün tarihini alır.
            var deger15 = c.SatisHarekets.Count(x => x.Tarih == bugün).ToString(); //Bugün yapılan satış sayısını alır.
            ViewBag.d15 = deger15;

            var deger16 = c.SatisHarekets
               .Where(x => x.Tarih == bugün)
               .Sum(y => (decimal?)y.ToplamTutar) ?? 0;
            ViewBag.d16 = deger16.ToString();


            return View();
        }
        public ActionResult KolayTablolar()
        {
            var sorgu = from x in c.Carilers
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };

            return View(sorgu.ToList()); // ✅ ToList() ile View'a model olarak gönderiyoruz
        }

        public PartialViewResult Partial1()
        {
            var sorgu2 = from x in c.Personels
                         group x by x.Departman.DepartmanAd into g
                         select new SinifGrup2
                         {
                             Departman = g.Key, // Departman ID'sini alır
                             Sayi = g.Count()  // Personel sayısını alır
                         };

            return PartialView(sorgu2.ToList());
        }

        public PartialViewResult Partial2()
        {

            var sorgu = c.Carilers.ToList();


            return PartialView(sorgu);
        }

        public PartialViewResult Partial3()
        {             var sorgu = c.Uruns.ToList();
            return PartialView(sorgu);
        }

        public PartialViewResult Partial4()
        {
            var sorgu = from x in c.Uruns
                         group x by x.Marka into g
                         select new SinifGrup3
                         {
                             marka = g.Key, // Departman ID'sini alır
                             sayi = g.Count()  // Personel sayısını alır
                         };

            return PartialView(sorgu.ToList());
        }


    }
}