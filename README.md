# Jewel Architecture
Jewel Architecture is a fusion of DDD, CQRS, and Clean Architecture—a pattern that leverages their individual strengths to create scalable, maintainable, and valuable systems.
This repository contains examples structured using the Jewel Architecture. It contains an example of a simplified Smart Charging solution. It is an ASP.NET Core Web API project built with .NET 8. The API is configured with Swagger for API documentation and uses an in-memory database, so no external dependencies are required.

## Smart Charging Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine

## Getting Started

### 1. Clone the repository

Clone this repository to your local machine:

```bash
git clone https://github.com/emanuelebuldrini/jewel-architecture.git
cd Examples
cd SmartCharging
```
### 2. Build the Solution
Use the .NET CLI to restore and build the solution:
```bash
dotnet build
```
### 3. Run the Application
Run the application using the .NET CLI.
```bash
dotnet run --project ".\JewelArchitecture.Examples.SmartCharging.WebApi\JewelArchitecture.Examples.SmartCharging.WebApi.csproj"
```
### 4. Access the Application
Once running, you can access the Swagger API documentation at:

HTTP: http://localhost:5253/swagger

### 5. Test the Application
To run tests for the application, use:
```bash
dotnet test
```
### 6. Project Structure
This project follows a clean architecture, with the following key layers:

- Application Layer: Contains use cases and business logic.
- Domain Layer (Core): Defines entities and aggregates, using an in-memory repository implementation for simplicity.
- Infrastructure Layer: Optional infrastructure components (in-memory database used here).
- Presentation Layer: ASP.NET Core Web API exposing application endpoints.
### 7. Customization
If you need to add persistent storage or external dependencies in the future, consider replacing the in-memory repository with a real database implementation, adding external services, or configuring additional infrastructure components.

