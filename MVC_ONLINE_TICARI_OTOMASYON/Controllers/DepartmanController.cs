using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class DepartmanController : Controller
    {
        // GET: Departman

        Context c = new Context();
       
        public ActionResult Index()
        {
            List<Departman> degerler = c.Departmans.Where(x => x.Durum).ToList(); //Değerleri ekrana getirmek için
            return View(degerler);
        }  



        //Yeni Departman Eklemek İçin
        [HttpGet]
        [Authorize(Roles = "A")] //A yetkisine sahip olan kullanıcılar bu işlemi yapabilir.
        public ActionResult DepartmanEkle()
        {
           
            return View();
        }
        //Yeni Departman Eklemek İçin
        [HttpPost]
       
        public ActionResult DepartmanEkle(Departman d) 
        
        {
            d.Durum = true;
            c.Departmans.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index"); 
        
        }

        //Silme işlemi için
        public ActionResult DepartmanSil(int id)
        
        {
            var dep = c.Departmans.Find(id);
            dep.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        
        }

        public ActionResult DepartmanGetir(int id)
        {
            var dpt = c.Departmans.Find(id);

            return View("DepartmanGetir",dpt); 
        
        }

        public ActionResult DepartmanGuncelle(Departman p)
        {
            var dept = c.Departmans.Find(p.Departmanid);
            dept.DepartmanAd = p.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personels.Where(x=>x.Departmanid==id).ToList();
            var dpt = c.Departmans.Where(x => x.Departmanid == id).Select(y => y.DepartmanAd).FirstOrDefault();
            //Controller dan view e veri taşır. 
            ViewBag.d = dpt;
            return View(degerler);
        }

        public ActionResult DepartmanPersonelSatis(int id)

        {
            List<SatisHareket> degerler = c.SatisHarekets.Where(x => x.Personelid == id).ToList();

            var per = c.Personels.Where(x => x.Personelid == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.dpers = per;
            return View(degerler);
        }


    }
}