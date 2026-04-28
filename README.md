# 🛒 KSHOP — E-Commerce REST API

A full-featured E-Commerce RESTful API built with **ASP.NET Core**, following a clean **N-Tier Architecture** pattern.

---

## 📐 Architecture

The solution is divided into three layers:

```
KSHOP/
├── KSHOP.PL/       → Presentation Layer (ASP.NET Core Web API — Controllers, Middlewares)
├── KSHOP.BLL/      → Business Logic Layer (Services, DTOs, Interfaces)
├── KSHOP.DAL/      → Data Access Layer (EF Core, Repositories, Models, Migrations)
└── KSHOP.sln
```

| Layer | Responsibility |
|-------|----------------|
| **PL** | Exposes HTTP endpoints, handles requests & responses |
| **BLL** | Contains business rules, validations, and service logic |
| **DAL** | Interacts with the database via Entity Framework Core |

---

## 🚀 Features

- ✅ Product management (CRUD)
- ✅ Category management
- ✅ User authentication & authorization (JWT)
- ✅ Shopping cart
- ✅ Order management
- ✅ RESTful API design
- ✅ Repository
- ✅ AutoMapper for DTO mapping


---

## 🛠️ Tech Stack

| Technology | Usage |
|------------|-------|
| ASP.NET Core | Web API framework |
| Entity Framework Core | ORM / Database access |
| SQL Server | Database |
| JWT Bearer | Authentication |
| AutoMapper | Object mapping |


---

## ⚙️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code



## 📡 API Endpoints

### 🔐 Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Login and receive JWT token |

### 📦 Products
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Get all products |
| GET | `/api/products/{id}` | Get product by ID |
| POST | `/api/products` | Create a new product |
| PUT | `/api/products/{id}` | Update product  |
| DELETE | `/api/products/{id}` | Delete product  |

### 🗂️ Categories
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/categories` | Get all categories |
| POST | `/api/categories` | Create category  |

### 🛒 Cart & Orders
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/cart` | Get current user's cart |
| POST | `/api/cart` | Add item to cart |
| DELETE | `/api/cart/{id}` | Remove item from cart |
| POST | `/api/orders` | Place an order |
| GET | `/api/orders` | Get user's orders |

---

## 🗄️ Database Schema (Overview)

```
Users ──< Orders ──< OrderItems >── Products >── Categories
                                        │
                                     CartItems
```

---

## 📁 Project Structure

```
KSHOP.DAL/
├── Models/           → Entity classes (Product, Order, User, ...)
├── Repositories/     → Generic & specific repository implementations
├── Data/             → AppDbContext + Migrations

KSHOP.BLL/
├── Services/         → Business logic services
├── DTOs/             → Data Transfer Objects
├── Interfaces/       → Service & repository contracts

KSHOP.PL/
├── Controllers/      → API Controllers
├── Middlewares/       → Error handling, etc.
├── Program.cs        → App configuration & DI
└── appsettings.json  → Configuration
```

---

## 👩‍💻 Author

**Leen Mahdi**
- GitHub: [@LeenMahdi345](https://github.com/LeenMahdi345)

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).
