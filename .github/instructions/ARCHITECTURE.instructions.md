# Architecture
JobsAI is built using modular monolith architecture with Domain-Driven Design (DDD) principles. The project is organized into modules that encapsulate specific business domains, and each module follows the clean architecture principles. This approach allows for better separation of concerns, maintainability, and scalability of the codebase.

# Code Structure
The code is following the Clean Architecture, Vertical Architecture, and Domain-Driven Design (DDD) principles. Each module has four core folders:
- `Domain`: Contains the core business logic, aggregates, entities, value objects, exceptions, and events.
- `Application`: Contains the use cases implemented as features (using vertical slice architecture), interfaces, and DTOs.
- `Infrastructure`: Contains the implementation of interfaces defined in the application layer, such as repositories, external services, and database access.
- `Api`: Contains the controllers, routes, and any API-related code.