# Repository Pattern & Unit of Work Template

A modern, clean, and extensible template for implementing the Repository Pattern and Unit of Work in C# using Entity Framework Core. This template is designed to accelerate the development of robust, maintainable, and testable .NET applications by promoting the separation of concerns and abstracting data access logic.

---

![Architecture Diagram](https://pradeepl.com/blog/repository-and-unit-of-work-pattern-asp-net-core/images/Repository-and-unit-of-work-pattern.png)

---

## Features

- **Generic Repository:** Reusable repository for standard CRUD operations on any entity.
- **Specific Repositories:** Extend generic functionality for domain-specific queries (e.g., products, categories).
- **Unit of Work:** Ensures atomic transactions and coordinates repository operations.
- **Async/Await:** All data operations are asynchronous for scalable, non-blocking performance.
- **Entity Framework Core:** Leverages EF Core for data access and LINQ queries.
- **Dependency Injection Ready:** Easily integrates with modern .NET DI containers.
- **Transaction Management:** Supports explicit transaction control with commit and rollback.

---

## Technology Stack

- **.NET 8**
- **C#**
- **Entity Framework Core**
- **FluentValidation**
- **AutoMapper**
- **Swashbuckle (Swagger)**

---

## Architecture Overview

- **Domain Layer:** Core business entities and interfaces.
- **Infrastructure Layer:** Concrete implementations for repositories and Unit of Work.
- **API Layer:** ASP.NET Core Web API setup for exposing endpoints.
- **Application Layer:** DTOs, mapping profiles, validation, and services.
- **Tests:** Unit and integration test projects.

---

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (or update connection string for your DB)
- [Optional] Visual Studio 2022+ or Rider

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/Tareq-Bilal/Repository-Pattern-Unit-Of-Work-Template.git
   ```

2. **Configure the Database**
   - Update your connection string in `appsettings.json` (`DefaultConnection`).

3. **Apply Migrations**
   ```bash
   dotnet ef database update --project src/MyApp.Infrastructure
   ```

4. **Run the API**
   ```bash
   dotnet run --project src/MyApp.API
   ```

5. **Swagger UI**
   - Navigate to `http://localhost:<port>/swagger` for interactive API documentation.

---

## Usage Example

### Registering Services

```csharp
// In Program.cs or Startup.cs
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
```

### Working with Repositories & Unit of Work

```csharp
public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddProductAsync(Product product)
    {
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveAsync();
    }
}
```

---

## Project Structure

```
src/
  MyApp.Core/           # Domain entities and repository interfaces
  MyApp.Infrastructure/ # EF Core DbContext, repository implementations, UnitOfWork
  MyApp.API/            # ASP.NET Core Web API
  MyApp.Application/    # DTOs, mappings, validators, services
tests/
  MyApp.Tests/          # Unit and integration tests
```

---

## Contributing

Contributions, issues, and feature requests are welcome! Feel free to open a [pull request](https://github.com/Tareq-Bilal/Repository-Pattern-Unit-Of-Work-Template/pulls) or [issue](https://github.com/Tareq-Bilal/Repository-Pattern-Unit-Of-Work-Template/issues).

---

## License

This project is open source. See the [LICENSE](LICENSE) file for details.

---

## Contact

Created by [Tareq Bilal](https://github.com/Tareq-Bilal).
