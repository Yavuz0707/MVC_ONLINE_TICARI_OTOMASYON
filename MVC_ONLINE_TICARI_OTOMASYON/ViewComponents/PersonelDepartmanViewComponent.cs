using Microsoft.AspNetCore.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_ONLINE_TICARI_OTOMASYON.ViewComponents
{
    /// <summary>
    /// Personel departman istatistiklerini gösteren ViewComponent
    /// İstatistik modülünde kullanılır
    /// </summary>
    public class PersonelDepartmanViewComponent : ViewComponent
    {
        private readonly Context _context;

        public PersonelDepartmanViewComponent()
        {
            _context = new Context();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sorgu = from x in _context.Personels
                        group x by x.Departman.DepartmanAd into g
                        select new SinifGrup2
                        {
                            Departman = g.Key,
                            Sayi = g.Count()
                        };

            return View(sorgu.ToList());
        }
    }
}
