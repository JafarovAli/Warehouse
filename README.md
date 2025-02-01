**🔐Warehouse Security Module**

A security module for the Warehouse Management System, built with .NET 6 and Microservices Architecture, utilizing modern technologies like RabbitMQ, MinIO, Redis, and Docker to ensure secure authentication and authorization.

**📌 Features**

🛡 JWT-based Authentication for secure API access

🔑 Role-Based Access Control (RBAC)

📦 RabbitMQ for event-driven security notifications

☁ MinIO for encrypted file storage

⚡ Redis for session management and caching

🐳 Dockerized Deployment for seamless scalability

**🛠 Tech Stack**

Backend: .NET 6, ASP.NET Core Identity, Entity Framework Core

Message Queue: RabbitMQ (for security logs and events)

Storage: MinIO (for encrypted data storage)

Caching: Redis (for session storage and authentication tokens)

Database: SQL Server

Containerization: Docker & Docker Compose

**🚀 Getting Started**

1️⃣ Prerequisites

Ensure you have the following installed:

.NET 6 SDK

Docker & Docker Compose

RabbitMQ

MinIO

Redis

SQL Server

2️⃣ Clone the Repository
```bash
git clone https://github.com/JafarovAli/Warehouse.git
cd Warehouse
  ```

3️⃣ Configure Environment Variables
Create a .env file and configure the following:
```bash
RABBITMQ_URI=amqp://user:password@localhost
MINIO_URI=http://localhost:9000
REDIS_URI=redis://localhost
DB_CONNECTION=Server=localhost;Database=your database name;User Id=sa;Password=your password;
JWT_SECRET_KEY=your_secret_key  
  ```
4️⃣ Run with Docker Compose
```bash
docker-compose up -d
  ```
