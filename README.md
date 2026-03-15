# Organization & Company Management

A full-stack application for managing organizations and companies, built with .NET Clean Architecture and Angular. The backend features a custom lightweight MediatR alternative for CQRS dispatching, uses the Decorator pattern for cross-cutting concerns (validation, unit of work), and the Builder pattern for constructing search queries.

## Tech Stack

### Backend
- .NET 10 Web API (Minimal APIs)
- Entity Framework Core (In-Memory)
- Clean Architecture (Domain, Application, Infrastructure, Web.Api)
- CQRS with Command/Query handlers (custom lightweight MediatR alternative for CQRS dispatching)
- FluentValidation
- Decorator pattern for cross-cutting concerns (validation, unit of work)
- Builder pattern for constructing search queries
- Result pattern for explicit error handling (no exceptions for control flow)
- Domain Events with reflection-based dispatcher
- Scrutor for assembly scanning and DI registration
- Serilog structured logging with Seq sink
- Swagger / OpenAPI documentation
- Architecture Tests (layer dependency verification with NetArchTest)
- Docker multi-stage build

### Frontend
- Angular 16
- Angular Material UI
- Reactive Forms
- Lazy-loaded modules

## Project Structure

```
org-company-management/
├── BackEnd/                 # .NET Clean Architecture API
│   ├── src/
│   │   ├── Domain/          # Entities and domain logic
│   │   ├── Application/     # Commands, queries, handlers
│   │   ├── Infrastructure/  # EF Core, DI, services
│   │   ├── SharedKernel/    # Base classes, Result pattern
│   │   └── Web.Api/         # Endpoints, middleware, startup
│   └── tests/
│       └── ArchitectureTests/
├── FrontEnd/                # Angular 16 SPA
│   └── src/
│       ├── app/
│       │   ├── organization/
│       │   ├── company/
│       │   └── shared/
│       └── environments/
└── docker-compose.yml       # Run entire stack
```

## Getting Started

### Run with Docker (recommended)

```bash
docker compose up --build
```

| Service    | URL                    |
|------------|------------------------|
| Frontend   | http://localhost:4200   |
| Backend API| http://localhost:5000   |
| Seq Logs   | http://localhost:8081   |

### Run Locally

**Backend:**
```bash
cd BackEnd/src/Web.Api
dotnet run
```

**Frontend:**
```bash
cd FrontEnd
npm install
ng serve
```

The frontend proxies API calls to `http://localhost:5000` via `proxy.conf.json`.

## Features

- **Organizations**: Create, update, search, delete with country autocomplete
- **Companies**: Full CRUD linked to organizations with validation
- **Search**: Filtered search with pagination and sorting
- **Responsive UI**: Mobile-friendly Angular Material design
- **Seed Data**: Pre-loaded with sample organizations and companies
