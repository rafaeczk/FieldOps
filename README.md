# FieldOps API

Modular monolith backend for the FieldOps field service platform. Built with .NET 10, CQRS and a domain-driven modular architecture.

## Tech Stack

- **Runtime:** .NET 10 (C# 13)
- **Architecture:** Modular Monolith with Clean Architecture
- **Database:** PostgreSQL 16 via Entity Framework Core
- **Messaging:** MediatR (CQRS and pipeline behaviors)
- **Validation:** FluentValidation
- **Auth:** JWT Bearer (HMAC-SHA256)
- **Storage:** S3-compatible object storage (Backblaze B2)
- **API Docs:** Swashbuckle (Swagger/OpenAPI)

## Project Structure

```
FieldOpsApi/
  FieldOps.Bootstrapper/          # Application entry point
  FieldOps.Shared.Abstractions/   # Shared kernel (value objects, events, interfaces)
  FieldOps.Shared.Infrastructure/ # Auth, DB, S3, MediatR pipeline, Swagger
  FieldOps.Modules.Accounts/      # User/account management, authentication
  FieldOps.Modules.Assets/        # Field asset/equipment management
  FieldOps.Modules.Files/         # S3-backed file uploads
  FieldOps.Modules.Jobs/          # Job CRUD, assignment, status tracking
  FieldOps.Modules.Operators/     # Operator entity management
  FieldOps.Modules.Reports/       # Report creation and management
  FieldOps.Modules.Technicians/   # Technician entity management
```

Modules communicate through shared Contracts projects. No direct cross-module Core references.

### Module Layering

Simple modules (Accounts, Assets, Files, Operators, Technicians):
```
Module.Api        # Controllers, DI registration
Module.Core       # Entities, DbContext, Repositories, Services, Validators
Module.Contracts  # DTOs, events, interfaces
```

Complex modules (Jobs, Reports):
```
Module.Api             # Controllers, DI registration
Module.Application     # Commands/Queries, handlers, validators
Module.Domain          # Entities, value objects, domain events
Module.Infrastructure  # DbContext, EF configs, migrations
Module.Contracts       # Shared DTOs, events, interfaces
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/) (10.0.111)
- [Docker](https://www.docker.com/) and Docker Compose
- OpenSSL (for JWT key generation)

### Quick Start

```bash
# Clone the repo
git clone https://github.com/rafaeczk/FieldOps.git FieldOpsApi
cd FieldOpsApi

# Generate a JWT signing key
openssl rand -base64 64

# Create .env from example and fill in values
cp .env.example .env

# Start PostgreSQL and the API
docker compose up --build
```

The API starts on `http://localhost:8080`. Swagger UI is available at `/swagger` in development mode.

### Development (without Docker)

```bash
# Start only the database
docker compose up db -d

# Run the API
cd FieldOps.Bootstrapper
dotnet run
```

### Environment Variables

| Variable | Description | Default |
|---|---|---|
| `POSTGRES_DB` | Database name | `FieldOpsDb` |
| `POSTGRES_USER` | Database user | `postgres` |
| `POSTGRES_PASSWORD` | Database password | `postgres` |
| `JWT_SIGNING_KEY` | HMAC key for JWT signing | Required |
| `AUTH_ISSUER` | JWT issuer claim | `FieldOps` |
| `AUTH_VALID_ISSUER` | JWT valid issuer claim | `FieldOps` |
| `S3_SERVICE_URL` | S3-compatible endpoint | Required |
| `S3_ACCESS_KEY` | S3 access key ID | Required |
| `S3_SECRET_KEY` | S3 secret access key | Required |
| `S3_BUCKET_NAME` | S3 bucket name | Required |
| `ASPNETCORE_ENVIRONMENT` | Environment name | `Production` |

## Database

PostgreSQL with a schema-per-module pattern. All modules share one database instance but use separate schemas. Migrations run automatically on startup.

Default development credentials: `postgres:ciucia@127.0.0.1:5432/FieldOpsDb`

## Authentication

JWT Bearer tokens with role-based access control.

**Roles:**
| Role | Access |
|---|---|
| `ADMIN` | Full access, user management |
| `OPERATOR` | Job and report management |
| `TECHNICIAN` | View assigned jobs, submit reports |

Default admin account: `admin@fieldops.com` / `123`

## Testing

```bash
dotnet test
```

Test projects use xUnit, Moq and coverlet for coverage. Five test suites cover Accounts, Jobs, Operators, Reports and Technicians modules.

## CI

### GitLab CI (`.gitlab-ci.yml`)
Three parallel jobs on `mcr.microsoft.com/dotnet/sdk:10.0`:
1. `dotnet format --verify-no-changes`
2. `dotnet build --configuration Release`
3. `dotnet test --configuration Release`

### GitHub Actions (`.github/workflows/ci.yml`)
Runs on push/PR to main (sequential steps in one job):
1. `dotnet restore`
2. `dotnet format --verify-no-changes`
3. `dotnet build --configuration Release`
4. `dotnet test --configuration Release`

## License

MIT
