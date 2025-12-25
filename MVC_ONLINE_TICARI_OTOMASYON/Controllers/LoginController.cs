using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Partial1(Cariler p) 
        {
            c.Carilers.Add(p);
            c.SaveChanges();
            return PartialView();

        }

        [HttpGet]
        public ActionResult CariLogin1()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CariLogin1(Cariler p)
        {

            var bilgiler = c.Carilers.FirstOrDefault(x => x.CariMail == p.CariMail && x.CariSifre == p.CariSifre);
            if(bilgiler != null)
            {
                // ASP.NET Core Authentication
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, bilgiler.CariMail),
                    new Claim(ClaimTypes.Role, "Cari")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                
                HttpContext.Session.SetString("CariMail", bilgiler.CariMail);
                return RedirectToAction("Index", "CariPanel");  

            }
            else { 
                TempData["HataMesaji"] = "Hatalı giriş! E-mail veya şifre yanlış. Lütfen tekrar deneyiniz.";
                return RedirectToAction("Index", "Login");

            }
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdminLogin(Admin p)
        {
            if (string.IsNullOrEmpty(p.KullaniciAd) || string.IsNullOrEmpty(p.Sifre))
            {
                TempData["HataMesaji"] = "Kullanıcı adı ve şifre boş bırakılamaz!";
                return RedirectToAction("Index", "Login");
            }

            var bilgiler = c.Admins.FirstOrDefault(x => x.KullaniciAd == p.KullaniciAd && x.Sifre == p.Sifre);
            if(bilgiler != null)
            {
                // ASP.NET Core Authentication
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, bilgiler.KullaniciAd),
                    new Claim(ClaimTypes.Role, bilgiler.Yetki)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                
                HttpContext.Session.SetString("KullaniciAd", bilgiler.KullaniciAd);
                HttpContext.Session.SetString("Yetki", bilgiler.Yetki);
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                TempData["HataMesaji"] = "Hatalı giriş! Kullanıcı adı veya şifre yanlış. Lütfen tekrar deneyiniz.";
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<ActionResult> Logout()
        {
            // ASP.NET Core Sign Out
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                c?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


