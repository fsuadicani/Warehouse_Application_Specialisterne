# Lageropgave_SpecialisterneMake sure you have set the following with dotnet user-secrets set "key" "value"

# Api Backend ArchitectureWith CoPilot

warehouse-storage-api/
│
├── WarehouseStorage.Api/              # Web API (controllers, startup, auth)
│
├── WarehouseStorage.Domain/           # Entities, aggregates, domain logic
│
├── WarehouseStorage.DTOs/             # Request/response models
│
├── WarehouseStorage.Infrastructure/   # EF Core, DB context, repositories
│
└── WarehouseStorage.Services/         # Business logic, interfaces, mapping

# How to setup Database
Right now it only works with postgres (TODO Make it more agnostic)

Make sure you have set following with dotnet user-secrets set "key" "value"

In API
dotnet user-secrets set "DB_HOST" "Hostname" (Localhost)
dotnet user-secrets set "DB_USERNAME" "Database user name" (postgres)
dotnet user-secrets set "DB_PASSWORD" "Database password" (YourPassword)
dotnet user-secrets set "DB_PORT" "Database port" (5433)
dotnet user-secrets set "DB_NAME" "Database name" (WareHouseStorage)
dotnet user-secrets set "Jwt:Key" "Jwt key" (Get one from: https://jwtsecrets.com/#generator)

In Infrastructure:
Run: dotnet ef migrations add InitialCreate
Run: dotnet ef database update

To remove database:
Run: dotnet ef database drop

If making model changes, consider deleting migrations and recreating unless we want migration history

# Secret configuration
Development-time application secrets are loaded through .NET User Secrets in the API startup, but the active UserSecretsId currently lives in WarehouseStorage.Infrastructure. Program.cs therefore loads user secrets from the infrastructure assembly only in Development so values such as JWT settings can flow into IConfiguration without checking secrets into source control.

DotNetEnv is still in active use in WarehouseStorage.Infrastructure/ConnectionString.cs and is therefore currently the primary source only for the DB_* variables consumed by infrastructure tooling and Npgsql connection string creation. To avoid duplicate or conflicting secret loading, keep application configuration keys such as Jwt:* in User Secrets and reserve .env values for the DB_* variables until database configuration is moved fully into IConfiguration.

# References
1. [UI_design](https://www.canva.com/)
2. [JSON_WEB_TOKEN_CSHARP](https://saigontechnology.com/blog/json-web-token-using-c/)
3. [Create_and_sign_JWT_with_CSHARP](https://docs.hidglobal.com/dev/auth-service/buildingapps/csharp/create-and-sign-a-json-web-token--jwt--with-c--and--net.htm)
4. [Introduction_to_JWT](https://www.jwt.io/introduction#how-json-web-tokens-work)
5. [Implementing_JWT_in_ASP.NET_core_5_mvc](https://www.codemag.com/Article/2105051/Implementing-JWT-Authentication-in-ASP.NET-Core-5)
