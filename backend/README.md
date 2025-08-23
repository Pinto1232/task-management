# Task Management Backend

A .NET 8 Web API for task management built with Clean Architecture principles.

## Project Structure

- **TaskManagement.Api** - Web API layer with controllers and endpoints
- **TaskManagement.Application** - Business logic and application services
- **TaskManagement.Domain** - Domain entities and core business rules
- **TaskManagement.Infrastructure** - Data access and external dependencies
- **TaskManagement.Tests** - Unit and integration tests

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## How to Run

### Option 1: From Backend Directory
```bash
cd backend
dotnet run --project TaskManagement.Api
```

### Option 2: From API Project Directory
```bash
cd backend/TaskManagement.Api
dotnet run
```

### Option 3: From Root Directory
```bash
dotnet run --project backend/TaskManagement.Api
```

The API will be available at: `http://localhost:5174`

## Development Commands

### Build the solution
```bash
cd backend
dotnet build
```

### Run tests
```bash
cd backend
dotnet test
```

### Restore packages
```bash
cd backend
dotnet restore
```

## API Endpoints

The API provides endpoints for task management. You can test the endpoints using:
- The included `.http` file in the API project
- Postman or similar API testing tools
- Swagger UI (available when running in Development mode)