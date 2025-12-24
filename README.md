# MVC Online Ticari Otomasyon Sistemi

Modern ve profesyonel bir ticari otomasyon sistemi. ASP.NET MVC 5, Entity Framework 6 ve SQL Server ile geliştirilmiştir.

## 🚀 Özellikler

### 📊 Yönetim Modülleri
- **Ürün Yönetimi**: Ürün ekleme, düzenleme, silme ve listeleme
- **Kategori Yönetimi**: Kategori bazlı ürün organizasyonu
- **Cari Yönetimi**: Müşteri ve tedarikçi takibi
- **Personel Yönetimi**: Çalışan bilgileri ve departman yönetimi
- **Departman Yönetimi**: Organizasyon yapısı yönetimi

### 💼 Satış ve Faturalama
- **Satış Hareketleri**: Satış işlemleri ve takibi
- **Fatura Yönetimi**: Fatura oluşturma ve kalem detayları
- **Dinamik Faturalar**: Esnek fatura yapılandırması

### 📦 Kargo Sistemi
- **Kargo Takip**: Gerçek zamanlı kargo takip sistemi
- **Otomatik Takip Kodu**: Benzersiz takip kodu üretimi
- **Müşteri Sorgulama**: Müşterilerin kendi kargolarını takip edebilmesi
- **Kargo Geçmişi**: Detaylı hareket kayıtları

### 📈 Raporlama ve İstatistikler
- **Grafikler**: Görsel raporlama
- **İstatistikler**: Detaylı analiz araçları
- **Dashboard**: Özet bilgiler ve hızlı erişim

### 💬 İletişim
- **Mesajlaşma Sistemi**: Dahili mesajlaşma
- **Bildirimler**: Sistem bildirimleri

### 🔐 Güvenlik
- **Rol Tabanlı Yetkilendirme**: Admin ve Müşteri rolleri
- **Forms Authentication**: Güvenli oturum yönetimi
- **CSRF Koruması**: Cross-site request forgery koruması
- **Session Yönetimi**: Güvenli session kontrolü

## 🛠️ Teknolojiler

- **Framework**: ASP.NET MVC 5
- **ORM**: Entity Framework 6 (Code First)
- **Veritabanı**: Microsoft SQL Server / SQL Server Express
- **UI Framework**: Bootstrap 5
- **İkonlar**: Font Awesome 6
- **Paketleme**: NuGet Package Manager
- **Sayfalama**: PagedList.Mvc

## 📋 Gereksinimler

- Visual Studio 2019/2022
- .NET Framework 4.7.2 veya üzeri
- SQL Server 2016 veya üzeri / SQL Server Express
- IIS Express (Visual Studio ile gelir)

## 🔧 Kurulum

### 1. Projeyi İndirin
```bash
git clone [repo-url]
cd MVC_ONLINE_TICARI_OTOMASYON
```

### 2. Veritabanı Bağlantısı

`Web.config` dosyasını açın ve connection string'i düzenleyin:

```xml
<connectionStrings>
  <!-- SQL Server Express için -->
  <add name="Context" 
       connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=dataproje;Integrated Security=True;MultipleActiveResultSets=True" 
       providerName="System.Data.SqlClient" />
  
  <!-- LocalDB için (Visual Studio ile gelen) -->
  <!--<add name="Context" 
       connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=TicariOtomasyonDB;Integrated Security=True" 
       providerName="System.Data.SqlClient" />-->
</connectionStrings>
```

### 3. NuGet Paketlerini Yükleyin

Visual Studio'da:
```
Tools > NuGet Package Manager > Package Manager Console
```

Ardından:
```powershell
Update-Package
```

### 4. Veritabanını Oluşturun

Package Manager Console'da:
```powershell
Update-Database
```

### 5. Projeyi Çalıştırın

- `F5` tuşuna basın veya `▶ Start` butonuna tıklayın
- Tarayıcı otomatik açılacaktır

## 👥 Kullanıcı Tipleri

### Admin Girişi
- URL: `/Login/AdminLogin`
- Tam sistem erişimi
- Tüm modülleri yönetebilir

### Müşteri Girişi
- URL: `/Login/CariLogin1`
- Sınırlı erişim
- Profil, siparişler, kargo takibi ve mesajlar

## 📁 Proje Yapısı

```
MVC_ONLINE_TICARI_OTOMASYON/
├── Controllers/           # MVC Controller'lar
│   ├── BaseController.cs # Ortak base controller
│   ├── LoginController.cs
│   ├── KargoController.cs
│   └── ...
├── Models/               # Veri modelleri
│   └── Siniflar/        # Entity sınıfları
│       ├── Context.cs   # DbContext
│       ├── Admin.cs
│       ├── Cariler.cs
│       └── ...
├── Views/               # Razor view'lar
│   ├── Shared/         # Ortak layout'lar
│   ├── Kargo/
│   └── ...
├── Roller/             # Rol yönetimi
│   └── AdminRoleProvider.cs
├── Migrations/         # EF Migration'lar
├── Content/           # CSS, resimler
├── Scripts/           # JavaScript dosyaları
└── Web.config        # Yapılandırma

```

## 🔑 Önemli Dosyalar

- **Web.config**: Veritabanı bağlantısı ve güvenlik ayarları
- **Global.asax.cs**: Uygulama başlangıcı ve global error handling
- **Context.cs**: Entity Framework DbContext
- **AdminRoleProvider.cs**: Özel rol sağlayıcı

## 🎨 Tema ve UI

- Modern ve responsive tasarım
- Bootstrap 5 tabanlı
- Font Awesome ikonları
- AdminLTE 3.0.4 admin template'i

## 🔒 Güvenlik Özellikleri

1. **Forms Authentication**: Güvenli oturum yönetimi
2. **Role-Based Authorization**: Rol tabanlı erişim kontrolü
3. **CSRF Protection**: ValidateAntiForgeryToken kullanımı
4. **Session Security**: Güvenli session yönetimi
5. **SQL Injection Protection**: Entity Framework parametreli sorgular
6. **Custom Error Pages**: Özel hata sayfaları

## 📊 Veritabanı Tabloları

- **Admins**: Admin kullanıcıları
- **Carilers**: Müşteri/Cari kayıtları
- **Uruns**: Ürün bilgileri
- **Kategoris**: Kategori bilgileri
- **Departmans**: Departman bilgileri
- **Personels**: Personel kayıtları
- **SatisHarekets**: Satış işlemleri
- **Faturalars**: Fatura başlıkları
- **FaturaKalems**: Fatura kalemleri
- **KargoDetays**: Kargo bilgileri
- **KargoTakips**: Kargo hareket geçmişi
- **mesajlars**: Mesajlaşma sistemi
- **Giders**: Gider kayıtları

## 🐛 Hata Ayıklama

### Veritabanı Bağlantı Hatası
```
SQL Server servisinin çalıştığından emin olun:
- services.msc açın
- SQL Server (SQLEXPRESS) servisini başlatın
```

### Migration Hatası
```powershell
# Package Manager Console'da
Enable-Migrations
Add-Migration InitialCreate
Update-Database
```

### NuGet Paketi Eksik
```powershell
# Tüm paketleri geri yükle
Update-Package -Reinstall
```

## 📞 Destek

Sorun yaşarsanız:
1. Web.config bağlantı stringini kontrol edin
2. SQL Server'ın çalıştığından emin olun
3. NuGet paketlerinin yüklendiğini doğrulayın
4. Migration'ların çalıştırıldığını kontrol edin

## 📝 Notlar

- **Geliştirme Modu**: `debug="true"` Web.config'de aktif
- **Prodüksiyon**: `debug="false"` yapın ve customErrors="On" ayarlayın
- **Connection String**: Sunucu adını kendi ortamınıza göre düzenleyin

## 🚀 Yayınlama

### IIS'e Yayınlama

1. Visual Studio'da: **Build > Publish**
2. **Folder** seçin
3. Hedef klasörü belirleyin
4. **Publish** tıklayın
5. IIS'de yeni site oluşturun
6. Application Pool'u .NET 4.x olarak ayarlayın

### Azure'a Yayınlama

1. Azure'da Web App oluşturun
2. Visual Studio'da **Publish**
3. **Azure** seçin
4. Subscription ve Web App'i seçin
5. Connection string'i Azure'da ayarlayın

## 📜 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

## 👨‍💻 Geliştirici

- **Versiyon**: 1.0.0
- **Tarih**: 2025
- **Framework**: ASP.NET MVC 5
- **Durum**: Production Ready ✅

---

**Son Güncelleme**: 24 Aralık 2025
