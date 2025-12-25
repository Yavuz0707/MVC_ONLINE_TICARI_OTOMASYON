#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class PersonselController : Controller

    {
        // GET: Personel

        Context c = new Context();
        private readonly IWebHostEnvironment _environment;

        public PersonselController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        
        public ActionResult Index()
        {
            var degerler = c.Personels.Include(x => x.Departman).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult PersonelEkle() // D�ZELT�LD�: "PersonelEkel" � "PersonelEkle"
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(Personel p, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string dosyaadi = Path.GetFileNameWithoutExtension(file.FileName);
                string uzanti = Path.GetExtension(file.FileName);
                string fileName = dosyaadi + uzanti;
                string wwwRootPath = _environment.WebRootPath;
                string path = Path.Combine(wwwRootPath, "Image", fileName);
                
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                
                p.PersonelGorsel = "/Image/" + fileName;
            }

            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir (int id)
        {

            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var prs = c.Personels.Find(id);
            return View("PersonelGetir",prs);
        }

        public ActionResult PersonelGuncelle(Personel p, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string dosyaadi = Path.GetFileNameWithoutExtension(file.FileName);
                string uzanti = Path.GetExtension(file.FileName);
                string fileName = dosyaadi + uzanti;
                string wwwRootPath = _environment.WebRootPath;
                string path = Path.Combine(wwwRootPath, "Image", fileName);
                
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                
                p.PersonelGorsel = "/Image/" + fileName;
            }

            var prs = c.Personels.Find(p.Personelid);
            prs.PersonelAd = p.PersonelAd;
            prs.PersonelSoyad = p.PersonelSoyad;
            prs.Departmanid = p.Departmanid;
            prs.PersonelGorsel = p.PersonelGorsel; // G�rseli g�ncelle
            c.SaveChanges(); // De�i�iklikleri kaydet
            return RedirectToAction("Index"); // Liste sayfas�na y�nlendir
        }

        public ActionResult PersonelListe()
        {
            var sorgu = c.Personels.Include(x => x.Departman).ToList();
            return View(sorgu);
        }


    }
}



