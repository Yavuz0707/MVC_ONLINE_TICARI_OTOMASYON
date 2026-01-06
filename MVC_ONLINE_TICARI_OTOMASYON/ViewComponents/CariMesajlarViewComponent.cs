using Microsoft.AspNetCore.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MVC_ONLINE_TICARI_OTOMASYON.ViewComponents
{
    /// <summary>
    /// Cari panel için mesaj istatistiklerini gösteren ViewComponent
    /// Cari panel modülünde kullanılır
    /// </summary>
    public class CariMesajlarViewComponent : ViewComponent
    {
        private readonly Context _context;

        public CariMesajlarViewComponent()
        {
            _context = new Context();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mail = HttpContext.Session.GetString("CariMail");
            
            if (string.IsNullOrEmpty(mail))
            {
                return View(new List<Mesajlar>());
            }

            var mesajlar = _context.mesajlars
                .Where(x => x.Alici == mail)
                .OrderByDescending(x => x.MesajID)
                .Take(5)
                .ToList();

            return View(mesajlar);
        }
    }
}
