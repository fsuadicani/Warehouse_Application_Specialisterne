# Lageropgave_Specialisterne


# Api Backend Archtecture
With CoPilot

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
In Infrastructure:
Run: dotnet ef migrations add InitialCreate
Run: dotnet ef database update

To remove database:
Run: dotnet ef database drop

If making model changes, consider deleting migrations and recreating unless we want migration history

# References
1. [UI_design](https://www.canva.com/)
2. [JSON_WEB_TOKEN_CSHARP](https://saigontechnology.com/blog/json-web-token-using-c/)
3. [Create_and_sign_JWT_with_CSHARP](https://docs.hidglobal.com/dev/auth-service/buildingapps/csharp/create-and-sign-a-json-web-token--jwt--with-c--and--net.htm)
4. [Introduction_to_JWT](https://www.jwt.io/introduction#how-json-web-tokens-work)
5. [Implementing_JWT_in_ASP.NET_core_5_mvc](https://www.codemag.com/Article/2105051/Implementing-JWT-Authentication-in-ASP.NET-Core-5)
