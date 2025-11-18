# Geliştirici Kılavuzu

## 🎯 Başlangıç

Bu dokümantasyon, Cakir QR Menu projesine katkıda bulunmak isteyen geliştiriciler için hazırlanmıştır.

---

## 🛠️ Geliştirme Ortamı Kurulumu

### Gereksinimler

#### Zorunlu
- **.NET 8.0 SDK** veya üzeri
- **Visual Studio 2022** (17.8+) veya **Visual Studio Code**
- **SQL Server 2019+** veya **PostgreSQL 13+**
- **Git** (versiyon kontrolü için)

#### Önerilen
- **Postman** veya **Insomnia** (API testing)
- **SQL Server Management Studio** (SSMS)
- **Docker Desktop** (containerization için)
- **Redis** (caching için - opsiyonel)

### Proje Kurulumu

#### 1. Repository'yi Klonlama

```bash
# HTTPS ile
git clone https://github.com/ahmetcakr/Cakir-Qr.git

# veya SSH ile
git clone git@github.com:ahmetcakr/Cakir-Qr.git

cd Cakir-Qr
```

#### 2. Solution'ı Açma

```bash
# Visual Studio ile
start QrMenu.sln

# veya VS Code ile
code .
```

#### 3. NuGet Paketlerini Yükleme

```bash
dotnet restore QrMenu.sln
```

#### 4. Veritabanı Konfigürasyonu

`src/QrMenu/QrMenu.WebAPI/appsettings.json` dosyasını düzenleyin:

```json
{
  "ConnectionStrings": {
    "QrMenuConnectionString": "Server=localhost;Database=QrMenuDb;User Id=sa;Password=YourPassword123;TrustServerCertificate=True;"
  },
  "TokenOptions": {
    "Audience": "qrmenu.com",
    "Issuer": "qrmenu.com",
    "AccessTokenExpiration": 15,
    "SecurityKey": "your-super-secret-key-min-32-characters-long",
    "RefreshTokenTTL": 7
  },
  "WebApiConfiguration": {
    "AllowedOrigins": ["http://localhost:3000", "http://localhost:4200"]
  }
}
```

#### 5. Veritabanı Migration

```bash
# Persistence projesine gidin
cd src/QrMenu/QrMenu.Persistence

# Migration'ı uygulayın
dotnet ef database update --startup-project ../QrMenu.WebAPI

# veya Package Manager Console'da (Visual Studio)
Update-Database -StartupProject QrMenu.WebAPI -Project QrMenu.Persistence
```

#### 6. Uygulamayı Çalıştırma

```bash
cd src/QrMenu/QrMenu.WebAPI
dotnet run
```

Uygulama varsayılan olarak `https://localhost:5001` adresinde çalışacaktır.

#### 7. Swagger UI

Tarayıcınızda şu adresi açın:
```
https://localhost:5001/swagger
```

---

## 📁 Proje Yapısı

```
Cakir-Qr/
│
├── src/
│   ├── Core/                          # Temel altyapı modülleri
│   │   ├── Core.Application/          # CQRS, Pipelines, Base classes
│   │   ├── Core.Persistence/          # Repository pattern, EF Core base
│   │   ├── Core.Security/             # JWT, Hashing, Encryption
│   │   ├── Core.WebAPI/               # Base controllers, Extensions
│   │   ├── Core.CrossCuttingConcerns/ # Exception handling, Logging
│   │   ├── Core.QrCodeGenerator/      # QR kod servisi
│   │   ├── Core.Mailing/              # Email servisleri
│   │   ├── Core.ElasticSearch/        # Elasticsearch entegrasyonu
│   │   └── Core.Helpers/              # Utility fonksiyonlar
│   │
│   └── QrMenu/                        # İş mantığı katmanı
│       ├── QrMenu.Domain/             # Entity'ler, Domain models
│       ├── QrMenu.Application/        # Use cases, CQRS handlers
│       │   └── Features/              # Feature-based organization
│       │       ├── Auth/
│       │       ├── Companies/
│       │       ├── Menus/
│       │       ├── Categories/
│       │       └── Items/
│       ├── QrMenu.Persistence/        # Database context, Migrations
│       ├── QrMenu.Infrastructure/     # External services
│       └── QrMenu.WebAPI/             # API endpoints, Controllers
│
├── docs/                              # Dokümantasyon
│   ├── ARCHITECTURE.tr.md
│   ├── FEATURES.tr.md
│   ├── WORKFLOW.tr.md
│   └── DEVELOPER_GUIDE.tr.md
│
├── QrMenu.sln                         # Solution file
└── README.md
```

---

## 🏗️ Yeni Feature Ekleme

### Adım Adım Rehber

#### 1. Domain Entity Oluşturma

Önce domain entity'sini `QrMenu.Domain/Entities/` klasörüne ekleyin:

```csharp
// QrMenu.Domain/Entities/Review.cs
using Core.Persistence.Repositories;

namespace QrMenu.Domain.Entities;

public class Review : Entity<int>
{
    public int ItemId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    
    // Navigation properties
    public virtual Item Item { get; set; }
    public virtual User User { get; set; }
    
    public Review()
    {
        Id = 0;
        ItemId = 0;
        UserId = 0;
        Rating = 0;
        Comment = string.Empty;
    }
    
    public Review(int id, int itemId, int userId, int rating, string comment)
    {
        Id = id;
        ItemId = itemId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
    }
}
```

#### 2. Repository Interface ve Implementation

```csharp
// QrMenu.Application/Services/Repositories/IReviewRepository.cs
using Core.Persistence.Repositories;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Services.Repositories;

public interface IReviewRepository : IAsyncRepository<Review, int>
{
    Task<IList<Review>> GetReviewsByItemIdAsync(int itemId);
    Task<double> GetAverageRatingByItemIdAsync(int itemId);
}
```

```csharp
// QrMenu.Persistence/Repositories/ReviewRepository.cs
using Core.Persistence.Repositories;
using QrMenu.Application.Services.Repositories;
using QrMenu.Domain.Entities;
using QrMenu.Persistence.Contexts;

namespace QrMenu.Persistence.Repositories;

public class ReviewRepository : EfRepositoryBase<Review, int, QrMenuDbContext>, 
                                 IReviewRepository
{
    public ReviewRepository(QrMenuDbContext context) : base(context)
    {
    }
    
    public async Task<IList<Review>> GetReviewsByItemIdAsync(int itemId)
    {
        return await Context.Reviews
            .Where(r => r.ItemId == itemId)
            .OrderByDescending(r => r.CreatedDate)
            .ToListAsync();
    }
    
    public async Task<double> GetAverageRatingByItemIdAsync(int itemId)
    {
        return await Context.Reviews
            .Where(r => r.ItemId == itemId)
            .AverageAsync(r => r.Rating);
    }
}
```

#### 3. DbContext'e Ekleyin

```csharp
// QrMenu.Persistence/Contexts/QrMenuDbContext.cs
public class QrMenuDbContext : DbContext
{
    // ... existing DbSets
    public DbSet<Review> Reviews { get; set; }
    
    // ...
}
```

#### 4. Entity Configuration

```csharp
// QrMenu.Persistence/EntityConfigurations/ReviewConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QrMenu.Domain.Entities;

namespace QrMenu.Persistence.EntityConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews").HasKey(r => r.Id);
        
        builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
        builder.Property(r => r.ItemId).HasColumnName("ItemId").IsRequired();
        builder.Property(r => r.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(r => r.Rating).HasColumnName("Rating").IsRequired();
        builder.Property(r => r.Comment).HasColumnName("Comment").HasMaxLength(500);
        
        builder.HasOne(r => r.Item)
               .WithMany()
               .HasForeignKey(r => r.ItemId)
               .OnDelete(DeleteBehavior.Cascade);
               
        builder.HasOne(r => r.User)
               .WithMany()
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
```

#### 5. Migration Oluşturma

```bash
cd src/QrMenu/QrMenu.Persistence

# Migration oluştur
dotnet ef migrations add AddReviewEntity --startup-project ../QrMenu.WebAPI

# Migration'ı uygula
dotnet ef database update --startup-project ../QrMenu.WebAPI
```

#### 6. Application Layer - Feature Oluşturma

Feature klasör yapısı:

```
Features/
└── Reviews/
    ├── Commands/
    │   ├── Create/
    │   │   ├── CreateReviewCommand.cs
    │   │   ├── CreateReviewCommandValidator.cs
    │   │   └── CreatedReviewResponse.cs
    │   ├── Update/
    │   └── Delete/
    ├── Queries/
    │   ├── GetById/
    │   ├── GetList/
    │   └── GetByItemId/
    ├── Rules/
    │   └── ReviewBusinessRules.cs
    ├── Constants/
    │   └── ReviewMessages.cs
    └── Profiles/
        └── MappingProfile.cs
```

#### 7. Command Örneği

```csharp
// Features/Reviews/Commands/Create/CreateReviewCommand.cs
using MediatR;
using AutoMapper;
using QrMenu.Application.Services.Repositories;
using QrMenu.Domain.Entities;
using QrMenu.Application.Features.Reviews.Rules;

namespace QrMenu.Application.Features.Reviews.Commands.Create;

public class CreateReviewCommand : IRequest<CreatedReviewResponse>
{
    public int ItemId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    
    public class CreateReviewCommandHandler 
        : IRequestHandler<CreateReviewCommand, CreatedReviewResponse>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly ReviewBusinessRules _businessRules;
        
        public CreateReviewCommandHandler(
            IReviewRepository reviewRepository,
            IMapper mapper,
            ReviewBusinessRules businessRules)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }
        
        public async Task<CreatedReviewResponse> Handle(
            CreateReviewCommand request,
            CancellationToken cancellationToken)
        {
            await _businessRules.ItemShouldExist(request.ItemId);
            await _businessRules.UserShouldNotHaveReviewedItem(
                request.UserId, 
                request.ItemId);
            await _businessRules.RatingShouldBeValid(request.Rating);
            
            Review review = _mapper.Map<Review>(request);
            Review createdReview = await _reviewRepository.AddAsync(review);
            CreatedReviewResponse response = _mapper.Map<CreatedReviewResponse>(createdReview);
            
            return response;
        }
    }
}
```

#### 8. Validator

```csharp
// Features/Reviews/Commands/Create/CreateReviewCommandValidator.cs
using FluentValidation;

namespace QrMenu.Application.Features.Reviews.Commands.Create;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(r => r.ItemId)
            .GreaterThan(0).WithMessage("Ürün ID geçerli olmalı");
            
        RuleFor(r => r.UserId)
            .GreaterThan(0).WithMessage("Kullanıcı ID geçerli olmalı");
            
        RuleFor(r => r.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating 1-5 arasında olmalı");
            
        RuleFor(r => r.Comment)
            .MaximumLength(500).WithMessage("Yorum max 500 karakter olabilir");
    }
}
```

#### 9. Business Rules

```csharp
// Features/Reviews/Rules/ReviewBusinessRules.cs
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using QrMenu.Application.Services.Repositories;

namespace QrMenu.Application.Features.Reviews.Rules;

public class ReviewBusinessRules : BaseBusinessRules
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IItemRepository _itemRepository;
    
    public ReviewBusinessRules(
        IReviewRepository reviewRepository,
        IItemRepository itemRepository)
    {
        _reviewRepository = reviewRepository;
        _itemRepository = itemRepository;
    }
    
    public async Task ItemShouldExist(int itemId)
    {
        var item = await _itemRepository.GetAsync(i => i.Id == itemId);
        if (item == null)
            throw new BusinessException("Ürün bulunamadı");
    }
    
    public async Task UserShouldNotHaveReviewedItem(int userId, int itemId)
    {
        var existingReview = await _reviewRepository.GetAsync(
            r => r.UserId == userId && r.ItemId == itemId);
            
        if (existingReview != null)
            throw new BusinessException("Bu ürün için zaten değerlendirme yaptınız");
    }
    
    public Task RatingShouldBeValid(int rating)
    {
        if (rating < 1 || rating > 5)
            throw new BusinessException("Rating 1-5 arasında olmalı");
            
        return Task.CompletedTask;
    }
}
```

#### 10. Controller

```csharp
// QrMenu.WebAPI/Controllers/ReviewsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QrMenu.Application.Features.Reviews.Commands.Create;
using QrMenu.Application.Features.Reviews.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;

namespace QrMenu.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : BaseController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateReviewCommand command)
    {
        CreatedReviewResponse response = await Mediator.Send(command);
        return Created(uri: "", response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListReviewQuery query = new() { PageRequest = pageRequest };
        GetListResponse<GetListReviewListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdReviewQuery query = new() { Id = id };
        GetByIdReviewResponse response = await Mediator.Send(query);
        return Ok(response);
    }
}
```

#### 11. Dependency Injection Registration

```csharp
// QrMenu.Persistence/PersistenceServiceRegistration.cs
public static IServiceCollection AddPersistenceServices(
    this IServiceCollection services,
    IConfiguration configuration)
{
    // ... existing registrations
    services.AddScoped<IReviewRepository, ReviewRepository>();
    
    return services;
}
```

```csharp
// QrMenu.Application/ApplicationServiceRegistration.cs
public static IServiceCollection AddApplicationServices(
    this IServiceCollection services)
{
    // ... existing registrations
    services.AddScoped<ReviewBusinessRules>();
    
    return services;
}
```

---

## 🧪 Test Yazma

### Unit Test Örneği

```csharp
// QrMenu.Application.Tests/Features/Reviews/Commands/CreateReviewCommandTests.cs
using Xunit;
using Moq;
using AutoMapper;
using QrMenu.Application.Features.Reviews.Commands.Create;
using QrMenu.Application.Services.Repositories;
using QrMenu.Application.Features.Reviews.Rules;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Tests.Features.Reviews.Commands;

public class CreateReviewCommandTests
{
    private readonly Mock<IReviewRepository> _mockReviewRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ReviewBusinessRules> _mockBusinessRules;
    private readonly CreateReviewCommand.CreateReviewCommandHandler _handler;
    
    public CreateReviewCommandTests()
    {
        _mockReviewRepository = new Mock<IReviewRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockBusinessRules = new Mock<ReviewBusinessRules>(
            _mockReviewRepository.Object,
            Mock.Of<IItemRepository>());
            
        _handler = new CreateReviewCommand.CreateReviewCommandHandler(
            _mockReviewRepository.Object,
            _mockMapper.Object,
            _mockBusinessRules.Object);
    }
    
    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreatedResponse()
    {
        // Arrange
        var command = new CreateReviewCommand
        {
            ItemId = 1,
            UserId = 1,
            Rating = 5,
            Comment = "Great item!"
        };
        
        var review = new Review(1, 1, 1, 5, "Great item!");
        var expectedResponse = new CreatedReviewResponse { Id = 1 };
        
        _mockMapper.Setup(m => m.Map<Review>(command)).Returns(review);
        _mockReviewRepository.Setup(r => r.AddAsync(review))
            .ReturnsAsync(review);
        _mockMapper.Setup(m => m.Map<CreatedReviewResponse>(review))
            .Returns(expectedResponse);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        _mockReviewRepository.Verify(r => r.AddAsync(It.IsAny<Review>()), Times.Once);
    }
}
```

---

## 🎨 Kod Standartları

### Naming Conventions

#### C# Naming

```csharp
// PascalCase for classes, methods, properties
public class ReviewService { }
public void ProcessReview() { }
public string ItemName { get; set; }

// camelCase for local variables, parameters
var reviewCount = 10;
public void AddReview(Review review) { }

// _camelCase for private fields
private readonly IReviewRepository _reviewRepository;

// ALL_CAPS for constants
public const int MAX_RATING = 5;
```

#### File Naming

```
- Entity: Review.cs
- Command: CreateReviewCommand.cs
- Query: GetListReviewQuery.cs
- Repository Interface: IReviewRepository.cs
- Repository Implementation: ReviewRepository.cs
- Controller: ReviewsController.cs (plural)
- Validator: CreateReviewCommandValidator.cs
```

### Code Style

```csharp
// ✅ İyi
public async Task<Review> GetReviewAsync(int id)
{
    if (id <= 0)
        throw new ArgumentException("Id must be positive", nameof(id));
        
    var review = await _reviewRepository.GetAsync(r => r.Id == id);
    
    if (review == null)
        throw new NotFoundException("Review not found");
        
    return review;
}

// ❌ Kötü
public async Task<Review> GetReviewAsync(int id)
{
    var review=await _reviewRepository.GetAsync(r=>r.Id==id);
    if(review==null) throw new NotFoundException("Review not found");
    return review;
}
```

### SOLID Principles

#### Single Responsibility Principle

```csharp
// ✅ İyi - Her sınıf tek sorumluluk
public class ReviewService
{
    public Task<Review> GetReviewAsync(int id) { }
}

public class ReviewNotificationService
{
    public Task SendReviewNotificationAsync(Review review) { }
}

// ❌ Kötü - Çok fazla sorumluluk
public class ReviewService
{
    public Task<Review> GetReviewAsync(int id) { }
    public Task SendNotification(Review review) { }
    public Task GenerateReport(List<Review> reviews) { }
}
```

---

## 🔍 Debugging

### Logging Kullanımı

```csharp
public class CreateReviewCommandHandler 
    : IRequestHandler<CreateReviewCommand, CreatedReviewResponse>
{
    private readonly ILogger<CreateReviewCommandHandler> _logger;
    
    public CreateReviewCommandHandler(
        ILogger<CreateReviewCommandHandler> logger,
        // other dependencies
    )
    {
        _logger = logger;
    }
    
    public async Task<CreatedReviewResponse> Handle(
        CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating review for ItemId: {ItemId} by UserId: {UserId}",
            request.ItemId,
            request.UserId);
            
        try
        {
            // ... business logic
            
            _logger.LogInformation(
                "Review created successfully with Id: {ReviewId}",
                createdReview.Id);
                
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error creating review for ItemId: {ItemId}",
                request.ItemId);
            throw;
        }
    }
}
```

### Breakpoint Stratejileri

1. **Command/Query Handler başlangıcı**: İstek verilerini kontrol edin
2. **Business Rules**: Validasyon kontrollerini inceleyin
3. **Repository operations**: Database sorgularını kontrol edin
4. **Mapping işlemleri**: DTO dönüşümlerini inceleyin

---

## 📝 Git Workflow

### Branch Naming

```bash
# Feature branches
feature/add-review-system
feature/update-menu-api

# Bug fix branches
bugfix/fix-qr-generation
bugfix/fix-auth-token-expiry

# Hotfix branches
hotfix/critical-security-fix
```

### Commit Messages

Semantic Commit Messages kullanın:

```bash
# Format
<type>(<scope>): <subject>

# Örnekler
feat(reviews): add review creation feature
fix(auth): fix token expiration issue
docs(readme): update installation instructions
refactor(repositories): simplify query methods
test(reviews): add unit tests for review service
chore(deps): update nuget packages
```

### Commit Types

- `feat`: Yeni feature
- `fix`: Bug fix
- `docs`: Dokümantasyon
- `style`: Code style (format, semicolons)
- `refactor`: Code refactoring
- `test`: Test ekleme/güncelleme
- `chore`: Build, dependencies

### Pull Request Süreci

1. Feature branch oluşturun
```bash
git checkout -b feature/add-review-system
```

2. Değişikliklerinizi yapın ve commit edin
```bash
git add .
git commit -m "feat(reviews): add review creation command"
```

3. Remote'a push edin
```bash
git push origin feature/add-review-system
```

4. GitHub'da Pull Request oluşturun
5. Code review bekleyin
6. Gerekli değişiklikleri yapın
7. Merge edilmesini bekleyin

---

## 🚀 Deployment

### Production Checklist

- [ ] Tüm testler başarılı
- [ ] Code review tamamlandı
- [ ] appsettings.Production.json yapılandırıldı
- [ ] Connection string'ler güncellendi
- [ ] Secret key'ler environment variables'a taşındı
- [ ] Migration'lar production DB'ye uygulandı
- [ ] Logging yapılandırması kontrol edildi
- [ ] CORS ayarları production için güncellendi
- [ ] SSL certificate yapılandırıldı

### Docker Deployment (Opsiyonel)

```dockerfile
# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/QrMenu/QrMenu.WebAPI/QrMenu.WebAPI.csproj", "src/QrMenu/QrMenu.WebAPI/"]
RUN dotnet restore "src/QrMenu/QrMenu.WebAPI/QrMenu.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/QrMenu/QrMenu.WebAPI"
RUN dotnet build "QrMenu.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QrMenu.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QrMenu.WebAPI.dll"]
```

```bash
# Build Docker image
docker build -t qrmenu-api .

# Run container
docker run -p 5000:80 qrmenu-api
```

---

## 📚 Faydalı Kaynaklar

### Dokümantasyon
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [MediatR Documentation](https://github.com/jbogard/MediatR/wiki)
- [AutoMapper Documentation](https://docs.automapper.org/)
- [FluentValidation Documentation](https://docs.fluentvalidation.net/)

### Best Practices
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)
- [Repository Pattern](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)

### Tools
- [Postman](https://www.postman.com/) - API testing
- [Swagger](https://swagger.io/) - API documentation
- [SonarLint](https://www.sonarlint.org/) - Code quality
- [ReSharper](https://www.jetbrains.com/resharper/) - Code analysis

---

## 🤝 Katkıda Bulunma

1. Projeyi fork edin
2. Feature branch oluşturun
3. Değişikliklerinizi commit edin
4. Branch'inizi push edin
5. Pull Request oluşturun

### Code Review Checklist

- [ ] Kod clean ve okunabilir
- [ ] SOLID prensiplere uygun
- [ ] Naming conventions'a uygun
- [ ] Yeterli test coverage
- [ ] Dokümantasyon güncel
- [ ] Exception handling yapılmış
- [ ] Logging eklenmiş
- [ ] Performance optimize edilmiş

---

## 🐛 Sorun Bildirme

GitHub Issues kullanarak sorun bildirebilirsiniz:

1. Issue template'i kullanın
2. Detaylı açıklama yapın
3. Hata mesajlarını ekleyin
4. Adım adım reproduce senaryosu yazın
5. Environment bilgilerini paylaşın

---

Bu kılavuz, projeye katkıda bulunmak için gerekli tüm bilgileri içermektedir. Sorularınız için GitHub Issues veya Discussions kullanabilirsiniz.
