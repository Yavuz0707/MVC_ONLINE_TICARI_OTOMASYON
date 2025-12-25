using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    /// <summary>
    /// Tüm controller'lar için ortak base sınıf
    /// Session kontrolü, kullanıcı bilgileri ve dispose işlemlerini otomatik yapar
    /// NOT: [Authorize] her controller'a ayrı ayrı eklenmeli
    /// </summary>
    public class BaseController : Controller
    {
        protected Context c = new Context();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                // Kullanıcı bilgilerini ViewBag'e yükle
                if (User.Identity.IsAuthenticated)
                {
                    ViewBag.KullaniciAd = User.Identity.Name;
                    
                    // Session'dan bilgileri al
                    if (HttpContext.Session.GetString("KullaniciAd") != null)
                    {
                        ViewBag.KullaniciAd = HttpContext.Session.GetString("KullaniciAd");
                        ViewBag.Yetki = HttpContext.Session.GetString("Yetki");
                    }
                    else if (HttpContext.Session.GetString("CariMail") != null)
                    {
                        ViewBag.KullaniciAd = HttpContext.Session.GetString("CariMail");
                        ViewBag.IsCari = true;
                    }
                    else
                    {
                        // Session yoksa veritabanından yükle
                        var admin = c.Admins.FirstOrDefault(x => x.KullaniciAd == User.Identity.Name);
                        if (admin != null)
                        {
                            HttpContext.Session.SetString("KullaniciAd", admin.KullaniciAd);
                            HttpContext.Session.SetString("Yetki", admin.Yetki);
                            ViewBag.KullaniciAd = admin.KullaniciAd;
                            ViewBag.Yetki = admin.Yetki;
                        }
                    }
                }
                
                // Tarih format ayarları
                ViewBag.CurrentDate = DateTime.Now;
                ViewBag.CurrentYear = DateTime.Now.Year;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BaseController Error: {ex.Message}");
            }
            
            base.OnActionExecuting(filterContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                c?.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Başarı mesajı göster
        /// </summary>
        protected ActionResult ShowSuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Hata mesajı göster
        /// </summary>
        protected ActionResult ShowErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Bilgi mesajı göster
        /// </summary>
        protected ActionResult ShowInfoMessage(string message)
        {
            TempData["InfoMessage"] = message;
            return RedirectToAction("Index");
        }
    }
}



