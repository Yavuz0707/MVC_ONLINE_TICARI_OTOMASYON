using Microsoft.AspNetCore.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_ONLINE_TICARI_OTOMASYON.ViewComponents
{
    /// <summary>
    /// Ürünler listesini gösteren ViewComponent
    /// İstatistik modülünde kullanılır
    /// </summary>
    public class UrunlerListViewComponent : ViewComponent
    {
        private readonly Context _context;

        public UrunlerListViewComponent()
        {
            _context = new Context();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var urunler = _context.Uruns.ToList();
            return View(urunler);
        }
    }
}
