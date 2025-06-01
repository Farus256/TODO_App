# TodoApi

A simple RESTful API to manage a Todo list, built with ASP.NET Core, Entity Framework Core, and MySQL.

## Feedback about creating with ChatGpt
- Was it easy to complete the task using AI?

  - Yes, the ChatGpt provided helpful and structured guidance throughout the process. It simplified the whole process of creating an app.

- How long did task take you to complete? (Please be honest, we need it to gather anonymized statistics)

  - Approximately 2 hours, including debugging and verifying database setup.

- Was the code ready to run after generation? What did you have to change to make it usable?

  - The generated code was mostly correct, but I had to make some work related to database set-up. Also, I had to modify project template and build settings.

- Which challenges did you face during completion of the task?

  - Migrations. Incorrectly created project template. Issues with connection to database

- Which specific prompts you learned as a good practice to complete the task?

  - Ask for full test class code with context setup.

  - Specify your prompt like step by step tutorial.

  - Giving chatgpt full log about errors.

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



