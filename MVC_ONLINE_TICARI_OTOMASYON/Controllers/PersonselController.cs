using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; // EKLENDİ: Include için gerekli
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using System.IO;
using System.Drawing;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class PersonselController : Controller

    {
        // GET: Personel

        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Personels.Include(x => x.Departman).ToList(); // DÜZELTİLDİ: Departman dahil edildi
            return View(degerler);
        }

        [HttpGet]
        public ActionResult PersonelEkle() // DÜZELTİLDİ: "PersonelEkel" → "PersonelEkle"
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
        public ActionResult PersonelEkle(Personel p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName); // Dosya adını al
                string uzanti = Path.GetExtension(Request.Files[0].FileName); // Dosya uzantısını al
                string yol = "~/Image/" + dosyaadi + uzanti; // Dosya yolu
                Request.Files[0].SaveAs(Server.MapPath(yol)); // Dosyayı kaydet
                p.PersonelGorsel = "/Image/" + dosyaadi + uzanti; // Personel görselini ayarla

            }

            c.Personels.Add(p); // Yeni personeli ekle
            c.SaveChanges(); // Değişiklikleri kaydet
            return RedirectToAction("Index"); // Liste sayfasına yönlendir
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

        public ActionResult PersonelGuncelle(Personel p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName); // Dosya adını al
                string uzanti = Path.GetExtension(Request.Files[0].FileName); // Dosya uzantısını al
                string yol = "~/Image/" + dosyaadi + uzanti; // Dosya yolu
                Request.Files[0].SaveAs(Server.MapPath(yol)); // Dosyayı kaydet
                p.PersonelGorsel = "/Image/" + dosyaadi + uzanti; // Personel görselini ayarla

            }

            var prs = c.Personels.Find(p.Personelid); // Personel ID ile personeli bul
            prs.PersonelAd = p.PersonelAd; // Adı güncelle
            prs.PersonelSoyad = p.PersonelSoyad; // Soyadı güncelle
            prs.Departmanid = p.Departmanid; // Departman ID'yi güncelle
            prs.PersonelGorsel = p.PersonelGorsel; // Görseli güncelle
            c.SaveChanges(); // Değişiklikleri kaydet
            return RedirectToAction("Index"); // Liste sayfasına yönlendir
        }

        public ActionResult PersonelListe()
        {
            var sorgu = c.Personels.Include(x => x.Departman).ToList();
            return View(sorgu);
        }


    }
}
