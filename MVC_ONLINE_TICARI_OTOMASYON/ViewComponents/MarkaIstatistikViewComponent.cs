using Microsoft.AspNetCore.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_ONLINE_TICARI_OTOMASYON.ViewComponents
{
    /// <summary>
    /// Marka istatistiklerini gösteren ViewComponent
    /// İstatistik modülünde kullanılır
    /// </summary>
    public class MarkaIstatistikViewComponent : ViewComponent
    {
        private readonly Context _context;

        public MarkaIstatistikViewComponent()
        {
            _context = new Context();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sorgu = from x in _context.Uruns
                        group x by x.Marka into g
                        select new SinifGrup3
                        {
                            marka = g.Key,
                            sayi = g.Count()
                        };

            return View(sorgu.ToList());
        }
    }
}
