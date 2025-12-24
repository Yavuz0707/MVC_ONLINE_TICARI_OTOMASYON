using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class GrafikController : Controller
    {
        // GET: Grafik
        Context c = new Context();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            var grafikciz = new Chart(600, 600);
            grafikciz.AddTitle("Kategori - Ürün Stok Sayısı")
                     .AddLegend("Stok")
                     .AddSeries("Değerler",
                                xValue: new[] { "Beyaz Eşya", "Oyuncak", "Elektronik Eşya", "Küçük Ev Aletleri" },
                                yValues: new[] { 85, 66, 23, 69 })
                     .Write();

            return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");
        }

        // Veri tabanından çekilen verilerle grafik oluşturma
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();

            var sonuclar = c.Uruns.ToList();
            sonuclar.ToList().ForEach(x=> xvalue.Add(x.UrunAd));
            sonuclar.ToList().ForEach(y=> yvalue.Add(y.Stok));

            var grafik = new Chart(800, 800).AddTitle("Stoklar")
                .AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");


            
        }

        public ActionResult Index4()
        {

            return View();

        }

        public ActionResult VisualizeUrunResult() 
        {
         return Json(Urunlistesi(), JsonRequestBehavior.AllowGet); //Google chart için json formatında veri döndürüyoruz
        }
        public List<sinif1> Urunlistesi()
        {

            List<sinif1> snf =  new List<sinif1>();
            snf.Add(new sinif1()
            {
                urunad = "Bilgisayar",
                stok = 120
            });

            snf.Add(new sinif1()
            {
                urunad = "Beyaz Eşya",
                stok = 150
            });

            snf.Add(new sinif1()
            {
                urunad = "Mobilya",
                stok = 70
            });
            snf.Add(new sinif1()
            {
                urunad = "Küçük Ev Aletleri",
                stok = 180
            });
            snf.Add(new sinif1()
            {
                urunad = "Mobil Cihazlar",
                stok = 99
            });

            return snf;
        }

        //Veri tabanından çekilen verilerle grafik oluşturma - Google Charts

        public ActionResult Index5()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult2()
        
       {

        return Json(UrunListesi2(), JsonRequestBehavior.AllowGet);

        }

        public List<sinif2>UrunListesi2()
        {

            List<sinif2> snf = new List<sinif2>();
            using (var context=new Context())
            {
                snf = c.Uruns.Select(x => new sinif2
                {
                    urn = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }

            return snf;
        }

        public ActionResult Index6()
        {
            return View();
        }

        public ActionResult Index7() { 
        return View();

        }
    }


}
