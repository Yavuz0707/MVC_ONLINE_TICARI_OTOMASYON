using Microsoft.AspNetCore.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_ONLINE_TICARI_OTOMASYON.ViewComponents
{
    /// <summary>
    /// Cariler listesini gösteren ViewComponent
    /// İstatistik ve cari panel modüllerinde kullanılır
    /// </summary>
    public class CarilerListViewComponent : ViewComponent
    {
        private readonly Context _context;

        public CarilerListViewComponent()
        {
            _context = new Context();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cariler = _context.Carilers.ToList();
            return View(cariler);
        }
    }
}
