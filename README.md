# 🛒 ECommerce API

This project is a modular, scalable, and production-ready **ECommerce RESTful API** built with **.NET 9**, **Clean Architecture**, **CQRS**, and **MediatR** patterns. The backend is designed for secure, fast, and maintainable development using cutting-edge technologies such as **JWT Authentication**, **EF Core**, **Scalar (OpenAPI UI)**, **FluentValidation**, and **AutoMapper**.

---

## 📁 Project Structure

The project follows the **Clean Architecture** pattern with clearly separated layers:

```
ECommerce/
├── ECommerce.WebAPI             --> API Layer (Controllers, Middlewares)
├── ECommerce.Application        --> CQRS, Validators, Business Rules
├── ECommerce.Infrastructure     --> Repository, Services, JWT, EF Core
├── ECommerce.Domain             --> Entities, Abstractions
└── ECommerce.Persistence        --> (if used) Seeding, DbContext, Configurations
```

---

## ✅ Features

- 🔐 **JWT Authentication** with login and register functionality
- 🧾 **Product & Order Management** with CRUD operations
- 📦 **MediatR + CQRS** for decoupled architecture
- 📊 **Scalar** instead of Swagger for API documentation
- 🧠 **FluentValidation** for request validation
- 🔄 **AutoMapper** for clean DTO mapping
- ✅ **Authorization** via `[Authorize]` attribute
- 🔎 **EF Core** with repository pattern and `Include` support for navigation properties

---

## 🚀 Getting Started

### 🔧 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server or Dockerized SQL Server

### ⚙️ Configuration

Make sure to configure your `appsettings.json` like:

```json
"JwtSettings": {
  "Issuer": "your-issuer",
  "Audience": "your-audience",
  "ExpireMinutes": 60,
  "SecretKey": "your-super-secret-key"
}
```

Also ensure connection string is set in `appsettings.json`:
```json
"ConnectionStrings": {
  "SqlServer": "Server=localhost;Database=ECommerceDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### ▶️ Run the App

```bash
dotnet build
dotnet ef database update
dotnet run --project ECommerce.WebAPI
```

Go to: `https://localhost:PORT/scalar/v1`

Use `Bearer eyJ...` token on Scalar Authorize button to test protected endpoints.

---

## 📌 API Examples

### 🔑 Register / Login
- `POST /api/auth/register`
- `POST /api/auth/login`

### 📦 Products
- `GET /api/products`
- `POST /api/products`
- `DELETE /api/products/{id}`

### 📦 Orders
- `GET /api/orders`
- `GET /api/orders/{id}`
- `POST /api/orders`

---

## 📚 Technologies

- ASP.NET Core 9
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- Scalar UI for OpenAPI
- MediatR & CQRS Pattern
- AutoMapper
- FluentValidation



## 👤 Author

GitHub: [batucglyn](https://github.com/batucglyn)