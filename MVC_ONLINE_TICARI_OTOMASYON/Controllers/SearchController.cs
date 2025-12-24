using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_ONLINE_TICARI_OTOMASYON.Controllers
{
    [AllowAnonymous]
    public class SearchController : Controller
    {
        // Menü öğelerini döndür
        [HttpGet]
        public JsonResult GetMenuItems(string term)
        {
            var menuItems = new List<object>
            {
                new { label = "Kategoriler", value = "Kategoriler", url = "/Kategori/Index" },
                new { label = "Ürünler", value = "Ürünler", url = "/Urun/Index" },
                new { label = "Departmanlar", value = "Departmanlar", url = "/Departman/Index" },
                new { label = "Cariler", value = "Cariler", url = "/Cari/Index" },
                new { label = "Müşteriler", value = "Müşteriler", url = "/Cari/Index" },
                new { label = "Personeller", value = "Personeller", url = "/Personel/Index" },
                new { label = "Grafikler", value = "Grafikler", url = "/Grafik/Index" },
                new { label = "Satışlar", value = "Satışlar", url = "/Satis/Index" },
                new { label = "Faturalar", value = "Faturalar", url = "/Fatura/Index" },
                new { label = "Dinamik Faturalar", value = "Dinamik Faturalar", url = "/FaturaKalem/Index" },
                new { label = "Personel Listesi", value = "Personel Listesi", url = "/Personel/Index" },
                new { label = "Giderler", value = "Giderler", url = "/Gider/Index" },
                new { label = "Kargo", value = "Kargo", url = "/Kargo/Index" },
                new { label = "PDF - EXCEL", value = "PDF - EXCEL", url = "/Pdf/Index" },
                new { label = "Alertler", value = "Alertler", url = "/Urun/UrunListesiPDF" },
                new { label = "İstatistikler", value = "İstatistikler", url = "/Istatistik/Index" },
                new { label = "Mesajlar", value = "Mesajlar", url = "/Mesaj/Index" },
                new { label = "Yeni Kargo Ekle", value = "Yeni Kargo Ekle", url = "/Kargo/YeniKargo" },
                new { label = "Yeni Ürün Ekle", value = "Yeni Ürün Ekle", url = "/Urun/YeniUrun" },
                new { label = "Yeni Kategori Ekle", value = "Yeni Kategori Ekle", url = "/Kategori/KategoriEkle" },
                new { label = "Yeni Departman Ekle", value = "Yeni Departman Ekle", url = "/Departman/DepartmanEkle" },
                new { label = "Yeni Cari Ekle", value = "Yeni Cari Ekle", url = "/Cari/YeniCari" },
                new { label = "Yeni Personel Ekle", value = "Yeni Personel Ekle", url = "/Personel/YeniPersonel" },
                new { label = "Yeni Gider Ekle", value = "Yeni Gider Ekle", url = "/Gider/YeniGider" },
                new { label = "Yeni Fatura Ekle", value = "Yeni Fatura Ekle", url = "/Fatura/FaturaEkle" }
            };

            // Term'e göre filtrele
            if (!string.IsNullOrEmpty(term))
            {
                menuItems = menuItems
                    .Where(x => x.GetType().GetProperty("label").GetValue(x, null).ToString()
                        .ToLower().Contains(term.ToLower()))
                    .ToList<object>();
            }

            return Json(menuItems, JsonRequestBehavior.AllowGet);
        }
    }
}
