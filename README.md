# 🛒 E-Commerce APIs

A full-featured RESTful E-Commerce backend built with **ASP.NET Core 10**, following a clean **3-Layer Architecture** (DAL → BLL → API). Supports product & category management, cart, orders, image uploads, JWT authentication, and role-based authorization.

---

## Table of Contents

- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Project Structure](#-project-structure)
- [Database ERD](#-database-erd)
- [Getting Started](#-getting-started)
- [Configuration](#-configuration)
- [API Endpoints](#-api-endpoints)
- [Authentication](#-authentication)
- [Roles & Policies](#-roles--policies)
- [Image Upload](#-image-upload)
- [Pagination & Filtering](#-pagination--filtering)

---

## Features

- JWT Authentication (Register / Login)
- Role-based Authorization (Admin / User)
- Product CRUD with filtering, sorting, and pagination
- Category CRUD with product count
- Cart Management (add, update, remove, clear)
- Order Processing (place, view, cancel, update status)
- Image Upload for Products and Categories
- FluentValidation on all inputs
- CORS Enabled
- Scalar API Reference (OpenAPI)
- Audit Logging (CreatedAt / UpdatedAt auto-filled)

---

## 🛠 Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 10 |
| ORM | Entity Framework Core 10 |
| Database | SQL Server |
| Authentication | JWT Bearer |
| Identity | ASP.NET Core Identity |
| Validation | FluentValidation |
| API Docs | Scalar / OpenAPI |
| Architecture | 3-Layer (DAL / BLL / API) |

---

## 🏗 Architecture

```
┌─────────────────────────────────────┐
│            API Layer                │
│  Controllers → DTOs → Managers      │
└────────────────┬────────────────────┘
                 │
┌────────────────▼────────────────────┐
│           BLL Layer                 │
│  Managers, Validators, DTOs,        │
│  Filtering, Pagination              │
└────────────────┬────────────────────┘
                 │
┌────────────────▼────────────────────┐
│           DAL Layer                 │
│  DbContext, Models, Repositories,   │
│  UnitOfWork, Migrations             │
└─────────────────────────────────────┘
```

### Design Patterns Used
- **Repository Pattern** — per entity (`ProductRepository`, `CategoryRepository`, etc.)
- **Unit of Work** — wraps all repositories under one `Save()` call
- **Generic Repository** — base CRUD reused across all entities
- **DTO Pattern** — separate Create / Edit / Read DTOs per entity
- **Result Pattern** — `GeneralResult<T>` wraps every response with `Success`, `Data`, `Message`, `Errors`

---

## 📁 Project Structure

```
E-Commerce APIs/
│
├── DAL/                               # Data Access Layer
│   ├── Data/
│   │   ├── Context/
│   │   │   └── AppDbContext.cs
│   │   ├── Models/
│   │   │   ├── Product.cs
│   │   │   ├── Category.cs
│   │   │   ├── Cart.cs
│   │   │   ├── CartItem.cs
│   │   │   ├── Order.cs
│   │   │   ├── OrderItem.cs
│   │   │   ├── OrderStatus.cs
│   │   │   ├── ApplicationUser.cs
│   │   │   ├── ApplicationRole.cs
│   │   │   └── IAuditableEntity.cs
│   │   └── Configuration/
│   ├── Repositories/
│   │   ├── GenericRepository/
│   │   ├── ProductRepository/
│   │   ├── CategoryRepository/
│   │   ├── CartRepository/
│   │   └── OrderRepository/
│   ├── UnitOfWork/
│   │   ├── IUnitOfWork.cs
│   │   └── UnitOfWork.cs
│   ├── Migrations/
│   └── ServicesExtension/
│       └── DALServicesExtension.cs
│
├── BLL/                               # Business Logic Layer
│   ├── DTO/
│   │   ├── Auth/
│   │   ├── Product/
│   │   ├── Category/
│   │   ├── Cart/
│   │   ├── Order/
│   │   ├── Image/
│   │   └── Role/
│   ├── Managers/
│   │   ├── Product/
│   │   ├── Category/
│   │   ├── Cart/
│   │   ├── Order/
│   │   └── Image/
│   ├── Validator/
│   ├── Filtering/
│   ├── Pagination/
│   ├── GeneralResult/
│   ├── ErrorMapper/
│   ├── Settings/
│   │   └── JwtSettings.cs
│   ├── AuthPolicies.cs
│   └── ServicesExtension/
│       └── BLLServicesExtension.cs
│
└── E-Commerce APIs/                   # Presentation Layer
    ├── Controllers/
    │   ├── AuthController.cs
    │   ├── ProductsController.cs
    │   ├── CategoryController.cs
    │   ├── CartController.cs
    │   ├── OrdersController.cs
    │   ├── ImagesController.cs
    │   └── RolesController.cs
    ├── Files/                         # Uploaded images
    ├── appsettings.json
    └── Program.cs
```

---

## 🗄 Database ERD

### Entities & Relationships

```
ApplicationUser
    │
    ├──< Cart (1 user → 1 cart)
    │       └──< CartItem >── Product
    │
    └──< Order (1 user → many orders)
            └──< OrderItem >── Product
                                │
                            Category
```

### Full ERD

| Table | PK | FKs | Key Fields |
|---|---|---|---|
| `AspNetUsers` | `Id` | — | FirstName, LastName, Email, CreatedAt |
| `AspNetRoles` | `Id` | — | Name, Description |
| `Categories` | `Id` | — | Name, Description, ImageURL, Slug, CreatedAt |
| `Products` | `Id` | `CategoryId` | Title, Price, Count, Unit, Rating, IsOrganic, IsFeatured, ImageURL |
| `Carts` | `Id` | `UserId` | CreatedAt *(unique per user)* |
| `CartItems` | `Id` | `CartId`, `ProductId` | Quantity, UnitPrice *(unique CartId+ProductId)* |
| `Orders` | `Id` | `UserId` | Status, TotalPrice, PaymentMethod, ShippingAddress*, CreatedAt |
| `OrderItems` | `Id` | `OrderId`, `ProductId` | Quantity, UnitPrice |

> *ShippingAddress is stored as flat columns: `ShippingFullName`, `ShippingAddress`, `ShippingCity`, `ShippingCountry`, `ShippingPhone`

### Relationships

| From | To | Type |
|---|---|---|
| `ApplicationUser` | `Cart` | One-to-One |
| `Cart` | `CartItem` | One-to-Many |
| `CartItem` | `Product` | Many-to-One |
| `ApplicationUser` | `Order` | One-to-Many |
| `Order` | `OrderItem` | One-to-Many |
| `OrderItem` | `Product` | Many-to-One |
| `Category` | `Product` | One-to-Many |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or full instance)
- Visual Studio 2022+ or VS Code

### Installation

```bash
# 1. Clone the repository
git clone https://github.com/your-username/ecommerce-apis.git
cd ecommerce-apis

# 2. Restore packages
dotnet restore

# 3. Update the connection string in appsettings.json (see Configuration)

# 4. Apply migrations
dotnet ef database update --project DAL --startup-project "E-Commerce APIs"

# 5. Run the project
dotnet run --project "E-Commerce APIs"
```

### Default Seeded Admin Account

| Field | Value |
|---|---|
| Email | `admin@company.com` |
| Password | `Admin@123` |
| Role | Admin |

---

## ⚙️ Configuration

Edit `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ECommerceDB;Trusted_Connection=True;"
  },
  "JwtSettings": {
    "SecretKey": "YOUR_BASE64_SECRET_KEY_HERE",
    "Issuer": "ECommerceApp",
    "Audience": "ECommerceUsers",
    "DurationInMinutes": 60
  }
}
```

> ⚠️ `SecretKey` must be a **Base64-encoded** string of at least 32 bytes.

Generate a key:
```csharp
Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(32))
```

---

## 📡 API Endpoints

### 🔐 Auth — `/api/auth`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/auth/register` | Public | Register new user |
| `POST` | `/api/auth/login` | Public | Login and get JWT token |

**Register Body:**
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "password": "Pass@123",
  "role": "User"
}
```

**Login Body:**
```json
{
  "email": "john@example.com",
  "password": "Pass@123"
}
```

---

### 📦 Products — `/api/products`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `GET` | `/api/products` | User / Admin | Get all products (filterable, paginated) |
| `GET` | `/api/products/{id}` | User / Admin | Get product by ID |
| `POST` | `/api/products` | Admin only | Create product |
| `PUT` | `/api/products/{id}` | Admin only | Update product |
| `DELETE` | `/api/products/{id}` | Admin only | Delete product |
| `POST` | `/api/products/{id}/image` | Admin only | Upload product image |

**Query Params for GET /api/products:**

| Param | Type | Description |
|---|---|---|
| `search` | string | Filter by title |
| `minPrice` | decimal | Minimum price |
| `maxPrice` | decimal | Maximum price |
| `isOrganic` | bool | Filter organic products |
| `isFeatured` | bool | Filter featured products |
| `categoryId` | int | Filter by category |
| `sortBy` | string | `price`, `title`, `rating` |
| `sortDescending` | bool | Sort direction |
| `pageNumber` | int | Page number (default: 1) |
| `pageSize` | int | Items per page (default: 50) |

---

### 🗂️ Categories — `/api/categories`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `GET` | `/api/categories` | User / Admin | Get all categories |
| `GET` | `/api/categories/{id}` | User / Admin | Get category by ID (with product count) |
| `POST` | `/api/categories` | Admin only | Create category |
| `PUT` | `/api/categories/{id}` | Admin only | Update category |
| `DELETE` | `/api/categories/{id}` | Admin only | Delete category |
| `POST` | `/api/categories/{id}/image` | Admin only | Upload category image |

---

### 🛒 Cart — `/api/cart`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `GET` | `/api/cart` | Authenticated | Get current user's cart |
| `POST` | `/api/cart` | Authenticated | Add item to cart |
| `PUT` | `/api/cart` | Authenticated | Update item quantity |
| `DELETE` | `/api/cart/{productId}` | Authenticated | Remove item from cart |
| `DELETE` | `/api/cart` | Authenticated | Clear entire cart |

**Add to Cart Body:**
```json
{
  "productId": 101,
  "quantity": 2
}
```

**Update Cart Item Body:**
```json
{
  "productId": 101,
  "newQuantity": 5
}
```

---

### 📋 Orders — `/api/orders`

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/orders` | Authenticated | Place order from cart |
| `GET` | `/api/orders` | Authenticated | Get my orders |
| `GET` | `/api/orders/{id}` | Authenticated | Get order details |
| `PUT` | `/api/orders/{id}/status` | Admin only | Update order status |
| `DELETE` | `/api/orders/{id}/cancel` | Authenticated | Cancel order |

**Place Order Body:**
```json
{
  "paymentMethod": "cod",
  "shippingAddress": {
    "fullName": "John Doe",
    "address": "123 Main St",
    "city": "Cairo",
    "country": "Egypt",
    "phone": "+201234567890"
  }
}
```

**Order Status Values:**

| Value | Status |
|---|---|
| `0` | Pending |
| `1` | Processing |
| `2` | Shipped |
| `3` | Delivered |
| `4` | Cancelled |

> ⚠️ Orders with status `Shipped` or `Delivered` cannot be cancelled.

---

## 🔐 Authentication

All protected endpoints require a **Bearer Token** in the `Authorization` header:

```
Authorization: Bearer <your_jwt_token>
```

Tokens are returned from `/api/auth/login` and expire based on `DurationInMinutes` in config.

---

## 👥 Roles & Policies

| Policy | Roles | Used On |
|---|---|---|
| `AdminOnly` | Admin | Create/Update/Delete products, categories, update order status |
| `AdminOrUser` | Admin, User | Read products, categories |
| `AuthenticatedUser` | Any logged-in user | Cart, Orders |

---

## 🖼️ Image Upload

Images are stored locally in the `/Files` directory and served as static files.

**Endpoint:** `POST /api/products/{id}/image` or `POST /api/categories/{id}/image`

**Request:** `multipart/form-data`

| Field | Type | Constraints |
|---|---|---|
| `file` | `IFormFile` | Required, max 5MB, `.jpg` / `.jpeg` / `.png` |

**Response:**
```json
{
  "success": true,
  "data": {
    "imageURL": "https://localhost:5001/Files/my-image-abc123.jpg"
  }
}
```

---

## 📄 Pagination & Filtering

All paginated responses return a `PagedResult<T>`:

```json
{
  "success": true,
  "data": {
    "items": [...],
    "metadata": {
      "currentPage": 1,
      "pageSize": 10,
      "totalCount": 85,
      "totalPages": 9,
      "hasNext": true,
      "hasPrevious": false
    }
  }
}
```

---

## 🌐 CORS

CORS is enabled for all origins in development. Configure in `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
```

---

## 📖 API Documentation

Scalar UI is available in Development at:

```
https://localhost:{port}/scalar
```

---

## 🌱 Seeded Data

The database is pre-seeded with:
- **2 Roles:** Admin, User
- **1 Admin User:** `admin@company.com` / `Admin@123`
- **8 Categories:** Fresh Fruits, Vegetables, Herbs & Green, Dried Fruits, Dried Vegetables, Nuts, Ready to Cook, Ready-to-use Vegetables
- **24 Products** across all categories

---

## 📝 GeneralResult Pattern

Every API response is wrapped in `GeneralResult<T>`:

```json
// Success
{
  "success": true,
  "data": { ... }
}

// Validation Error
{
  "success": false,
  "errors": ["Title is required", "Price must be > 0"]
}

// Not Found
{
  "success": false,
  "message": "Resource Not Found"
}
```
