# CQRSPattern

## Introduction

This project, CQRSPattern, is a demonstration of the Command Query Responsibility Segregation (CQRS) pattern in a .NET application. It uses MediatR for handling commands and queries, FluentValidation for validating requests, and Entity Framework for data access. It now also includes user registration, sign in, and JWT token authentication.

## Getting Started

### Prerequisites

- .NET 5.0 or later
- A SQL Server database

### Installation

1. Clone the repository: `git clone https://github.com/YogeshHadiya33/CQRSPattern.git`
2. Navigate to the project directory: `cd CQRSPattern`
3. Restore the NuGet packages: `dotnet restore`
4. Set up the connection string in your user secrets. User secrets provide a secure way to store sensitive data during the development process, without the risk of accidentally committing the sensitive data into source control: `dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YourConnectionString"`
5. Set up the JWT settings in your user secrets: `dotnet user-secrets set "jwt-key" "YourJwtKey"`, `dotnet user-secrets set "jwt-issuer" "YourJwtIssuer"`, `dotnet user-secrets set "jwt-audience" "YourJwtAudience"`
6. Apply the Entity Framework migrations to create the database schema: `dotnet ef database update --project CQRSPattern.Repository --startup-project CQRSPattern.API`
7. Run the project: `dotnet run`

## Project Structure

- `CQRSPattern.API`: This is the ASP.NET Core Web API project. It contains the controllers and the startup configuration.
- `CQRSPattern.Services`: This project contains the business logic, including the command handlers, validators, and user authentication services.
- `CQRSPattern.Entity`: This project contains the Entity Framework data context, entity classes, and user identity models.
- `CQRSPattern.Common`: This project contains common classes and interfaces used across the solution.

## Key Features

- **Employee Management**: The application provides endpoints for creating, updating, retrieving, and deleting employees. These endpoints require JWT token authentication.
- **User Registration and Authentication**: The application now supports user registration and sign in. It uses JWT tokens for authentication. These endpoints do not require JWT token authentication.
- **Validation**: Requests are validated using FluentValidation. If a request is invalid, the API returns a detailed validation error response.
- **CQRS**: The application uses the CQRS pattern, separating read and write operations to improve performance and scalability.

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
