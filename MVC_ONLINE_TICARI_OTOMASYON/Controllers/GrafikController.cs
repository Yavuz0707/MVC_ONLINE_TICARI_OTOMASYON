using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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
            // TODO: Chart sınıfı .NET Core'da yok. Chart.js veya başka bir kütüphane kullanılmalı
            // var grafikciz = new Chart(600, 600);
            return View();
        }

        // Veri tabanından çekilen verilerle grafik oluşturma
        public ActionResult Index3()
        {
            // TODO: Chart sınıfı .NET Core'da yok. Chart.js veya başka bir kütüphane kullanılmalı
            return View();
        }

        public ActionResult Index4()
        {

            return View();

        }

        public ActionResult VisualizeUrunResult() 
        {
         return Json(Urunlistesi()); //Google chart i�in json format�nda veri d�nd�r�yoruz
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
                urunad = "Beyaz E�ya",
                stok = 150
            });

            snf.Add(new sinif1()
            {
                urunad = "Mobilya",
                stok = 70
            });
            snf.Add(new sinif1()
            {
                urunad = "K���k Ev Aletleri",
                stok = 180
            });
            snf.Add(new sinif1()
            {
                urunad = "Mobil Cihazlar",
                stok = 99
            });

            return snf;
        }

        //Veri taban�ndan �ekilen verilerle grafik olu�turma - Google Charts

        public ActionResult Index5()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult2()
        
       {

        return Json(UrunListesi2());

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



