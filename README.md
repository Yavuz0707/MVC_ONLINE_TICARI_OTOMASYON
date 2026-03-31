# 🏢 MVC Online Ticari Otomasyon Sistemi

Modern ve kapsamlı bir ticari otomasyon web uygulaması. E-ticaret, stok yönetimi, müşteri takibi, satış işlemlerini yönetmek için geliştirilmiş profesyonel bir çözüm.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?style=for-the-badge&logo=.net)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-6.5.1-512BD4?style=for-the-badge)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.2.3-7952B3?style=for-the-badge&logo=bootstrap)
![ViewComponent](https://img.shields.io/badge/ViewComponent-✓-success?style=for-the-badge)
![Tag Helpers](https://img.shields.io/badge/Tag_Helpers-✓-success?style=for-the-badge)

---

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [ViewComponent Desteği](#-viewcomponent-desteği)
- [Teknolojiler](#-teknolojiler)
- [Son Güncellemeler](#-son-güncellemeler)
- [Sistem Gereksinimleri](#-sistem-gereksinimleri)
- [Kurulum](#-kurulum)
- [Veritabanı Yapılandırması](#-veritabanı-yapılandırması)
- [Kullanım](#-kullanım)
- [Proje Yapısı](#-proje-yapısı)
- [Dokümantasyon](#-dokümantasyon)

---

## ✨ Özellikler

### 🎯 Temel Özellikler

- ✅ **Ürün Yönetimi**: Kapsamlı ürün ekleme, düzenleme, silme ve listeleme işlemleri.
- ✅ **Stok Takibi**: Gerçek zamanlı stok durumu izleme ve uyarı sistemi
- ✅ **Kategori Yönetimi**: Hiyerarşik kategori yapısı ve organizasyon
- ✅ **Müşteri (Cari) Yönetimi**: Detaylı müşteri bilgileri ve profil yönetimi
- ✅ **Müşteri Kayıt Sistemi**: Ana sayfadan yeni müşteri kaydı oluşturma
- ✅ **Satış Takibi**: Detaylı satış raporları ve Foreign Key kontrolü ile güvenli işlem
- ✅ **Personel Yönetimi**: Çalışan bilgileri ve yetki kontrolü
- ✅ **Departman Organizasyonu**: Departman bazlı personel yönetimi

### 📊 Raporlama ve Analiz

- 📈 **Dinamik Grafikler**: Chart.js ile interaktif grafik görünümleri
- 📉 **Satış İstatistikleri**: Günlük, haftalık, aylık satış analizleri
- 📊 **Stok Raporları**: Kritik stok seviyesi uyarıları
- 💰 **Finansal Raporlar**: Gelir-gider analizi ve kar/zarar hesaplamaları
- 📋 **Ciro Takibi**: Müşteri bazlı ciro raporları

---

## 🚀 Son Güncellemeler (Ocak 2026)

### 🔧 Hata Düzeltmeleri ve İyileştirmeler

- ✅ **Satış Modülü Düzeltildi**: 
  - Foreign Key kontrolü eklendi (Cari, Ürün, Personel doğrulama)
  - DbUpdateException hata yönetimi geliştirildi
  - Detaylı hata mesajları ve kullanıcı bildirimleri
  - Try-Catch blokları ile güvenli işlem

- ✅ **View Klasör Yapısı Düzeltildi**:
  - `Views\Satıs` → `Views\Satis` (Encoding sorunu çözüldü)
  - Controller-View eşleşmesi sağlandı
  - Türkçe karakter sorunları giderildi

- ✅ **Müşteri Kayıt Sistemi**:
  - Ana giriş sayfasından "Kayıt Ol" özelliği aktif edildi
  - Partial View yapısı düzeltildi
  - E-mail tekrar kontrolü eklendi
  - SweetAlert2 ile başarı/hata bildirimleri

- ✅ **Menü ve Navigasyon İyileştirmeleri**:
  - Satışlar linki düzeltildi (`/Satis/Index`)
  - Kullanılmayan menü öğeleri kaldırıldı (Faturalar, Galeri, Personel Listesi)
  - Daha temiz ve kullanışlı menü yapısı

- ✅ **Encoding ve Karakter Sorunları**:
  - Türkçe karakterler (ı, ş, ğ, ü, ö, ç) düzeltildi
  - UTF-8 encoding standardına geçiş
  - Controller ve View dosyaları temizlendi

---

## 🧩 ViewComponent Desteği

Proje modern ASP.NET Core ViewComponent yapısını tam olarak desteklemektedir.

### 🎨 Kullanılan ViewComponent'ler

1. **PersonelDepartmanViewComponent** - Departman personel istatistikleri
2. **CarilerListViewComponent** - Müşteri listesi görüntüleme
3. **UrunlerListViewComponent** - Ürün listesi görüntüleme
4. **MarkaIstatistikViewComponent** - Marka bazlı istatistikler
5. **CariMesajlarViewComponent** - Müşteri mesaj yönetimi
6. **DashboardSummaryViewComponent** - Dashboard özet kartları

### 📚 ViewComponent Dokümantasyonu

Detaylı kullanım için:
- [ViewComponent Kullanım Rehberi](MVC_ONLINE_TICARI_OTOMASYON/VIEWCOMPONENT_KULLANIM.md)
- [ViewComponent Uygulama Raporu](MVC_ONLINE_TICARI_OTOMASYON/VIEWCOMPONENT_UYGULAMA_RAPORU.md)

### 🔔 Bildirim Sistemi

- 🔕 **Gerçek Zamanlı Bildirimler**: Önemli olaylar için anlık bildirimler
- 📧 **Mesajlaşma Sistemi**: Dahili mesajlaşma ve iletişim modülü
- ⚠️ **Uyarı Sistemi**: Kritik stok, ödeme ve görev bildirimleri (SweetAlert2)
- 📅 **Görev Yönetimi**: Yapılacaklar listesi ve takip sistemi

### 🎨 Kullanıcı Arayüzü

- 🖥️ **Responsive Tasarım**: Tüm cihazlarda uyumlu görünüm
- 🎭 **Modern UI/UX**: AdminLTE 3.0.4 ile profesyonel arayüz
- 📱 **Mobil Uyumlu**: Mobil cihazlar için optimize edilmiş
- 🔍 **Gelişmiş Arama**: DataTables ile güçlü filtreleme ve arama
- 🧩 **ViewComponent**: Yeniden kullanılabilir UI bileşenleri
- 🏷️ **Tag Helpers**: ASP.NET Core Tag Helper desteği
- 🎨 **Custom HTML Helpers**: Özel HTML helper metodları
- 💬 **SweetAlert2**: Modern pop-up bildirimler

### 🔐 Güvenlik ve Yetkilendirme

- 🔒 **Rol Bazlı Yetkilendirme**: Admin, Personel, Müşteri rolleri
- 🛡️ **Güvenli Oturum Yönetimi**: Session ve Cookie güvenliği
- 🔑 **Şifre Koruması**: Güvenli şifreleme algoritmaları
- 📝 **Denetim İzleri**: Tüm işlemler için log kaydı
- 🚫 **XSS ve CSRF Koruması**: Anti-Forgery Token kullanımı
- ✅ **Foreign Key Validation**: Veri bütünlüğü kontrolü

### 🚀 Performans Özellikleri

- ⚡ **Hızlı Yükleme**: Optimize edilmiş veritabanı sorguları
- 💾 **Entity Framework 6.5.1**: Code First yaklaşımı
- 📦 **Lazy Loading**: İhtiyaç anında veri yükleme
- 🗜️ **Sıkıştırma**: CSS/JS dosyalarının minify edilmesi
- 🔄 **Server-Side Paging**: PagedList ile sayfalama
- 🔄 **Asenkron İşlemler**: AJAX ile sayfa yenilemesiz işlemler

---

## 🛠 Teknolojiler

### Backend Teknolojileri

| Teknoloji | Versiyon | Açıklama |
|-----------|----------|----------|
| ![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet) | 9.0 | Ana framework |
| ![ASP.NET](https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4) | MVC | Web framework |
| ![Entity Framework](https://img.shields.io/badge/EF_Core-6.5.1-512BD4) | 6.5.1 | ORM |
| ![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp) | 12.0 | Programlama dili |

### Frontend Teknolojileri

| Teknoloji | Versiyon | Açıklama |
|-----------|----------|----------|
| ![Bootstrap](https://img.shields.io/badge/Bootstrap-5.2.3-7952B3?logo=bootstrap) | 5.2.3 | CSS Framework |
| ![ViewComponent](https://img.shields.io/badge/ViewComponent-Implemented-success) | - | Reusable UI Components |
| ![jQuery](https://img.shields.io/badge/jQuery-3.7.0-0769AD?logo=jquery) | 3.7.0 | JavaScript Library |
| ![AdminLTE](https://img.shields.io/badge/AdminLTE-3.0.4-3c8dbc) | 3.0.4 | Admin Template |
| ![DataTables](https://img.shields.io/badge/DataTables-1.10.15-3c8dbc) | 1.10.15 | Table Plugin |
| ![Chart.js](https://img.shields.io/badge/Chart.js-Latest-FF6384?logo=chartdotjs) | Latest | Grafik Kütüphanesi |

### Ek Kütüphaneler ve Araçlar

- **PagedList.Mvc**: Sayfalama işlemleri için (v4.5.0)
- **Newtonsoft.Json**: JSON işlemleri için (v13.0.3)
- **QRCoder**: QR kod oluşturma için
- **Font Awesome**: İkon kütüphanesi
- **SweetAlert2**: Modern alert mesajları
- **Select2**: Gelişmiş select kutuları
- **Summernote**: Zengin metin editörü

---

## 💻 Sistem Gereksinimleri

### Minimum Gereksinimler

- **İşletim Sistemi**: Windows 10/11, Linux, macOS
- **Framework**: .NET 9.0 SDK veya üzeri
- **Veritabanı**: SQL Server 2016 veya üzeri (LocalDB destekli)
- **RAM**: En az 4 GB
- **Disk Alanı**: En az 500 MB boş alan
- **Tarayıcı**: Chrome 90+, Firefox 88+, Edge 90+, Safari 14+

### Önerilen Gereksinimler

- **İşletim Sistemi**: Windows 11 Pro / Ubuntu 22.04 LTS
- **Framework**: .NET 9.0 SDK (Latest)
- **Veritabanı**: SQL Server 2022 / SQL Server Express
- **RAM**: 8 GB veya üzeri
- **Disk Alanı**: 1 GB boş alan
- **IDE**: Visual Studio 2022 / Visual Studio Code / JetBrains Rider

---

## 📦 Kurulum

### 1. Projeyi Klonlama

```bash
git clone https://github.com/Yavuz0707/MVC_ONLINE_TICARI_OTOMASYON.git
cd MVC_ONLINE_TICARI_OTOMASYON
```

### 2. Bağımlılıkları Yükleme

#### NuGet Paketlerini Geri Yükleme

```bash
cd MVC_ONLINE_TICARI_OTOMASYON
dotnet restore
```

#### Alternatif: Visual Studio ile

1. Solution'ı Visual Studio'da açın
2. **Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution**
3. **Restore** butonuna tıklayın

### 3. Veritabanı Kurulumu

#### Connection String Yapılandırması

`appsettings.json` dosyasını düzenleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MvcOnlineTicariDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

#### Migration İşlemleri

```bash
# Package Manager Console'da
Update-Database

# veya .NET CLI ile
dotnet ef database update
```

### 4. Projeyi Çalıştırma

#### Visual Studio ile

1. **F5** tuşuna basın veya **IIS Express** seçerek çalıştırın
2. Tarayıcı otomatik olarak açılacaktır

#### .NET CLI ile

```bash
dotnet run --project MVC_ONLINE_TICARI_OTOMASYON
```

Uygulama varsayılan olarak `https://localhost:5001` adresinde çalışacaktır.

### 5. İlk Kullanıcı Oluşturma

Uygulama ilk çalıştırıldığında, aşağıdaki varsayılan admin hesabı oluşturulur:

- **Kullanıcı Adı**: admin
- **Şifre**: admin123

> ⚠️ **Güvenlik Uyarısı**: İlk girişten sonra varsayılan şifreyi mutlaka değiştirin!

---

## 🗄 Veritabanı Yapılandırması

### Veritabanı Tabloları

Sistem aşağıdaki ana tabloları içerir:

- **Admin**: Yönetici kullanıcıları
- **Cariler**: Müşteri bilgileri
- **Departman**: Departman tanımları
- **FaturaKalem**: Fatura kalemleri
- **Faturalar**: Fatura başlıkları
- **Gider**: Gider kayıtları
- **Kargo**: Kargo takip bilgileri
- **Kategori**: Ürün kategorileri
- **Personel**: Personel bilgileri
- **SatisHareket**: Satış hareketleri
- **Urunler**: Ürün bilgileri
- **Yapilacak**: Görev listesi

### Entity Framework Migrations

Yeni migration oluşturma:

```bash
# Package Manager Console
Add-Migration MigrationAdi

# .NET CLI
dotnet ef migrations add MigrationAdi
```

Migration uygulama:

```bash
# Package Manager Console
Update-Database

# .NET CLI
dotnet ef database update
```

Migration geri alma:

```bash
# Package Manager Console
Update-Database -Migration PreviousMigration

# .NET CLI
dotnet ef database update PreviousMigration
```

---

## 🎮 Kullanım

### Admin Paneli

1. **Giriş Yapma**: `/Login/Index` sayfasından giriş yapın
2. **Dashboard**: Ana sayfada özet istatistikler görüntülenir
3. **Ürün Yönetimi**: Ürün ekleme, düzenleme ve silme işlemleri
4. **Stok Takibi**: Mevcut stok durumu ve uyarılar
5. **Fatura İşlemleri**: Alış/satış faturası oluşturma
6. **Raporlar**: Detaylı satış ve stok raporları

### Müşteri Paneli

1. **Giriş**: `/CariPanel/Index` üzerinden giriş
2. **Profil**: Profil bilgilerini görüntüleme ve güncelleme
3. **Siparişler**: Geçmiş siparişleri görüntüleme
4. **Mesajlar**: Sistem mesajlarını okuma ve yanıtlama
5. **Faturalar**: Fatura geçmişini görüntüleme

### Public Alan

- **Ana Sayfa**: `/Public/Home` - Genel tanıtım sayfası
- **Hakkımızda**: Şirket hakkında bilgiler
- **İletişim**: İletişim formu
- **Ürün Kataloğu**: Halka açık ürün listesi

---

## 📁 Proje Yapısı

```
MVC_ONLINE_TICARI_OTOMASYON/
│
├── 📂 Areas/                          # Area yapısı (Public, Admin vb.)
│   └── 📂 Public/
│       ├── 📂 Controllers/
│       ├── 📂 Models/
│       └── 📂 Views/
│
├── 📂 Controllers/                    # Ana controller'lar
│   ├── AlertController.cs            # Bildirim yönetimi
│   ├── CariController.cs             # Müşteri işlemleri
│   ├── DepartmanController.cs        # Departman yönetimi
│   ├── FaturalarController.cs        # Fatura işlemleri
│   ├── GrafikController.cs           # Grafik ve raporlar
│   ├── HomeController.cs             # Ana sayfa
│   ├── KargoController.cs            # Kargo takibi
│   ├── KategoriController.cs         # Kategori yönetimi
│   ├── LoginController.cs            # Giriş/Çıkış
│   ├── MesajController.cs            # Mesajlaşma
│   ├── PersonselController.cs        # Personel yönetimi
│   ├── SatışController.cs            # Satış işlemleri
│   ├── UrunController.cs             # Ürün yönetimi
│   └── istatistikController.cs       # İstatistikler
│
├── 📂 Models/                         # Model sınıfları
│   └── 📂 Siniflar/
│       ├── Admin.cs                  # Admin modeli
│       ├── Cariler.cs                # Müşteri modeli
│       ├── Context.cs                # DbContext
│       ├── Departman.cs              # Departman modeli
│       ├── FaturaKalem.cs            # Fatura kalem modeli
│       ├── Faturalar.cs              # Fatura modeli
│       ├── Kategori.cs               # Kategori modeli
│       ├── Personel.cs               # Personel modeli
│       ├── SatisHareket.cs           # Satış hareketi
│       └── Urunler.cs                # Ürün modeli
│
├── 📂 Views/                          # View dosyaları
│   ├── 📂 Shared/                    # Paylaşılan view'lar
│   │   ├── _Layout.cshtml            # Ana layout
│   │   ├── AdminLayout.cshtml        # Admin layout
│   │   └── _CariLayout.cshtml        # Müşteri layout
│   ├── 📂 Cari/                      # Müşteri view'ları
│   ├── 📂 Urun/                      # Ürün view'ları
│   ├── 📂 Faturalar/                 # Fatura view'ları
│   └── 📂 Grafik/                    # Grafik view'ları
│
├── 📂 Helpers/                        # Yardımcı sınıflar
│   ├── HtmlHelperExtensions.cs       # Custom HTML Helper'lar
│   └── FileHelper.cs                 # Dosya işlemleri
│
├── 📂 TagHelpers/                     # Custom Tag Helper'lar
│   └── CustomTagHelpers.cs           # Email, Phone, Card tag helper'lar
│
├── 📂 ViewComponents/                 # ViewComponent sınıfları
│   ├── PersonelDepartmanViewComponent.cs     # Personel departman istatistikleri
│   ├── CarilerListViewComponent.cs           # Cariler listesi
│   ├── UrunlerListViewComponent.cs           # Ürünler listesi
│   ├── MarkaIstatistikViewComponent.cs       # Marka istatistikleri
│   ├── CariMesajlarViewComponent.cs          # Cari mesajları
│   └── DashboardSummaryViewComponent.cs      # Dashboard özet kartları
│
├── 📂 wwwroot/                        # Statik dosyalar
│   ├── 📂 AdminLTE-3.0.4/            # AdminLTE tema dosyaları
│   ├── 📂 Content/                   # CSS dosyaları
│   ├── 📂 Scripts/                   # JavaScript dosyaları
│   ├── 📂 Image/                     # Resim dosyaları
│   │   └── 📂 urunler/               # Ürün resimleri
│   └── 📂 uploads/                   # Yüklenen dosyalar
│
├── 📂 Migrations/                     # EF Core migration'lar
├── 📄 Program.cs                      # Uygulama başlangıcı
├── 📄 appsettings.json               # Yapılandırma dosyası
└── 📄 README.md                       # Bu dosya
```

---

## 🧩 Modüller

### 1. 👤 Kullanıcı Yönetimi

- Kullanıcı kaydı ve girişi
- Profil yönetimi
- Şifre sıfırlama
- Rol ve yetki kontrolü

### 2. 📦 Ürün Yönetimi

- Ürün ekleme, düzenleme, silme
- Kategori bazlı listeleme
- Toplu ürün işlemleri
- Ürün resmi yükleme
- Stok durumu takibi

### 3. 🏪 Stok Yönetimi

- Gerçek zamanlı stok takibi
- Minimum stok uyarıları
- Stok giriş/çıkış hareketleri
- Depo bazlı stok yönetimi

### 4. 👥 Müşteri (Cari) Yönetimi

- Müşteri kayıt ve düzenleme
- Müşteri profil kartı
- Alacak/borç takibi
- Müşteri bazlı satış geçmişi
- İletişim bilgileri yönetimi

### 5. 📄 Fatura Sistemi

- Alış faturası oluşturma
- Satış faturası oluşturma
- Fatura detay görüntüleme
- Fatura iptal ve düzeltme
- PDF fatura çıktısı
- E-Fatura entegrasyonu (planlı)

### 6. 💰 Satış ve Sipariş

- Hızlı satış girişi
- Sipariş oluşturma ve takibi
- Satış onay süreci
- Ödeme takibi
- İade ve değişim işlemleri

### 7. 📊 Raporlama ve Analiz

- Satış raporları
- Stok raporları
- Müşteri analizleri
- Ürün satış analizleri
- Kar/zarar hesaplama
- Grafik ve dashboard

### 8. 📦 Kargo Entegrasyonu

- Kargo takip numarası oluşturma
- Kargo durumu güncelleme
- Toplu kargo işlemleri
- Kargo firması entegrasyonu (planlı)

### 9. 💬 Mesajlaşma Sistemi

- Dahili mesajlaşma
- Bildirim sistemi
- Toplu mesaj gönderimi
- Mesaj geçmişi

### 10. ⚙️ Sistem Ayarları

- Genel ayarlar
- E-posta yapılandırması
- Şirket bilgileri
- Vergi ve fatura ayarları
- Yedekleme ve geri yükleme

---

## 🔌 API Endpoints

### Ürün API'leri

```
GET    /Urun/Index                    # Ürün listesi
GET    /Urun/UrunGetir/{id}           # Ürün detayı
POST   /Urun/YeniUrun                 # Yeni ürün ekleme
PUT    /Urun/UrunGuncelle/{id}        # Ürün güncelleme
DELETE /Urun/UrunSil/{id}             # Ürün silme
```

### Müşteri API'leri

```
GET    /Cari/Index                    # Müşteri listesi
GET    /Cari/CariGetir/{id}           # Müşteri detayı
POST   /Cari/YeniCari                 # Yeni müşteri ekleme
PUT    /Cari/CariGuncelle/{id}        # Müşteri güncelleme
```

### Fatura API'leri

```
GET    /Faturalar/Index               # Fatura listesi
GET    /Faturalar/FaturaGetir/{id}    # Fatura detayı
POST   /Faturalar/YeniFatura          # Yeni fatura oluşturma
```

### Grafik ve İstatistik API'leri

```
GET    /Grafik/Index                  # Grafik sayfası
GET    /istatistik/Index              # İstatistik sayfası
```

---

## 📸 Ekran Görüntüleri

### 🏠 Dashboard

Admin paneli ana sayfası, tüm önemli metriklerin görüntülendiği merkezi kontrol paneli.

### 📦 Ürün Yönetimi

Ürünlerin listelendiği, düzenlendiği ve yeni ürün eklendiği sayfa. DataTables ile gelişmiş filtreleme özelliği.

### 👥 Müşteri Paneli

Müşteri bilgilerinin görüntülendiği, düzenlendiği ve yönetildiği panel.

### 📊 Raporlar ve Grafikler

Chart.js ile oluşturulmuş interaktif grafik ve raporlar.

### 🧾 Fatura Sistemi

Profesyonel görünümlü fatura oluşturma ve yönetim ekranı.

---

## 🔐 Güvenlik

### Uygulanan Güvenlik Önlemleri

#### 🔒 Kimlik Doğrulama

- Cookie-based authentication
- Secure session yönetimi
- Otomatik oturum sonlandırma
- Çoklu oturum kontrolü

#### 🛡️ Yetkilendirme

- Rol bazlı erişim kontrolü (Role-Based Access Control)
- Action seviyesinde yetki kontrolleri
- Area bazlı erişim kısıtlamaları

#### 🔐 Veri Güvenliği

- SQL Injection koruması (Parameterized queries)
- XSS (Cross-Site Scripting) koruması
- CSRF (Cross-Site Request Forgery) token'ları
- Input validation
- Output encoding

#### 🔑 Şifre Güvenliği

- Güçlü şifre politikası
- Şifre hash'leme (bcrypt/PBKDF2)
- Şifre sıfırlama token'ları
- Güvenli şifre saklama

#### 📝 Denetim ve Loglama

- Tüm kritik işlemlerin loglanması
- Kullanıcı aktivite takibi
- Hata logları
- Güvenlik olayları kaydı

#### 🌐 İletişim Güvenliği

- HTTPS zorunluluğu
- Secure cookie bayrakları
- HTTP Strict Transport Security (HSTS)
- Content Security Policy (CSP)

### Güvenlik Best Practices

```csharp
// Örnek: Authorize attribute kullanımı
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    // Sadece Admin rolüne sahip kullanıcılar erişebilir
}

// Örnek: ValidateAntiForgeryToken kullanımı
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Product model)
{
    // CSRF koruması
}
```

---

## 🤝 Katkıda Bulunma

Projeye katkıda bulunmak isterseniz:

### 1. Fork Edin

Projeyi kendi hesabınıza fork edin.

### 2. Yeni Branch Oluşturun

```bash
git checkout -b feature/YeniOzellik
```

### 3. Değişikliklerinizi Commit Edin

```bash
git commit -m "✨ Yeni özellik: Açıklama"
```

### 4. Branch'inizi Push Edin

```bash
git push origin feature/YeniOzellik
```

### 5. Pull Request Oluşturun

GitHub üzerinden Pull Request açın.

### Commit Mesajı Kuralları

Conventional Commits standardını kullanıyoruz:

- `feat:` Yeni özellik
- `fix:` Hata düzeltme
- `docs:` Dokümantasyon değişikliği
- `style:` Kod formatı değişikliği
- `refactor:` Kod yeniden yapılandırma
- `test:` Test ekleme veya düzeltme
- `chore:` Build veya araç değişikliği

### Kod Standartları

- C# Coding Conventions'a uyun
- XML dokümantasyon ekleyin
- Unit test yazın
- Code review sürecine katılın

---

##  İletişim

### Proje Sahibi

- **GitHub**: [@Yavuz0707](https://github.com/Yavuz0707)
- **Proje Linki**: [MVC_ONLINE_TICARI_OTOMASYON](https://github.com/Yavuz0707/MVC_ONLINE_TICARI_OTOMASYON)

### Destek

Herhangi bir sorun veya öneriniz için:

1. 🐛 [Issue açın](https://github.com/Yavuz0707/MVC_ONLINE_TICARI_OTOMASYON/issues)
2. 💬 [Discussion başlatın](https://github.com/Yavuz0707/MVC_ONLINE_TICARI_OTOMASYON/discussions)
3. ⭐ Projeyi beğendiyseniz yıldız vermeyi unutmayın!

---

## 🙏 Teşekkürler

Bu projeyi kullandığınız için teşekkür ederiz!

### Kullanılan Açık Kaynak Projeler

- [ASP.NET Core](https://github.com/dotnet/aspnetcore)
- [Entity Framework Core](https://github.com/dotnet/efcore)
- [Bootstrap](https://github.com/twbs/bootstrap)
- [AdminLTE](https://github.com/ColorlibHQ/AdminLTE)
- [jQuery](https://github.com/jquery/jquery)
- [Chart.js](https://github.com/chartjs/Chart.js)
- [DataTables](https://github.com/DataTables/DataTables)

---

## 📊 Proje İstatistikleri

![GitHub repo size](https://img.shields.io/github/repo-size/Yavuz0707/MVC_ONLINE_TICARI_OTOMASYON)
![GitHub contributors](https://img.shields.io/github/contributors/Yavuz0707/MVC_ONLINE_TICARI_OTOMASYON)
![GitHub stars](https://img.shields.io/github/stars/Yavuz0707/MVC_ONLINE_TICARI_OTOMASYON?style=social)
![GitHub forks](https://img.shields.io/github/forks/Yavuz0707/MVC_ONLINE_TICARI_OTOMASYON?style=social)

---

## 🗺️ Roadmap

### ✅ Tamamlanan Özellikler

- [x] Temel CRUD işlemleri
- [x] Kullanıcı yetkilendirme sistemi
- [x] Ürün ve stok yönetimi
- [x] Fatura sistemi
- [x] Raporlama modülü
- [x] Responsive tasarım
- [x] Areas yapısı
- [x] ViewComponent implementasyonu
- [x] Custom HTML Helper'lar
- [x] Custom Tag Helper'lar
- [x] Server-side ve Client-side Validation
- [x] Dependency Injection
- [x] Server-side Paging

### 🚧 Geliştirme Aşamasında

- [ ] RESTful API entegrasyonu
- [ ] Real-time bildirimler (SignalR)
- [ ] E-Fatura entegrasyonu
- [ ] Kargo firması API'leri
- [ ] Mobil uygulama

### 📋 Planlanan Özellikler

- [ ] Multi-tenant yapı
- [ ] Mikroservis mimarisi
- [ ] Docker containerization
- [ ] CI/CD pipeline
- [ ] Otomatik test coverage
- [ ] Machine learning entegrasyonu
- [ ] WhatsApp Business API
- [ ] SMS bildirimleri
- [ ] Blockchain entegrasyonu (ürün takibi)

---

<div align="center">

### ⭐ Projeyi beğendiyseniz yıldız vermeyi unutmayın!

**Made with ❤️ by [Yavuz0707](https://github.com/Yavuz0707)**

</div>

