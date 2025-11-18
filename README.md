# Cakir QR Menu - QR Kod Tabanlı Dijital Menü Yönetim Sistemi

## 📋 İçindekiler
- [Proje Hakkında](#proje-hakkında)
- [Temel Özellikler](#temel-özellikler)
- [Mimari Yapı](#mimari-yapı)
- [Teknoloji Stack](#teknoloji-stack)
- [Kurulum](#kurulum)
- [Detaylı Dokümantasyon](#detaylı-dokümantasyon)

## 🎯 Proje Hakkında

Cakir QR Menu, restoranlar, kafeler ve diğer yiyecek-içecek işletmeleri için modern bir dijital menü yönetim sistemidir. QR kod teknolojisi kullanarak müşterilerin akıllı telefonlarından kolayca menülere erişmesini sağlar.

### Temel Amaç
- Temassız menü deneyimi sunmak
- Menü yönetimini dijitalleştirmek
- Müşteri deneyimini iyileştirmek
- İşletmelere merkezi menü yönetimi sağlamak

## ✨ Temel Özellikler

### 🏢 Şirket Yönetimi
- Çoklu şirket desteği
- Şirket tipi kategorilendirmesi
- Şirket profil bilgileri yönetimi
- İletişim bilgileri ve adres yönetimi

### 📋 Menü Yönetimi
- Dinamik menü oluşturma ve düzenleme
- Birden fazla menü desteği
- Menü açıklamaları ve detayları
- Menü için QR kod üretimi

### 🏷️ Kategori Yönetimi
- Ürün kategorilendirmesi
- Kategori açıklamaları
- Şirkete özel kategoriler

### 🍽️ Ürün Yönetimi
- Detaylı ürün bilgileri
- Ürün açıklamaları
- Fiyat yönetimi
- Ürün görselleri (çoklu resim desteği)
- Ürün malzemeleri bilgisi
- Ürün müsaitlik durumu

### 🔐 Güvenlik ve Yetkilendirme
- JWT tabanlı kimlik doğrulama
- Rol tabanlı yetkilendirme (RBAC)
- Email ile doğrulama
- OTP (One-Time Password) doğrulama
- İki faktörlü kimlik doğrulama (2FA)
- Güvenli token yönetimi
- Token iptal mekanizması

### 📱 QR Kod Özellikleri
- Otomatik QR kod üretimi
- Menü bazlı QR kodlar
- QR kod görsellerinin saklanması

### 🔄 İş Süreçleri
- CQRS pattern ile komut ve sorgu ayrımı
- MediatR ile istek-yanıt yönetimi
- Validation pipeline ile veri doğrulama
- Authorization pipeline ile yetki kontrolü
- Caching mekanizması
- Logging sistemi
- Transaction yönetimi
- Rate limiting

## 🏗️ Mimari Yapı

Proje **Clean Architecture** ve **Domain-Driven Design (DDD)** prensiplerine göre yapılandırılmıştır.

### Katman Yapısı

```
├── Core (Temel Altyapı Katmanı)
│   ├── Core.Application          # CQRS, Pipelines, Base sınıflar
│   ├── Core.Persistence          # Repository pattern, EF Core
│   ├── Core.Security             # JWT, Hashing, Encryption
│   ├── Core.WebAPI               # Base controller, Extensions
│   ├── Core.CrossCuttingConcerns # Exception handling, Logging
│   ├── Core.QrCodeGenerator      # QR kod üretim servisi
│   ├── Core.Mailing              # Email servisleri
│   ├── Core.ElasticSearch        # Elasticsearch entegrasyonu
│   └── Core.Helpers              # Yardımcı fonksiyonlar
│
└── QrMenu (İş Mantığı Katmanı)
    ├── QrMenu.Domain             # Entity'ler, Domain modelleri
    ├── QrMenu.Application        # Use cases, CQRS handlers
    ├── QrMenu.Persistence        # Database context, Migrations
    ├── QrMenu.Infrastructure     # External servisler
    └── QrMenu.WebAPI             # API Controllers, Endpoints
```

### Mimari Prensipler

1. **Separation of Concerns**: Her katman kendi sorumluluğuna odaklanır
2. **Dependency Inversion**: Üst katmanlar alt katmanlara bağımlı değildir
3. **SOLID Principles**: Tüm kod SOLID prensiplerine uygun yazılmıştır
4. **CQRS Pattern**: Command ve Query işlemleri ayrılmıştır
5. **Repository Pattern**: Veri erişimi soyutlanmıştır

## 🛠️ Teknoloji Stack

### Backend
- **.NET 8.0** - Ana framework
- **ASP.NET Core WebAPI** - RESTful API
- **Entity Framework Core** - ORM
- **MediatR** - CQRS implementation
- **AutoMapper** - Object mapping
- **FluentValidation** - Validation
- **MassTransit** - Message broker
- **JWT** - Authentication

### Veritabanı
- **SQL Server / PostgreSQL** - İlişkisel veritabanı
- **Entity Framework Core** - Database provider

### Güvenlik
- **BCrypt** - Password hashing
- **JWT Bearer** - Token authentication
- **AES Encryption** - Data encryption

### Diğer Kütüphaneler
- **QRCoder** - QR kod üretimi
- **Elasticsearch** - Arama ve indexleme
- **Swashbuckle** - API documentation (Swagger)
- **Serilog** - Structured logging

## 🚀 Kurulum

### Gereksinimler
- .NET 8.0 SDK veya üzeri
- SQL Server veya PostgreSQL
- Visual Studio 2022 veya Visual Studio Code

### Adımlar

1. **Repository'yi klonlayın**
```bash
git clone https://github.com/ahmetcakr/Cakir-Qr.git
cd Cakir-Qr
```

2. **Bağımlılıkları yükleyin**
```bash
dotnet restore QrMenu.sln
```

3. **Veritabanı bağlantı ayarlarını yapılandırın**
`src/QrMenu/QrMenu.WebAPI/appsettings.json` dosyasında ConnectionString'i güncelleyin.

4. **Veritabanını oluşturun**
```bash
cd src/QrMenu/QrMenu.Persistence
dotnet ef database update
```

5. **Uygulamayı çalıştırın**
```bash
cd ../QrMenu.WebAPI
dotnet run
```

6. **Swagger UI'a erişin**
Tarayıcınızda `https://localhost:5001/swagger` adresine gidin.

## 📚 Detaylı Dokümantasyon

Daha detaylı bilgi için aşağıdaki dokümantasyonlara göz atın:

- [**Mimari Dokümantasyonu**](./docs/ARCHITECTURE.tr.md) - Detaylı mimari açıklamalar
- [**Özellikler ve Yetenekler**](./docs/FEATURES.tr.md) - Tüm özelliklerin detaylı açıklaması
- [**İç Akış ve Veri Akışı**](./docs/WORKFLOW.tr.md) - Sistem içindeki akışların detaylı açıklaması
- [**Geliştirici Kılavuzu**](./docs/DEVELOPER_GUIDE.tr.md) - Geliştirme için rehber

## 🤝 Katkıda Bulunma

Katkılarınızı bekliyoruz! Lütfen değişikliklerinizi pull request olarak gönderin.

## 📄 Lisans

Bu proje [LICENSE.txt](./LICENSE.txt) dosyasında belirtilen lisans altında lisanslanmıştır.

## 👤 İletişim

Proje Sahibi: Ahmet Çakır
- GitHub: [@ahmetcakr](https://github.com/ahmetcakr)