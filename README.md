# 📚 Book Lending System

A robust **Clean Architecture** Web API built with **.NET 8** for managing book rentals. This project implements modern software engineering patterns and handles background processing for overdue notifications.

---

## 🏗️ Architecture & Design Patterns
The project is built following **Clean Architecture** principles to ensure separation of concerns and maintainability:
* **Unit of Work & Repository Pattern**: For centralized data access and transaction management.
* **Dependency Injection**: Ensuring loosely coupled components.
* **DTO Pattern**: Using **AutoMapper** to decouple Domain Entities from API Responses.

---

## 🚀 Technical Stack
* **Framework**: .NET 8.0 (Web API)
* **Identity**: ASP.NET Core Identity with **JWT Authentication**.
* **Database**: SQL Server with **Entity Framework Core**.
* **Background Jobs**: **Hangfire** for automated daily checks.
* **Mapping**: AutoMapper.
* **Testing**: **xUnit** & **NSubstitute** for Unit Testing.
* **Logging**: Built-in **ILogger** for system monitoring.

---

## ✨ Key Features
* **User Management**: Secure Register/Login system using JWT.
* **Borrowing Logic**: Users can borrow books for 7 days. The system prevents borrowing unavailable books.
* **Overdue Tracker**: A **Hangfire Recurring Job** runs every 24 hours to scan for overdue books and logs warnings using `ILogger`.
* **Professional API Documentation**: Fully documented using **Swagger UI**.
* **Automated Testing**: Unit tests to verify core business logic.

---

## 🛠️ Getting Started

### 1. Prerequisites
* .NET 8 SDK
* SQL Server

### 2. Configuration
Update the `ConnectionStrings` in `appsettings.json`:
```json
"ConnectionStrings": {
  "Booklending": "Server=(localdb)\\MSSQLLocalDB;Database=book_lending_api;Trusted_Connection=True;TrustServerCertificate=True;"
}
