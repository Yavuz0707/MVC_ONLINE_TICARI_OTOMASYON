using Microsoft.AspNetCore.Mvc;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_ONLINE_TICARI_OTOMASYON.ViewComponents
{
    /// <summary>
    /// Dashboard özet kartlarını gösteren ViewComponent
    /// Ana sayfa ve dashboard'larda kullanılabilir
    /// </summary>
    public class DashboardSummaryViewComponent : ViewComponent
    {
        private readonly Context _context;

        public DashboardSummaryViewComponent()
        {
            _context = new Context();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new DashboardSummaryModel
            {
                ToplamUrun = _context.Uruns.Count(),
                ToplamKategori = _context.Kategoris.Count(),
                ToplamCari = _context.Carilers.Count(),
                ToplamPersonel = _context.Personels.Count(),
                KritikStok = _context.Uruns.Count(x => x.Stok <= 20),
                ToplamSatis = _context.SatisHarekets.Count()
            };

            return View(model);
        }
    }

    // ViewComponent için model sınıfı
    public class DashboardSummaryModel
    {
        public int ToplamUrun { get; set; }
        public int ToplamKategori { get; set; }
        public int ToplamCari { get; set; }
        public int ToplamPersonel { get; set; }
        public int KritikStok { get; set; }
        public int ToplamSatis { get; set; }
    }
}
