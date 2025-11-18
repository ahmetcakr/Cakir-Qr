# Mimari Dokümantasyonu

## 📐 Genel Mimari

Cakir QR Menu projesi, **Clean Architecture** prensiplerine dayalı katmanlı bir mimari yapısına sahiptir. Bu mimari, kodun test edilebilirliğini, bakımını ve genişletilebilirliğini maksimize etmek için tasarlanmıştır.

## 🏛️ Katman Mimarisi

### 1. Domain Katmanı (QrMenu.Domain)

**Amaç**: İş mantığının kalbi, tüm entity'lerin ve domain kurallarının tanımlandığı katman.

**Sorumluluklar**:
- Entity tanımlamaları
- Domain modelleri
- İş kuralları (Business Rules)
- Value objects

**Başlıca Entity'ler**:

```csharp
// Şirket Entity'si
public class Company : Entity<int>
{
    public string CompanyName { get; set; }
    public int CompanyTypeId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    
    // İlişkiler
    public virtual CompanyType CompanyType { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Menu> Menus { get; set; }
}

// Menü Entity'si
public class Menu : Entity<int>
{
    public int CompanyId { get; set; }
    public string MenuName { get; set; }
    public string Description { get; set; }
    
    // İlişkiler
    public virtual Company Company { get; set; }
    public virtual ICollection<MenuQrCode> MenuQrCodes { get; set; }
}

// Kategori Entity'si
public class Category : Entity<int>
{
    public int CompanyId { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    
    // İlişkiler
    public virtual Company Company { get; set; }
    public virtual ICollection<Item> Items { get; set; }
}

// Ürün Entity'si
public class Item : Entity<int>
{
    public int CategoryId { get; set; }
    public string ItemName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    
    // İlişkiler
    public virtual Category Category { get; set; }
    public virtual ICollection<ItemImage> ItemImages { get; set; }
    public virtual ItemIngredient ItemIngredient { get; set; }
}
```

**Bağımlılıklar**: Hiçbir dış bağımlılığı yoktur (sadece Core.Persistence)

---

### 2. Application Katmanı (QrMenu.Application)

**Amaç**: Use case'lerin (kullanım senaryolarının) implement edildiği katman.

**Sorumluluklar**:
- CQRS pattern implementation (Commands & Queries)
- Business logic orchestration
- Validation rules
- Authorization rules
- DTO (Data Transfer Objects)
- Mapping profiles

**CQRS Yapısı**:

Her feature için ayrı klasör yapısı:
```
Features/
├── Companies/
│   ├── Commands/
│   │   ├── Create/
│   │   │   ├── CreateCompanyCommand.cs
│   │   │   ├── CreateCompanyCommandValidator.cs
│   │   │   └── CreatedCompanyResponse.cs
│   │   ├── Update/
│   │   └── Delete/
│   ├── Queries/
│   │   ├── GetById/
│   │   ├── GetList/
│   │   └── GetListByDynamic/
│   ├── Rules/
│   │   └── CompanyBusinessRules.cs
│   ├── Constants/
│   │   └── CompanyMessages.cs
│   └── Profiles/
│       └── MappingProfile.cs
```

**Command Örneği**:
```csharp
public class CreateCompanyCommand : IRequest<CreatedCompanyResponse>
{
    public string CompanyName { get; set; }
    public int CompanyTypeId { get; set; }
    public string Address { get; set; }
    
    public class CreateCompanyCommandHandler 
        : IRequestHandler<CreateCompanyCommand, CreatedCompanyResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly CompanyBusinessRules _businessRules;
        
        public async Task<CreatedCompanyResponse> Handle(
            CreateCompanyCommand request, 
            CancellationToken cancellationToken)
        {
            await _businessRules.CompanyNameShouldBeUnique(request.CompanyName);
            
            Company company = _mapper.Map<Company>(request);
            Company createdCompany = await _companyRepository.AddAsync(company);
            
            return _mapper.Map<CreatedCompanyResponse>(createdCompany);
        }
    }
}
```

**Query Örneği**:
```csharp
public class GetListCompanyQuery : IRequest<GetListResponse<GetListCompanyListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    
    public class GetListCompanyQueryHandler 
        : IRequestHandler<GetListCompanyQuery, GetListResponse<GetListCompanyListItemDto>>
    {
        private readonly ICompanyRepository _companyRepository;
        
        public async Task<GetListResponse<GetListCompanyListItemDto>> Handle(
            GetListCompanyQuery request, 
            CancellationToken cancellationToken)
        {
            IPaginate<Company> companies = await _companyRepository
                .GetListAsync(index: request.PageRequest.PageIndex);
                
            return _mapper.Map<GetListResponse<GetListCompanyListItemDto>>(companies);
        }
    }
}
```

---

### 3. Persistence Katmanı (QrMenu.Persistence)

**Amaç**: Veritabanı erişimi ve data persistence işlemlerinin gerçekleştiği katman.

**Sorumluluklar**:
- Entity Framework Core DbContext
- Repository implementations
- Database migrations
- Seed data
- Database configuration

**DbContext Yapısı**:
```csharp
public class QrMenuDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<MenuQrCode> MenuQrCodes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity configurations
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
```

**Repository Pattern**:
```csharp
public class CompanyRepository : EfRepositoryBase<Company, int, QrMenuDbContext>, 
                                 ICompanyRepository
{
    public CompanyRepository(QrMenuDbContext context) : base(context)
    {
    }
    
    // Özel sorgular burada tanımlanır
    public async Task<Company?> GetByNameAsync(string name)
    {
        return await Context.Companies
            .FirstOrDefaultAsync(c => c.CompanyName == name);
    }
}
```

---

### 4. Infrastructure Katmanı (QrMenu.Infrastructure)

**Amaç**: Harici servis entegrasyonlarının yapıldığı katman.

**Sorumluluklar**:
- External API integrations
- File storage services
- Third-party service implementations
- Message queue implementations

---

### 5. WebAPI Katmanı (QrMenu.WebAPI)

**Amaç**: HTTP endpoint'lerinin tanımlandığı presentation katmanı.

**Sorumluluklar**:
- RESTful API endpoints
- HTTP request/response handling
- API configuration
- Middleware configuration
- Dependency injection setup

**Controller Örneği**:
```csharp
[Route("api/[controller]")]
[ApiController]
public class CompaniesController : BaseController
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
    {
        CreatedCompanyResponse response = await Mediator.Send(command);
        return Created(uri: "", response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCompanyQuery query = new() { PageRequest = pageRequest };
        GetListResponse<GetListCompanyListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdCompanyQuery query = new() { Id = id };
        GetByIdCompanyResponse response = await Mediator.Send(query);
        return Ok(response);
    }
}
```

---

## 🔧 Core Modülleri

### Core.Application

**Pipeline Behaviors**:

1. **Authorization Pipeline**: İstek öncesi yetki kontrolü
2. **Validation Pipeline**: FluentValidation ile veri doğrulama
3. **Caching Pipeline**: Response caching
4. **Logging Pipeline**: İstek/yanıt loglama
5. **Transaction Pipeline**: Veritabanı transaction yönetimi
6. **Performance Pipeline**: Performans ölçümü
7. **RateLimiting Pipeline**: Rate limiting kontrolü

```csharp
// Pipeline execution order
services.AddMediatR(configuration =>
{
    configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
    configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
    configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
    configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
    configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionScopeBehavior<,>));
});
```

### Core.Security

**Güvenlik Bileşenleri**:

1. **JWT (JSON Web Token)**:
   - Token generation
   - Token validation
   - Refresh token mechanism

2. **Hashing**:
   - Password hashing (BCrypt)
   - Hash verification

3. **Encryption**:
   - Security key generation
   - AES encryption/decryption

4. **Authentication Types**:
   - Email authenticator
   - OTP (One-Time Password) authenticator
   - Two-factor authentication

```csharp
public class TokenOptions
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int AccessTokenExpiration { get; set; }
    public string SecurityKey { get; set; }
    public int RefreshTokenTTL { get; set; }
}
```

### Core.Persistence

**Repository Pattern**:

```csharp
public interface IRepository<TEntity, TEntityId> where TEntity : Entity<TEntityId>
{
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IPaginate<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        int index = 0,
        int size = 10);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}
```

**Dynamic Query Support**:
- Filtreleme
- Sıralama
- Sayfalama

### Core.QrCodeGenerator

QR kod üretim servisi:

```csharp
public interface IQrCodeGeneratorService
{
    byte[] GenerateQrCode(string text);
}

public class QrCodeGeneratorService : IQrCodeGeneratorService
{
    public byte[] GenerateQrCode(string text)
    {
        using QRCodeGenerator qrGenerator = new();
        using QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, 
            QRCodeGenerator.ECCLevel.Q);
        using QRCode qrCode = new(qrCodeData);
        using Bitmap qrCodeImage = qrCode.GetGraphic(20);
        
        return BitmapToByteArray(qrCodeImage);
    }
}
```

---

## 🔄 İstek Akışı

### Tipik Bir HTTP İsteğinin Yaşam Döngüsü:

```
1. HTTP Request arrives at Controller
   ↓
2. Controller creates Command/Query object
   ↓
3. Command/Query sent to MediatR
   ↓
4. Pipeline Behaviors execute in order:
   - Authorization check
   - Validation
   - Caching check
   - Logging
   - Transaction begin
   ↓
5. Command/Query Handler executes:
   - Business rules check
   - Repository operations
   - Domain logic
   ↓
6. Response returns through pipeline:
   - Transaction commit/rollback
   - Cache update
   - Logging
   ↓
7. Controller returns HTTP Response
```

---

## 🗄️ Veritabanı Şeması

### Ana İlişkiler:

```
Company (1) ────── (*) Menu
   │                     │
   │                     └── (*) MenuQrCode
   │
   └── (*) Category ────── (*) Item
                              │
                              ├── (*) ItemImage
                              └── (1) ItemIngredient

User (1) ────── (*) UserOperationClaim ────── (1) OperationClaim
  │
  ├── (1) EmailAuthenticator
  └── (1) OtpAuthenticator
```

### Tablolar ve İlişkiler:

1. **Companies** (Şirketler)
   - CompanyTypes ile ilişkili (Many-to-One)
   - Categories ile ilişkili (One-to-Many)
   - Menus ile ilişkili (One-to-Many)

2. **Menus** (Menüler)
   - Companies ile ilişkili (Many-to-One)
   - MenuQrCodes ile ilişkili (One-to-Many)

3. **Categories** (Kategoriler)
   - Companies ile ilişkili (Many-to-One)
   - Items ile ilişkili (One-to-Many)

4. **Items** (Ürünler)
   - Categories ile ilişkili (Many-to-One)
   - ItemImages ile ilişkili (One-to-Many)
   - ItemIngredients ile ilişkili (One-to-One)

5. **Users** (Kullanıcılar)
   - UserOperationClaims ile ilişkili (One-to-Many)
   - EmailAuthenticators ile ilişkili (One-to-One)
   - OtpAuthenticators ile ilişkili (One-to-One)

---

## 📦 Dependency Injection

**Service Registration Yapısı**:

```csharp
// Program.cs
builder.Services.AddApplicationServices();      // Application layer
builder.Services.AddSecurityServices();         // Security services
builder.Services.AddPersistenceServices();      // Persistence layer
builder.Services.AddInfrastructureServices();   // Infrastructure layer
```

**Service Lifetime'lar**:
- **Scoped**: DbContext, Repositories, Business Rules
- **Transient**: MediatR Handlers, Validators
- **Singleton**: Configuration, Logging, Caching services

---

## 🔐 Güvenlik Mimarisi

### Authentication Flow:

```
1. User Login Request
   ↓
2. Credentials Validation
   ↓
3. Password Hash Verification
   ↓
4. Access Token Generation
   ↓
5. Refresh Token Generation & Store
   ↓
6. Return Tokens to Client
```

### Authorization Flow:

```
1. HTTP Request with Bearer Token
   ↓
2. JWT Token Validation
   ↓
3. User Claims Extraction
   ↓
4. Operation Claim Check (Role-based)
   ↓
5. Authorization Decision
   ↓
6. Allow/Deny Request
```

---

## 🚀 Performans Optimizasyonları

1. **Caching Strategy**:
   - In-memory caching (DistributedMemoryCache)
   - Cache invalidation on write operations
   - Configurable cache duration

2. **Database Optimizations**:
   - Index'ler
   - Query optimization
   - Eager/Lazy loading stratejisi
   - Connection pooling

3. **API Optimizations**:
   - Response compression
   - Rate limiting
   - Pagination
   - Asynchronous operations

---

## 📊 Monitoring ve Logging

**Logging Levels**:
- **Information**: Normal akış bilgileri
- **Warning**: Potansiyel problemler
- **Error**: Hata durumları
- **Critical**: Kritik sistem hataları

**Log Kategorileri**:
- HTTP Request/Response logs
- Database query logs
- Authentication/Authorization logs
- Business logic logs
- Exception logs

---

## 🧪 Test Stratejisi

**Test Katmanları**:

1. **Unit Tests**: Business logic ve handlers
2. **Integration Tests**: API endpoints ve database operations
3. **Repository Tests**: Data access layer

**Test Framework'leri**:
- xUnit
- Moq
- FluentAssertions

---

Bu mimari yapı, projenin ölçeklenebilir, bakımı kolay ve test edilebilir olmasını sağlar. Her katman kendi sorumluluğuna odaklanır ve diğer katmanlardan bağımsız olarak geliştirilebilir.
