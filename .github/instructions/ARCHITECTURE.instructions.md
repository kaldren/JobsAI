# Architecture

JobsAI is built using a **Modular Monolith** architecture with Domain-Driven Design (DDD) principles. The project is organized into modules that encapsulate specific business domains, and each module follows Clean Architecture principles. This approach allows for better separation of concerns, maintainability, and scalability of the codebase.

## Host Projects

| Project | Role |
|---|---|
| `JobsAI.API` | ASP.NET Core Web API host. Registers modules, configures authentication (Microsoft Identity / JWT Bearer), and exposes HTTP endpoints. |
| `JobsAI.Web` | Blazor WebAssembly front-end. Uses MSAL for authentication. |

## Module Projects

Each module lives in its own class library project following the naming convention `JobsAI.Modules.<ModuleName>`:

- `JobsAI.Modules.Candidates` – Candidate profiles, CVs, and related workflows.
- `JobsAI.Modules.Companies` – Company profiles and job postings.
- `JobsAI.Modules.Matching` – AI-driven matching between candidates and jobs.

# Code Structure

The code follows Clean Architecture, Vertical Slice Architecture, CQRS, and Domain-Driven Design (DDD) principles. Each module has four core folders with dependencies pointing inward — outer layers depend on inner layers, never the reverse.

```
JobsAI.Modules.<ModuleName>/
├── <ModuleName>Module.cs # Module registration (DI extension method)
├── Api/                  # Public surface of the module (endpoints, contracts)
├── Application/          # Use cases — orchestrates domain logic
│   ├── Abstractions/     # Interfaces (e.g., repository contracts)
│   ├── Dtos/             # Data Transfer Objects for input/output
│   └── Features/         # Vertical-slice feature folders (one folder per use case)
│       └── <FeatureName>/
├── Domain/               # Core business logic — no external dependencies
│   ├── Aggregates/       # Aggregate roots
│   ├── Entities/         # Entities owned by aggregates
│   ├── ValueObjects/     # Immutable value objects
│   ├── Events/           # Domain events
│   └── Exceptions/       # Domain-specific exceptions
└── Infrastructure/       # Technical concerns (persistence, external services)
    └── Data/             # EF Core DbContext, repositories
        └── Configurations/ # EF Core entity type configurations and constants
```

- `<ModuleName>Module.cs`: A `public static` class at the project root that exposes an `Add<ModuleName>Module` extension method on `IServiceCollection`. Registers the module's `DbContext`, repositories, and other services. Called from the API host's `Program.cs`.
- `Domain`: Contains the core business logic, aggregates, entities, value objects, exceptions, and events.
- `Application`: Contains the use cases implemented as features (using vertical slice architecture), interfaces (in `Abstractions/`), and DTOs.
- `Infrastructure`: Contains the implementation of interfaces defined in the Application layer, such as repositories, external services, and database access.
- `Api`: Contains the endpoints, routes, and any API-related code.

## DDD (Domain-Driven Design) Rules

- **Aggregates** are the consistency boundaries. Each aggregate root lives in `Domain/Aggregates/` and controls access to its child entities.
- **Entities** live in `Domain/Entities/` and are always accessed through their owning aggregate.
- **Value Objects** live in `Domain/ValueObjects/` and are modeled as C# `record` types with `init` properties and constructor validation.
- **Domain Events** live in `Domain/Events/` and represent things that happened in the domain.
- **Domain Exceptions** live in `Domain/Exceptions/` and represent domain rule violations.

## Clean Architecture Rules

- The **Domain** layer has zero dependencies on other layers or external packages.
- The **Application** layer depends only on the Domain layer and defines use-case orchestration.
- The **Infrastructure** layer implements interfaces defined in the Application or Domain layers (e.g., repositories).
- The **Api** layer is the entry point into the module and depends on the Application layer.

## Database Strategy

All modules share a **single SQL Server database** (`JobsAIDb` connection string) but are isolated via **per-module schemas**:

| Module | Schema |
|---|---|
| `Candidates` | `candidates` |

- Each module owns its own `DbContext` (e.g., `CandidatesDbContext`) that calls `modelBuilder.HasDefaultSchema(...)` in `OnModelCreating`.
- The schema name is defined as a constant in the module's `Infrastructure/Data/Configurations/ConfigurationConstants` class.
- Entity type configurations live in `Infrastructure/Data/Configurations/` and reference table/schema constants from `ConfigurationConstants`.
- Repository interfaces are defined in `Application/Abstractions/` and implemented in `Infrastructure/Data/`.

## Vertical Slice & CQRS Rules

- Each use case (command or query) gets its own folder under `Application/Features/<FeatureName>/`.
- Implement Commands and Queries inside the `Features` folder of the Application layer.
- Each Command or Query should have a nested (not a separate class!) `Handler` class that implements the logic for handling that specific Command or Query.
- **LiteBus** is used as the in-process message bus for dispatching commands and queries.

## Coding Conventions

### Access Modifiers

- Module-internal types (aggregates, entities, handlers, API classes) are marked `internal` to enforce module encapsulation. Only types that must be shared across modules are `public`.

### Domain Modeling

- **Aggregate roots**: regular `class` with `private set` properties and a constructor that enforces invariants. Use `Guid` for identifiers.
- **Value objects**: `record` type with `init` properties and constructor validation.
- **Entities**: regular `class` with properties appropriate to the entity.

### Namespaces

Namespaces mirror the folder structure:

```
JobsAI.Modules.<ModuleName>.<Layer>.<Subfolder>
```

Examples:
- `JobsAI.Modules.Candidates.Domain.Aggregates`
- `JobsAI.Modules.Candidates.Domain.ValueObjects`
- `JobsAI.Modules.Candidates.Application.Features.CreateCV`
- `JobsAI.Modules.Candidates.Application.Abstractions`
- `JobsAI.Modules.Candidates.Infrastructure.Data`
- `JobsAI.Modules.Candidates.Infrastructure.Data.Configurations`
- `JobsAI.Modules.Candidates.Api`

### General

- Target: **.NET 10**, C# 14.
- `<Nullable>enable</Nullable>` and `<ImplicitUsings>enable</ImplicitUsings>` are enabled in all projects.
- Use file-scoped namespaces.
- Keep `using` directives at the top of the file.

## Adding a New Feature

1. **Create a feature folder** under `Application/Features/<FeatureName>/`.
2. **Define the command or query** class with a nested `Handler` class (implement the appropriate LiteBus interface).
3. **Add any new domain types** (aggregates, entities, value objects, events, exceptions) in the corresponding `Domain/` subfolder.
4. **Add infrastructure implementations** (repository, persistence config) in `Infrastructure/`.
5. **Expose the endpoint** in the `Api/` layer.

## Adding a New Module

1. Create a new class library project: `JobsAI.Modules.<ModuleName>`.
2. Target `net10.0` with nullable and implicit usings enabled.
3. Create the four top-level folders: `Api/`, `Application/`, `Domain/`, `Infrastructure/`.
4. Add a `<ModuleName>Module.cs` at the project root with a `public static` `Add<ModuleName>Module` extension method that registers the module's `DbContext` (using the shared `JobsAIDb` connection string), repositories, and services.
5. In the module's `DbContext`, call `modelBuilder.HasDefaultSchema("<module_schema>")` to isolate its tables.
6. Add a project reference from `JobsAI.API` and call the registration extension in `Program.cs`.
