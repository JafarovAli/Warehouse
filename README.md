# Warehouse Security Module

## Overview
The **Warehouse** project is built using **.NET 6** and follows a **Microservices** architecture. The security module of this project is responsible for handling authentication, authorization, and secure communication between microservices.

## Technologies Used
- **.NET 6**
- **Microservices Architecture**
- **RabbitMQ** (for message brokering)
- **MinIO** (for object storage)
- **Redis** (for caching and session management)
- **Docker** (for containerization)

## Features of the Security Module
- **User Authentication & Authorization**
  - JWT-based authentication.
  - Role-based access control.
  - Secure password hashing.
- **Secure Inter-Service Communication**
  - Token validation for microservices.
  - Secure API Gateway integration.
- **Session Management & Caching**
  - Redis used for caching and session persistence.
- **Event-Driven Architecture**
  - RabbitMQ for asynchronous event handling.
- **Secure Storage**
  - MinIO for encrypted object storage.

## Installation & Setup
1. **Clone the repository:**
   ```sh
   git clone <repository_url>
   ```
2. **Navigate to the security module directory:**
   ```sh
   cd Warehouse/Security
   ```
3. **Configure Environment Variables:**
   - Update `.env` or `appsettings.json` for database connections, RabbitMQ, MinIO, and Redis settings.
4. **Run with Docker:**
   ```sh
   docker-compose up --build
   ```
5. **Run Locally:**
   - Ensure all required services (RabbitMQ, MinIO, Redis) are running.
   - Start the application using .NET CLI:
     ```sh
     dotnet run
     ```

## API Endpoints
- **Authentication**
  - `POST /auth/login` – User login.
  - `POST /auth/register` – User registration.
- **User Management**
  - `GET /users/{id}` – Retrieve user details.
  - `PUT /users/{id}` – Update user information.
  - `DELETE /users/{id}` – Delete a user.

## License
This project is open-source and can be modified as per requirements.

