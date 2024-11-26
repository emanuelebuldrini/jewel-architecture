# Jewel Architecture
Jewel Architecture is a fusion of DDD, CQRS, and Clean Architecture—a pattern that leverages their individual strengths to create scalable, maintainable, and valuable systems.
I call it Jewel Architecture because it organizes your system into multifaceted, interchangeable components, creating a highly valuable and mantainable structure—like a finely cut gem.
This repository contains solutions structured using the Jewel Architecture, like for example a simplified Smart Charging domain.
## Foundation Principles
**1. The Domain is Central:** It is self-contained and free from dependencies.

**2. The Application Layer Supports the Domain:** Its role is to provide the domain with the necessary context and data for business decisions.

**3. Commands Drive Change:** Any action that modifies the system's state is encapsulated in a Command.

**4. Queries Provide Insight:** Reading and retrieving data from the system is handled through Queries.

**5. Cross-Cutting Concerns are Extensible:** Decorators are a clean and flexible way to manage these concerns.

**6. Focused Interaction:** The application layer exposes Use Cases or Services, offering clear entry points for the interface layer.

## Smart Charging Example Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine
  
It is an ASP.NET Core Web API project built with .NET 8. The API is configured with Swagger for API documentation and uses an in-memory database, so no external dependencies are required.

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
### 6. Solution Structure
This solution uses an aggregate-based folder structure and follows a clean architecture, with the following key layers:

- Application Layer: Contains use cases, services and business logic.
- Domain Layer: Defines entities, aggregates, value objects, invariants and domain logic.
- Infrastructure Layer: Optional infrastructure components (in-memory database used here).
- Interface Layer: ASP.NET Core Web API exposing application endpoints.
### 7. Customization
If you need to implement cross-cutting concerns like logging, auditing, exceptions handling: Decorators for Commands, Queries or Use Cases are a clean and flexible way to manage these concerns.

If you need to go full async with CQRS: It is enough to write Commands and Queries in a queue and then move the handlers in a separate background process or dedicated worker that processes them asynchronously.

Also, if you need to manage Domain Events in a loosely coupled way: You can publish them in a message bus and let the subscribers handle them.

If you need to add persistent storage or external dependencies in the future, consider replacing the in-memory repository with another database implementation, adding external services, or configuring additional infrastructure components.

