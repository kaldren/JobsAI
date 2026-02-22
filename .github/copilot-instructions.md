# About The Project
JobsAI is a job platform that uses AI to match job seekers with relevant job opportunities. Unlike traditional job platforms, like LinkedIn, that transformed into social networks, JobsAI does not allow users to post content. Also, unlike LinkedIn, users are not allowed to candidate for jobs. Instead, JobsAI uses AI to match job seekers with relevant job opportunities based on their skills, experience, and preferences. This approach allows JobsAI to provide a more personalized and efficient job search experience for both job seekers and employers. This way, employers don't get flooded with irrelevant applications, and job seekers get matched with jobs that are a better fit for their skills and experience.

Once AI finds a match, the job seeker receives a notification and can choose to apply for the job. If the job seeker decides to apply, they can submit their application through the platform, and the employer will receive it for review. This process allows for a more streamlined and efficient job search experience for both parties.

# Architecture
JobsAI is built using modular monolith architecture with Domain-Driven Design (DDD) principles. The project is organized into modules that encapsulate specific business domains, and each module follows the clean architecture principles. This approach allows for better separation of concerns, maintainability, and scalability of the codebase.

# Code Structure
The code is following the Clean Architecture, Vertical Architecture, and Domain-Driven Design (DDD) principles. Each module has four core folders:
- `Domain`: Contains the core business logic, aggregates, entities, value objects, exceptions, and events.
- `Application`: Contains the use cases implemented as features (using vertical slice architecture), interfaces, and DTOs.
- `Infrastructure`: Contains the implementation of interfaces defined in the application layer, such as repositories, external services, and database access.
- `Api`: Contains the controllers, routes, and any API-related code.