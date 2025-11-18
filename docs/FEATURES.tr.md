# Özellikler ve Yetenekler Dokümantasyonu

## 📑 İçindekiler
- [Kimlik Doğrulama ve Yetkilendirme](#kimlik-doğrulama-ve-yetkilendirme)
- [Şirket Yönetimi](#şirket-yönetimi)
- [Menü Yönetimi](#menü-yönetimi)
- [Kategori Yönetimi](#kategori-yönetimi)
- [Ürün Yönetimi](#ürün-yönetimi)
- [QR Kod Yönetimi](#qr-kod-yönetimi)
- [Kullanıcı Yönetimi](#kullanıcı-yönetimi)
- [Rol ve Yetki Yönetimi](#rol-ve-yetki-yönetimi)

---

## 🔐 Kimlik Doğrulama ve Yetkilendirme

### Genel Bakış
Sistem, çok katmanlı bir güvenlik yapısına sahiptir. JWT (JSON Web Token) tabanlı kimlik doğrulama ve rol tabanlı yetkilendirme kullanır.

### Özellikler

#### 1. Kullanıcı Kaydı (Register)
**Endpoint**: `POST /api/Auth/Register`

**Özellikler**:
- Email ve şifre ile kayıt
- Şifre güvenlik gereksinimleri
- Otomatik kullanıcı profili oluşturma
- İlk kayıtta default roller atama

**Request**:
```json
{
  "email": "user@example.com",
  "password": "SecurePass123!",
  "firstName": "Ahmet",
  "lastName": "Yılmaz"
}
```

**Response**:
```json
{
  "accessToken": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiration": "2024-01-15T10:30:00Z"
  },
  "refreshToken": {
    "token": "random_refresh_token_string",
    "expiration": "2024-02-15T10:30:00Z"
  }
}
```

#### 2. Kullanıcı Girişi (Login)
**Endpoint**: `POST /api/Auth/Login`

**Özellikler**:
- Email ve şifre ile giriş
- JWT token üretimi
- Refresh token üretimi
- Başarısız giriş denemeleri takibi

**Request**:
```json
{
  "email": "user@example.com",
  "password": "SecurePass123!"
}
```

#### 3. İki Faktörlü Kimlik Doğrulama (2FA)

##### Email Tabanlı 2FA
**Enable**: `POST /api/Auth/EnableEmailAuthenticator`
- Email doğrulama kodu gönderimi
- Doğrulama kodu 5 dakika geçerli

**Verify**: `POST /api/Auth/VerifyEmailAuthenticator`
- Doğrulama kodu ile aktivasyon
- Email doğrulama durumu güncelleme

##### OTP (One-Time Password) Tabanlı 2FA
**Enable**: `POST /api/Auth/EnableOtpAuthenticator`
- QR kod üretimi
- Authenticator app (Google Authenticator, Authy) entegrasyonu
- Secret key üretimi

**Verify**: `POST /api/Auth/VerifyOtpAuthenticator`
- 6 haneli kod doğrulama
- Time-based OTP (TOTP) desteği

**Response Örneği**:
```json
{
  "secretKey": "JBSWY3DPEHPK3PXP",
  "qrCodeImage": "data:image/png;base64,iVBORw0KGgoAAAANS..."
}
```

#### 4. Doğrulama Kodu Gönderimi
**Endpoint**: `POST /api/Auth/SendAuthCode`

**Özellikler**:
- Login sırasında doğrulama kodu talep etme
- Email veya OTP seçimi
- Rate limiting ile spam koruması

#### 5. Token Yenileme
**Endpoint**: `POST /api/Auth/RefreshToken`

**Özellikler**:
- Refresh token ile yeni access token alma
- Otomatik token yenileme
- Token rotation stratejisi

#### 6. Token İptal Etme (Revoke)
**Endpoint**: `POST /api/Auth/RevokeToken`

**Özellikler**:
- Aktif token'ı geçersiz kılma
- Logout fonksiyonalitesi
- Güvenlik için token blacklist

---

## 🏢 Şirket Yönetimi

### Genel Bakış
Çoklu şirket yapısını destekleyen kapsamlı bir şirket yönetim sistemi.

### Özellikler

#### 1. Şirket Oluşturma
**Endpoint**: `POST /api/Companies`

**Özellikler**:
- Şirket adı, tipi, adres, telefon, email, website bilgileri
- Benzersiz şirket adı kontrolü
- Şirket tipi validasyonu

**Request**:
```json
{
  "companyName": "Lezzet Durağı Restaurant",
  "companyTypeId": 1,
  "address": "Atatürk Caddesi No:45 Beyoğlu/İstanbul",
  "phone": "+90 212 555 0100",
  "email": "info@lezzetduragi.com",
  "website": "https://www.lezzetduragi.com"
}
```

#### 2. Şirket Listeleme
**Endpoint**: `GET /api/Companies`

**Özellikler**:
- Sayfalama desteği
- Sıralama seçenekleri
- Filtreleme
- Dinamik sorgu desteği

**Query Parameters**:
- `pageIndex`: Sayfa numarası (default: 0)
- `pageSize`: Sayfa başına kayıt (default: 10)

**Response**:
```json
{
  "items": [
    {
      "id": 1,
      "companyName": "Lezzet Durağı Restaurant",
      "companyType": "Restaurant",
      "phone": "+90 212 555 0100",
      "email": "info@lezzetduragi.com"
    }
  ],
  "index": 0,
  "size": 10,
  "count": 1,
  "pages": 1,
  "hasPrevious": false,
  "hasNext": false
}
```

#### 3. Şirket Detay Görüntüleme
**Endpoint**: `GET /api/Companies/{id}`

**Özellikler**:
- Tüm şirket bilgileri
- İlişkili kategoriler
- İlişkili menüler
- İstatistikler (opsiyonel)

#### 4. Şirket Güncelleme
**Endpoint**: `PUT /api/Companies/{id}`

**Özellikler**:
- Tüm alanları güncelleme
- Partial update desteği
- Değişiklik geçmişi (audit log)

#### 5. Şirket Silme
**Endpoint**: `DELETE /api/Companies/{id}`

**Özellikler**:
- Soft delete (mantıksal silme)
- İlişkili verilerin durumu kontrolü
- Cascade delete seçenekleri

#### 6. Şirket Tipleri
**Endpoint**: `GET /api/CompanyTypes`

**Özellikler**:
- Restaurant
- Cafe
- Fast Food
- Bar
- Hotel
- Catering
- Other

---

## 📋 Menü Yönetimi

### Genel Bakış
Her şirket için birden fazla menü oluşturma ve yönetme imkanı.

### Özellikler

#### 1. Menü Oluşturma
**Endpoint**: `POST /api/Menus`

**Özellikler**:
- Menü adı ve açıklaması
- Şirkete bağlı menü
- Otomatik QR kod oluşturma seçeneği

**Request**:
```json
{
  "companyId": 1,
  "menuName": "Ana Menü",
  "description": "Restoranımızın güncel menüsü",
  "generateQrCode": true
}
```

#### 2. Menü Listeleme
**Endpoint**: `GET /api/Menus`

**Filtreleme Seçenekleri**:
- Şirkete göre filtreleme
- Aktif/pasif menüler
- Tarih aralığı

#### 3. Menü Detayları
**Endpoint**: `GET /api/Menus/{id}`

**İçerik**:
- Menü bilgileri
- İlişkili kategoriler
- Kategori altındaki ürünler
- QR kod bilgisi
- İstatistikler

**Response Örneği**:
```json
{
  "id": 1,
  "menuName": "Ana Menü",
  "description": "Restoranımızın güncel menüsü",
  "company": {
    "id": 1,
    "name": "Lezzet Durağı Restaurant"
  },
  "qrCodes": [
    {
      "id": 1,
      "qrCodeText": "https://menu.lezzetduragi.com/1",
      "createdDate": "2024-01-15T10:00:00Z"
    }
  ],
  "categoryCount": 5,
  "itemCount": 45
}
```

#### 4. Menü Güncelleme
**Endpoint**: `PUT /api/Menus/{id}`

**Özellikler**:
- Menü bilgilerini güncelleme
- Kategori sıralaması
- Aktif/pasif durumu

#### 5. Menü Silme
**Endpoint**: `DELETE /api/Menus/{id}`

**Özellikler**:
- Soft delete
- İlişkili QR kodların durumu

---

## 🏷️ Kategori Yönetimi

### Genel Bakış
Ürünlerin kategorize edilmesi için kategori yönetim sistemi.

### Özellikler

#### 1. Kategori Oluşturma
**Endpoint**: `POST /api/Categories`

**Request**:
```json
{
  "companyId": 1,
  "categoryName": "Ana Yemekler",
  "description": "Et ve tavuk yemekleri"
}
```

#### 2. Kategori Listeleme
**Endpoint**: `GET /api/Categories`

**Filtreleme**:
- Şirkete göre
- Alfabetik sıralama
- Ürün sayısına göre

**Response**:
```json
{
  "items": [
    {
      "id": 1,
      "categoryName": "Ana Yemekler",
      "description": "Et ve tavuk yemekleri",
      "itemCount": 12
    },
    {
      "id": 2,
      "categoryName": "Salatalar",
      "description": "Taze mevsim salataları",
      "itemCount": 8
    }
  ]
}
```

#### 3. Kategori Detayları
**Endpoint**: `GET /api/Categories/{id}`

**İçerik**:
- Kategori bilgileri
- Altındaki tüm ürünler
- Ürün görselleri
- Fiyat aralığı

#### 4. Kategori Güncelleme ve Silme
**Update**: `PUT /api/Categories/{id}`
**Delete**: `DELETE /api/Categories/{id}`

---

## 🍽️ Ürün Yönetimi

### Genel Bakış
Menü ürünlerinin detaylı yönetimi için kapsamlı özellikler.

### Özellikler

#### 1. Ürün Oluşturma
**Endpoint**: `POST /api/Items`

**Request**:
```json
{
  "categoryId": 1,
  "itemName": "Izgara Köfte",
  "description": "Özel baharatlarla hazırlanmış ızgara köfte",
  "price": 89.90,
  "isAvailable": true,
  "ingredients": [
    "Dana kıyma",
    "Soğan",
    "Baharatlar"
  ],
  "allergens": ["Soğan"]
}
```

#### 2. Ürün Listeleme
**Endpoint**: `GET /api/Items`

**Filtreleme Seçenekleri**:
- Kategoriye göre
- Fiyat aralığı
- Müsaitlik durumu
- Arama (ürün adı, açıklama)

**Sıralama**:
- Ada göre (A-Z, Z-A)
- Fiyata göre (Artan, Azalan)
- Popülerliğe göre
- Yeni eklenenlere göre

#### 3. Ürün Detayları
**Endpoint**: `GET /api/Items/{id}`

**Response**:
```json
{
  "id": 1,
  "itemName": "Izgara Köfte",
  "description": "Özel baharatlarla hazırlanmış ızgara köfte",
  "price": 89.90,
  "isAvailable": true,
  "category": {
    "id": 1,
    "name": "Ana Yemekler"
  },
  "images": [
    {
      "id": 1,
      "imageUrl": "https://cdn.example.com/items/kofte-1.jpg",
      "isPrimary": true
    }
  ],
  "ingredients": {
    "items": ["Dana kıyma", "Soğan", "Baharatlar"],
    "allergens": ["Soğan"]
  }
}
```

#### 4. Ürün Görsel Yönetimi
**Upload**: `POST /api/ItemImages`
**Delete**: `DELETE /api/ItemImages/{id}`

**Özellikler**:
- Çoklu resim yükleme
- Ana resim belirleme
- Resim sıralama
- Otomatik resim optimizasyonu
- Desteklenen formatlar: JPG, PNG, WebP

#### 5. Ürün Malzeme Yönetimi
**Endpoint**: `POST /api/Items/{id}/Ingredients`

**Özellikler**:
- Malzeme listesi
- Alerjen bilgileri
- Beslenme değerleri (opsiyonel)
- Kalori bilgisi (opsiyonel)

#### 6. Ürün Müsaitlik Durumu
**Endpoint**: `PATCH /api/Items/{id}/Availability`

**Request**:
```json
{
  "isAvailable": false,
  "reason": "Stok bitti"
}
```

**Kullanım Senaryoları**:
- Günlük menü değişiklikleri
- Mevsimsel ürünler
- Stok durumu
- Kampanya ürünleri

---

## 📱 QR Kod Yönetimi

### Genel Bakış
Menüler için otomatik QR kod üretimi ve yönetimi.

### Özellikler

#### 1. QR Kod Oluşturma
**Endpoint**: `POST /api/MenuQrCodes`

**Request**:
```json
{
  "menuId": 1,
  "qrCodeText": "https://menu.lezzetduragi.com/1"
}
```

**İşlem Akışı**:
1. Menü ID kontrolü
2. URL üretimi/validasyonu
3. QR kod görselinin oluşturulması
4. Veritabanına kaydetme
5. QR kod görseli ve bilgilerinin dönülmesi

#### 2. QR Kod İndirme
**Endpoint**: `GET /api/MenuQrCodes/{id}/Download`

**Özellikler**:
- PNG formatında indirme
- Özelleştirilebilir boyut
- Logo ekleme (opsiyonel)
- Renk seçenekleri

**Query Parameters**:
- `size`: QR kod boyutu (default: 300x300)
- `format`: Dosya formatı (png, svg, pdf)
- `quality`: Görsel kalitesi (low, medium, high)

#### 3. QR Kod Listeleme
**Endpoint**: `GET /api/MenuQrCodes`

**Filtreleme**:
- Menüye göre
- Şirkete göre
- Oluşturma tarihine göre

#### 4. QR Kod İstatistikleri
**Endpoint**: `GET /api/MenuQrCodes/{id}/Statistics`

**Metrikler**:
- Tarama sayısı
- Benzersiz kullanıcı sayısı
- Tarihsel trend
- Cihaz dağılımı
- Konum bilgileri (opsiyonel)

**Response**:
```json
{
  "totalScans": 1523,
  "uniqueUsers": 876,
  "scansByDate": [
    { "date": "2024-01-15", "count": 45 },
    { "date": "2024-01-14", "count": 52 }
  ],
  "deviceBreakdown": {
    "mobile": 85,
    "tablet": 10,
    "desktop": 5
  }
}
```

#### 5. QR Kod Güncelleme
**Endpoint**: `PUT /api/MenuQrCodes/{id}`

**Özellikler**:
- URL güncelleme
- Yeni QR kod üretimi
- Eski QR kodun arşivlenmesi

---

## 👤 Kullanıcı Yönetimi

### Genel Bakış
Sistem kullanıcılarının yönetimi için kapsamlı özellikler.

### Özellikler

#### 1. Kullanıcı Listeleme
**Endpoint**: `GET /api/Users`

**Yetkiler**: Admin rolü gerektirir

**Filtreleme**:
- Email
- Durum (Aktif/Pasif)
- Rol
- Kayıt tarihi

#### 2. Kullanıcı Detayları
**Endpoint**: `GET /api/Users/{id}`

**Response**:
```json
{
  "id": 1,
  "email": "user@example.com",
  "firstName": "Ahmet",
  "lastName": "Yılmaz",
  "status": "Active",
  "roles": ["User", "CompanyAdmin"],
  "emailVerified": true,
  "twoFactorEnabled": true,
  "createdDate": "2024-01-15T10:00:00Z",
  "lastLoginDate": "2024-01-20T14:30:00Z"
}
```

#### 3. Kullanıcı Güncelleme
**Endpoint**: `PUT /api/Users/{id}`

**Güncellenebilir Alanlar**:
- Ad, soyad
- Email
- Durum
- Roller (Admin yetkisi gerekir)

#### 4. Kullanıcı Silme
**Endpoint**: `DELETE /api/Users/{id}`

**Özellikler**:
- Soft delete
- İlişkili verilerin durumu
- GDPR uyumluluğu

#### 5. Profil Yönetimi
**Get Profile**: `GET /api/Users/Profile`
**Update Profile**: `PUT /api/Users/Profile`

**Kullanıcı Kendisi İçin**:
- Profil bilgilerini görüntüleme
- Profil bilgilerini güncelleme
- Şifre değiştirme
- 2FA ayarları

---

## 🔑 Rol ve Yetki Yönetimi

### Genel Bakış
Rol tabanlı erişim kontrolü (RBAC) sistemi.

### Özellikler

#### 1. Yetki (Operation Claim) Yönetimi
**Endpoint**: `GET /api/OperationClaims`

**Varsayılan Yetkiler**:
```
- Admin
- User
- CompanyAdmin
- CompanyUser
- MenuManager
- ItemManager
```

#### 2. Kullanıcıya Yetki Atama
**Endpoint**: `POST /api/UserOperationClaims`

**Request**:
```json
{
  "userId": 1,
  "operationClaimId": 2
}
```

#### 3. Yetki Kontrolü
Her API endpoint'inde `[Authorize]` attribute'u ile kontrol:

```csharp
[Authorize(Roles = "Admin,CompanyAdmin")]
[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateCommand command)
{
    // ...
}
```

#### 4. Hiyerarşik Yetki Yapısı

```
Admin (Tüm yetkiler)
  ├── CompanyAdmin (Şirket yönetimi)
  │   ├── MenuManager (Menü yönetimi)
  │   └── ItemManager (Ürün yönetimi)
  └── User (Temel kullanıcı)
```

#### 5. Dinamik Yetki Kontrolü
- Resource-based authorization
- Claim-based authorization
- Policy-based authorization

---

## 📊 Raporlama ve İstatistikler (Gelecek Özellikler)

### Planlanan Özellikler

1. **Menü İstatistikleri**:
   - En çok görüntülenen ürünler
   - QR kod tarama sayıları
   - Zaman bazlı analizler

2. **Satış Raporları** (Entegrasyon ile):
   - Ürün bazlı satış
   - Kategori performansı
   - Dönemsel karşılaştırmalar

3. **Kullanıcı Davranış Analizi**:
   - Menü gezinme süreleri
   - Popüler kategoriler
   - Arama terimleri

---

## 🔔 Bildirim Sistemi (Gelecek Özellikler)

### Planlanan Özellikler

1. **Email Bildirimleri**:
   - Hoş geldiniz emaili
   - Şifre sıfırlama
   - Önemli sistem bildirimleri

2. **Push Bildirimleri**:
   - Yeni ürün ekleme
   - Fiyat güncellemeleri
   - Özel kampanyalar

3. **SMS Bildirimleri**:
   - Doğrulama kodları
   - Kritik güvenlik bildirimleri

---

## 🌐 Çoklu Dil Desteği (Gelecek Özellikler)

### Planlanan Özellikler

1. **Menü Çevirisi**:
   - Türkçe, İngilizce, Almanca, Rusça
   - Otomatik çeviri entegrasyonu
   - Manuel çeviri yönetimi

2. **Arayüz Yerelleştirmesi**:
   - Tüm UI metinlerinin çevirisi
   - Tarih ve para formatları
   - Kültürel uyarlamalar

---

Tüm bu özellikler, RESTful API prensipleri ile tasarlanmış ve Swagger dokümantasyonu ile desteklenmektedir.
