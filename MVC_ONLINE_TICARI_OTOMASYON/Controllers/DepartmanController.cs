using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class DepartmanController : Controller
    {
        // GET: Departman

        Context c = new Context();
       
        public ActionResult Index()
        {
            List<Departman> degerler = c.Departmans.Where(x => x.Durum).ToList(); //De�erleri ekrana getirmek i�in
            return View(degerler);
        }  



        //Yeni Departman Eklemek ��in
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
           
            return View();
        }
        //Yeni Departman Eklemek ��in
        [HttpPost]
       
        public ActionResult DepartmanEkle(Departman d) 
        
        {
            d.Durum = true;
            c.Departmans.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index"); 
        
        }

        //Silme i�lemi i�in
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
            //Controller dan view e veri ta��r. 
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

