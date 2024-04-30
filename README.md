# CQRSPattern

## Introduction

This project, CQRSPattern, is a demonstration of the Command Query Responsibility Segregation (CQRS) pattern in a .NET application. It uses MediatR for handling commands and queries, FluentValidation for validating requests, and Entity Framework for data access. The application also includes user registration, sign in, JWT token authentication, and caching with both in-memory and Azure Cache for Redis.

## Getting Started

### Prerequisites

- .NET 5.0 or later
- A SQL Server database
- (Optional) Azure Cache for Redis


### Installation

1. Clone the repository: `git clone https://github.com/YogeshHadiya33/CQRSPattern.git`
2. Navigate to the project directory: `cd CQRSPattern`
3. Restore the NuGet packages: `dotnet restore`
4. Set up the connection string in your user secrets: `dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YourConnectionString"`
5. Set up the JWT settings in your user secrets: `dotnet user-secrets set "jwt-key" "YourJwtKey"`, `dotnet user-secrets set "jwt-issuer" "YourJwtIssuer"`, `dotnet user-secrets set "jwt-audience" "YourJwtAudience"`
6. (Optional) If you want to use Azure Cache for Redis, set up the Redis connection string in your user secrets: `dotnet user-secrets set "ConnectionStrings:Redis" "YourRedisConnectionString"`
7. Apply the Entity Framework migrations to create the database schema: `dotnet ef database update --project CQRSPattern.Repository --startup-project CQRSPattern.API`
8. Run the project: `dotnet run`


### Key Features

- **Employee Management**: The application provides endpoints for creating, updating, retrieving, and deleting employees. These endpoints require JWT token authentication and use caching to improve performance.
- **User Registration and Authentication**: The application supports user registration and sign in. It uses JWT tokens for authentication. These endpoints do not require JWT token authentication.
- **Validation**: Requests are validated using FluentValidation. If a request is invalid, the API returns a detailed validation error response.
- **CQRS**: The application uses the CQRS pattern, separating read and write operations to improve performance and scalability.
- **Caching**: The application uses both in-memory caching and Azure Cache for Redis to reduce the load on the database and improve performance. By default, the application uses in-memory cache for employee-related operations. There are two interfaces for caching:
  - `IInMemoryRedisCacheService`: Use this to store cache in memory. This is useful when you want to cache data that is frequently accessed and changes infrequently. This is the default caching method used in the application.
  - `IRedisCacheService`: Use this to store cache in Redis. This is useful when you want to cache data that is large or needs to be shared across multiple instances of the application. To use Redis cache, you need to specify the Redis connection string in the secret key with the name `ConnectionStrings:Redis`.


## API Endpoints

### Employee Endpoints (Requires JWT token authentication)

- `GET /api/employee`: Get all employees
- `GET /api/employee/{id}`: Get a specific employee by ID
- `POST /api/employee`: Create a new employee
- `PUT /api/employee`: Update an existing employee
- `DELETE /api/employee/{id}`: Delete an employee

### User Endpoints (Does not require JWT token authentication)

- `POST /api/user/register`: Register a new user
- `POST /api/user/login`: Sign in a user and get a JWT token
- `GET /api/user`: Get all users
- `GET /api/user/{id}`: Get a specific user by ID

## Entity Framework Migrations

This project uses Entity Framework Core Migrations for managing database schema changes. The migrations are located in the `CQRSPattern.Repository` project.

To apply the existing migrations and update the database schema, use the following command in the .NET CLI:

`dotnet ef database update --project CQRSPattern.Repository --startup-project CQRSPattern.API`

This command tells Entity Framework to apply the migrations from the `CQRSPattern.Repository` project to the database specified in the `CQRSPattern.API` project's configuration.
