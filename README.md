# TodoApi

A simple RESTful API to manage a Todo list, built with ASP.NET Core, Entity Framework Core, and MySQL.

---

## Features

- Create, read, update, and delete (CRUD) Todo items.
- Each `TodoItem` has:
  - `Id` (int, auto-increment)
  - `Title` (string, required, max length 100)
  - `Description` (string, optional, max length 500)
  - `IsCompleted` (bool)
- Swagger UI for interactive API documentation.
- Unit tests with xUnit, using EF Core InMemory provider.
- Code style and complexity checks via Roslyn analyzers and StyleCop.
- Coverage target: **> 80%**.

---

## Prerequisites

- .NET 7 SDK (or later)
- MySQL Server (v8.0+)
- MySQL user with permissions to create databases/tables (e.g., `todo_user`)
- Git (optional)

---

## Setup

1. **Clone the repository**  
   ```bash
   git clone https://your‐git‐server/todoapi.git
   cd todoapi
