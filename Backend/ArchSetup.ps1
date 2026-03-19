# Create the solution
dotnet new sln -n warehouse-storage-api

# Create the Web API project
dotnet new webapi -n WarehouseStorage.Api

# Create class library projects
dotnet new classlib -n WarehouseStorage.Domain
dotnet new classlib -n WarehouseStorage.DTOs
dotnet new classlib -n WarehouseStorage.Infrastructure
dotnet new classlib -n WarehouseStorage.Services

# Create test project (xUnit)
dotnet new xunit -n WarehouseStorage.Tests

# Add projects to the solution
dotnet sln add WarehouseStorage.Api/WarehouseStorage.Api.csproj
dotnet sln add WarehouseStorage.Domain/WarehouseStorage.Domain.csproj
dotnet sln add WarehouseStorage.DTOs/WarehouseStorage.DTOs.csproj
dotnet sln add WarehouseStorage.Infrastructure/WarehouseStorage.Infrastructure.csproj
dotnet sln add WarehouseStorage.Services/WarehouseStorage.Services.csproj
dotnet sln add WarehouseStorage.Tests/WarehouseStorage.Tests.csproj

# Add project references
# API depends on Services + DTOs
dotnet add WarehouseStorage.Api reference WarehouseStorage.Services
dotnet add WarehouseStorage.Api reference WarehouseStorage.DTOs

# Services depend on Domain + Infrastructure + DTOs
dotnet add WarehouseStorage.Services reference WarehouseStorage.Domain
dotnet add WarehouseStorage.Services reference WarehouseStorage.Infrastructure
dotnet add WarehouseStorage.Services reference WarehouseStorage.DTOs

# Infrastructure depends on Domain
dotnet add WarehouseStorage.Infrastructure reference WarehouseStorage.Domain

# Tests depend on Services, Domain, DTOs, and API (optional)
dotnet add WarehouseStorage.Tests reference WarehouseStorage.Services
dotnet add WarehouseStorage.Tests reference WarehouseStorage.Domain
dotnet add WarehouseStorage.Tests reference WarehouseStorage.DTOs
dotnet add WarehouseStorage.Tests reference WarehouseStorage.Api

# Add common NuGet packages
# Entity Framework Core + SQL Server
dotnet add WarehouseStorage.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add WarehouseStorage.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
dotnet add WarehouseStorage.Infrastructure package Microsoft.EntityFrameworkCore.Design
dotnet add WarehouseStorage.Infrastructure package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add WarehouseStorage.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add WarehouseStorage.Infrastructure package DotNetEnv
dotnet add WarehouseStorage.Api package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add WarehouseStorage.Domain package Microsoft.AspNetCore.Identity.EntityFrameworkCore

# Add Configuration to Infrastructure
dotnet add WarehouseStorage.Infrastructure package Microsoft.Extensions.Configuration
dotnet add WarehouseStorage.Infrastructure package Microsoft.Extensions.Configuration.Json
dotnet add WarehouseStorage.Infrastructure package Microsoft.Extensions.Configuration.FileExtensions

# Authentication & Authorization (JWT)
dotnet add WarehouseStorage.Api package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add WarehouseStorage.Domain package Microsoft.AspNet.Identity.EntityFramework
dotnet add WarehouseStorage.Services package Bcrypt.Net-Next

# FluentValidation (optional but recommended)
dotnet add WarehouseStorage.Services package FluentValidation
dotnet add WarehouseStorage.Api package FluentValidation.AspNetCore

# Test project packages
dotnet add WarehouseStorage.Tests package Moq
dotnet add WarehouseStorage.Tests package FluentAssertions
dotnet add WarehouseStorage.Tests package Microsoft.AspNetCore.Mvc.Testing