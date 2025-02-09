# TaskFlow API

TaskFlow API is a task management system designed with a focus on **middleware, authentication, role-based access control (RBAC), Fluent Validation, Entity Framework**, and **clean API error handling**. This project follows a structured architecture to ensure maintainability and scalability.

## Features

- **User Authentication & Authorization**

  - JWT-based authentication
  - Role-Based Access Control (RBAC)
  - Role assignment to users with predefined privileges

- **Task Management**

  - Create, update, delete, and assign tasks
  - Task prioritization and deadlines
  - Subtasks support

- **Middleware Implementation**

  - Custom exception handling
  - Request logging
  - Authentication middleware

- **Validation**

  - Fluent Validation for request validation
  - Model validation filter using `IActionFilter`

- **Entity Framework & Database Integration**

  - Code-first migrations
  - Repository & Service pattern
  - MSSQL database running in Docker

## Project Structure

```
TaskFlow-API/
│── TaskManagementAPI/         # Main API project
│── TaskManagement.Core/       # Core definitions (Models, DTOs, Enums)
│── TaskManagement.Data/       # Database context and migrations
│── TaskManagement.Repositories/ # Data access layer
│── TaskManagement.Services/   # Business logic layer
│── TaskManagement.Middlewares/ # Custom middleware
│── TaskManagement.Helpers/    # Utility and helper classes
```

## Prerequisites

- **.NET 7+**
- **Docker** (for MSSQL database)
- **Entity Framework Core**

## Installation & Setup

1. **Clone the repository:**

   ```sh
   git clone https://github.com/your-repo/taskflow-api.git
   cd taskflow-api
   ```

2. **Set up the database in Docker:**

   ```sh
   docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourPassword123' \
   -p 4001:1433 --name taskflow-mssql -d mcr.microsoft.com/mssql/server:2019-latest
   ```

3. **Apply database migrations:**

   ```sh
   dotnet ef database update
   ```

4. **Run the application:**

   ```sh
   dotnet run --project TaskManagementAPI
   ```

5. **Access the API documentation:**

   - Swagger UI: `http://localhost:5000/swagger`

## API Endpoints

| Method | Endpoint           | Description       |
| ------ | ------------------ | ----------------- |
| POST   | /api/auth/login    | User login        |
| POST   | /api/auth/register | User registration |
| GET    | /api/tasks         | Get all tasks     |
| POST   | /api/tasks         | Create a new task |
| PUT    | /api/tasks/{id}    | Update a task     |
| DELETE | /api/tasks/{id}    | Delete a task     |

## Future Enhancements

- Implementing **CI/CD pipelines**
- Adding **frontend** (Angular 18/19, Jira-like UI)
- Enhancing **caching** for performance optimization

