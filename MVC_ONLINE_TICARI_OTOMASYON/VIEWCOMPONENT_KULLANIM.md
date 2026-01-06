# 📖 ViewComponent Kullanım Rehberi

Bu dokümantasyon, projedeki ViewComponent'lerin kullanımını açıklar.

## 🎯 ViewComponent Nedir?

ViewComponent'ler, ASP.NET Core MVC'de yeniden kullanılabilir UI bileşenleri oluşturmak için kullanılır. Partial View'lardan daha güçlü ve esnek bir yapı sunarlar.

### ✨ Avantajları

- **Bağımsız İş Mantığı**: Kendi iş mantığını içerir
- **Test Edilebilir**: Birim testler yazılabilir
- **Asenkron Çalışma**: Async/await desteği
- **Yeniden Kullanılabilir**: Farklı view'larda kullanılabilir
- **Model Binding**: Parametre alabilir

---

## 📦 Proje ViewComponent'leri

Projede aşağıdaki ViewComponent'ler geliştirilmiştir:

### 1️⃣ PersonelDepartmanViewComponent

**Konum:** `ViewComponents/PersonelDepartmanViewComponent.cs`

**Açıklama:** Personel departman istatistiklerini gösterir.

**Kullanım:**
```razor
@await Component.InvokeAsync("PersonelDepartman")
```

**Kullanıldığı Sayfalar:**
- İstatistik / Kolay Tablolar

---

### 2️⃣ CarilerListViewComponent

**Konum:** `ViewComponents/CarilerListViewComponent.cs`

**Açıklama:** Tüm cariler listesini tablo formatında gösterir.

**Kullanım:**
```razor
@await Component.InvokeAsync("CarilerList")
```

**Kullanıldığı Sayfalar:**
- İstatistik / Kolay Tablolar

---

### 3️⃣ UrunlerListViewComponent

**Konum:** `ViewComponents/UrunlerListViewComponent.cs`

**Açıklama:** Ürünler listesini detaylı tablo formatında gösterir.

**Kullanım:**
```razor
@await Component.InvokeAsync("UrunlerList")
```

**Kullanıldığı Sayfalar:**
- İstatistik / Kolay Tablolar

---

### 4️⃣ MarkaIstatistikViewComponent

**Konum:** `ViewComponents/MarkaIstatistikViewComponent.cs`

**Açıklama:** Marka bazlı ürün istatistiklerini gösterir.

**Kullanım:**
```razor
@await Component.InvokeAsync("MarkaIstatistik")
```

**Kullanıldığı Sayfalar:**
- İstatistik / Kolay Tablolar

---

### 5️⃣ CariMesajlarViewComponent

**Konum:** `ViewComponents/CariMesajlarViewComponent.cs`

**Açıklama:** Cari panelinde kullanıcının son mesajlarını gösterir.

**Kullanım:**
```razor
@await Component.InvokeAsync("CariMesajlar")
```

**Kullanıldığı Sayfalar:**
- Cari Panel / Index

---

### 6️⃣ DashboardSummaryViewComponent

**Konum:** `ViewComponents/DashboardSummaryViewComponent.cs`

**Açıklama:** Dashboard için özet istatistik kartlarını gösterir.

**Kullanım:**
```razor
@await Component.InvokeAsync("DashboardSummary")
```

**Örnek Kullanım Alanları:**
- Dashboard / Ana Sayfa
- Admin Panel

---

## 🔧 Yeni ViewComponent Oluşturma

### Adım 1: ViewComponent Sınıfı Oluşturma

```csharp
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MVC_ONLINE_TICARI_OTOMASYON.ViewComponents
{
    public class OrneKViewComponent : ViewComponent
    {
        private readonly Context _context;

        public OrnekViewComponent()
        {
            _context = new Context();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = _context.Uruns.ToList();
            return View(data);
        }
    }
}
```

### Adım 2: View Dosyası Oluşturma

Konum: `Views/Shared/Components/Ornek/Default.cshtml`

```razor
@model List<Urun>

<div class="card">
    <div class="card-header">
        <h3>Örnek Başlık</h3>
    </div>
    <div class="card-body">
        @foreach(var item in Model)
        {
            <p>@item.UrunAd</p>
        }
    </div>
</div>
```

### Adım 3: View'da Kullanma

```razor
@await Component.InvokeAsync("Ornek")
```

---

## 📊 ViewComponent vs Partial View

| Özellik | ViewComponent | Partial View |
|---------|--------------|--------------|
| İş Mantığı | ✅ İçerir | ❌ İçermez |
| Asenkron | ✅ Destekler | ⚠️ Sınırlı |
| Test Edilebilirlik | ✅ Kolay | ⚠️ Zor |
| Performans | ✅ İyi | ✅ İyi |
| HTTP İsteği | ❌ Yapamaz | ❌ Yapamaz |

---

## 💡 Best Practices

1. **Tek Sorumluluk İlkesi:** Her ViewComponent tek bir görevi yapmalı
2. **Asenkron Kullanım:** Veritabanı işlemlerinde `async/await` kullanın
3. **Model Kullanımı:** Karmaşık veriler için model sınıfları oluşturun
4. **Cache:** Sık kullanılan veriler için caching düşünün
5. **Dependency Injection:** Servis kullanımında DI tercih edin

---

## 🎨 Parametre ile ViewComponent Kullanımı

### ViewComponent:
```csharp
public async Task<IViewComponentResult> InvokeAsync(int categoryId)
{
    var products = _context.Uruns.Where(x => x.Kategoriid == categoryId).ToList();
    return View(products);
}
```

### View'da Kullanım:
```razor
@await Component.InvokeAsync("UrunlerByCategory", new { categoryId = 5 })
```

---

## 🔍 Hata Ayıklama

ViewComponent çalışmıyorsa kontrol edin:

1. ✅ Sınıf adı `ViewComponent` ile bitiyor mu?
2. ✅ `InvokeAsync` metodu mevcut mu?
3. ✅ View dosyası doğru klasörde mi? (`Views/Shared/Components/{Name}/Default.cshtml`)
4. ✅ `_ViewImports.cshtml` doğru yapılandırılmış mı?

---

## 📚 Ek Kaynaklar

- [Microsoft Docs - ViewComponent](https://docs.microsoft.com/aspnet/core/mvc/views/view-components)
- [ASP.NET Core Best Practices](https://docs.microsoft.com/aspnet/core/fundamentals/best-practices)

---

**Geliştirici Notu:** Tüm ViewComponent'ler modern ASP.NET Core standartlarına uygun olarak geliştirilmiştir.
