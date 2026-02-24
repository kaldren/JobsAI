# Candidates Module – EF Core Migrations

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- EF Core CLI tools:
  ```sh
  dotnet tool install --global dotnet-ef
  ```
- `Microsoft.EntityFrameworkCore.Design` package referenced in the startup project (`JobsAI.API`).

## Database Details

| Setting      | Value                        |
|--------------|------------------------------|
| DbContext    | `CandidatesDbContext`        |
| Schema       | `candidates`                 |
| Connection   | `JobsAIDb` (in `appsettings.json`) |

All commands below should be run **from the solution root** (`JobsAI/`).

## Add a New Migration

```sh
dotnet ef migrations add <MigrationName> \
  --project src/JobsAI.Modules.Candidates \
  --startup-project src/JobsAI.API \
  --context CandidatesDbContext \
  --output-dir Infrastructure/Data/Migrations
```

## Apply Migrations to the Database

```sh
dotnet ef database update \
  --project src/JobsAI.Modules.Candidates \
  --startup-project src/JobsAI.API \
  --context CandidatesDbContext
```

## Remove the Last Migration (if not yet applied)

```sh
dotnet ef migrations remove \
  --project src/JobsAI.Modules.Candidates \
  --startup-project src/JobsAI.API \
  --context CandidatesDbContext
```

## Generate a SQL Script (for production deployments)

```sh
dotnet ef migrations script \
  --project src/JobsAI.Modules.Candidates \
  --startup-project src/JobsAI.API \
  --context CandidatesDbContext \
  --idempotent \
  --output migrations.sql
```

## Key Flags

| Flag                | Value                            | Purpose                                      |
|---------------------|----------------------------------|----------------------------------------------|
| `--project`         | `src/JobsAI.Modules.Candidates`  | Project containing `CandidatesDbContext`      |
| `--startup-project` | `src/JobsAI.API`                 | Runnable host with the connection string      |
| `--context`         | `CandidatesDbContext`            | Disambiguates when multiple DbContexts exist |
| `--output-dir`      | `Infrastructure/Data/Migrations` | Keeps migrations co-located with the DbContext|
