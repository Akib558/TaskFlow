# 🚀 TaskFlow

A clean, scalable **Task Management System** built with **.NET 7**, following best practices in architecture, authentication, and API development.  
Designed for professional use and future extension with a full **Angular frontend**.

---

## ✨ Highlights

- 🔒 **Authentication** with secure JWT tokens
- 🛡️ **Role-Based Access Control (RBAC)**
- 📄 **RESTful API** for tasks, projects, and users
- 🛠 **FluentValidation** for robust request validation
- 🐳 **Dockerized SQL Server** setup
- 🧹 **Clean Architecture** with Service-Repository patterns
- 🐞 **Custom Middleware** for error handling and logging
- 📚 **Swagger** integration for API exploration

---

## 🧩 Tech Stack

| Layer            | Technology                 |
| ---------------- | --------------------------- |
| Backend API      | .NET 7 Web API               |
| Database         | SQL Server (via Docker)      |
| ORM              | Dapper (raw SQL queries)     |
| Authentication   | JWT Tokens                   |
| Validation       | FluentValidation             |
| Frontend (Planned) | Angular 19                  |
| API Documentation | Swagger (OpenAPI)            |

---

## 📂 Project Structure

```
TaskFlow/
├── Core/            # Entities, DTOs, Enums
├── Data/            # Database context and migrations
├── Helpers/         # Helper classes and constants
├── Middlewares/     # Custom middleware (error handling, auth)
├── Repositories/    # Data access logic
├── Services/        # Business logic layer
├── Utilities/       # Shared utilities
├── WebAPI/          # API controllers and startup configs
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- SQL Server Docker image

### Setup Guide

1. **Clone the Repository**

   ```bash
   git clone https://github.com/Akib558/TaskFlow.git
   cd TaskFlow
   ```

2. **Run SQL Server in Docker**

   ```bash
   docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourPassword123' -p 4001:1433 --name taskflow-mssql -d mcr.microsoft.com/mssql/server:2019-latest
   ```

3. **Apply Migrations**

   If using EF Core migrations (or setup scripts manually):

   ```bash
   dotnet ef database update
   ```

4. **Run the API**

   ```bash
   dotnet run --project TaskFlow.WebAPI
   ```

5. **Access API Documentation**

   Open: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 📋 Core APIs

| Method | Route                 | Purpose              |
| :----: | :-------------------- | :------------------- |
| POST   | `/api/auth/login`       | Authenticate a user  |
| POST   | `/api/auth/register`    | Register a new user  |
| GET    | `/api/tasks`            | Get list of tasks    |
| POST   | `/api/tasks`            | Create a new task    |
| PUT    | `/api/tasks/{id}`        | Update a task        |
| DELETE | `/api/tasks/{id}`        | Delete a task        |

---

## 🎯 Future Roadmap

- ✨ Full Angular frontend (SPA)
- ✨ Real-time task updates with SignalR
- ✨ Implement caching (Redis/Memory Cache)
- ✨ CI/CD Pipeline integration
- ✨ Unit and Integration testing
- ✨ Role management UI

---

## 🤝 Contribution

This project is a portfolio showcase and learning ground.  
If you wish to contribute, feel free to fork and raise a PR! 🚀

---

## 📜 License

This project is licensed under the [MIT License](LICENSE).

---

# 📌 Demo Screenshots (Coming Soon)

*(Optional: Add Swagger screenshots or frontend previews later.)*

---

# 🚀 About the Developer

Hi! I'm **Saidul Islam Akib**, a passionate .NET Developer focused on building scalable backend systems and clean software architectures.  
Feel free to connect with me:

- [GitHub](https://github.com/Akib558)
- [LinkedIn](#) (https://www.linkedin.com/in/akib99/)

---