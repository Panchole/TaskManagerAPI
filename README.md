# TaskManager API

A high-performance task management API built with .NET 9 using Minimal APIs and Native AOT compilation.

## 🚀 Technologies

-   **.NET 9**: Latest version with performance improvements
-   **Minimal APIs**: Lightweight HTTP request pipeline
-   **Native AOT**: Ahead-of-time compilation for optimal performance
-   **Entity Framework Core**: ORM for data access
-   **Clean Architecture**: Separation of concerns for maintainability
-   **CQRS Pattern**: Using MediatR for command/query separation
-   **JWT Authentication**: Secure API access
-   **Swagger/OpenAPI**: API documentation

## 🏗️ Architecture

This project follows Clean Architecture principles with the following layers:

-   **Domain**: Contains business entities and logic
-   **Application**: Contains business use cases
-   **Infrastructure**: Contains implementation details (database, external services)
-   **API**: Contains the API controllers and configuration

## 🛠️ Setup and Configuration

### Prerequisites

-   .NET 9 SDK
-   Docker (for SQL Server)
-   Visual Studio Code or Visual Studio 2022

### Getting Started

1. Clone the repository

    ```
    git clone https://github.com/YOUR_USERNAME/TaskManagerAPI.git
    cd TaskManagerAPI
    ```

2. Start the SQL Server container

    ```
    docker-compose up -d
    ```

3. Run the migrations

    ```
    dotnet ef database update --project src/TaskManagerAPI.Infrastructure --startup-project src/TaskManagerAPI.Api
    ```

4. Run the application

    ```
    dotnet run --project src/TaskManagerAPI.Api
    ```

5. Access the API at `https://localhost:5001` or `http://localhost:5000`

## 📝 API Documentation

Once the application is running, you can access the Swagger documentation at:

`https://localhost:5001/swagger`

## 🧪 Testing

Run the tests with:

```
dotnet test
```

## 📦 Deployment

The application is configured for Native AOT compilation, which produces a self-contained executable with improved startup time and reduced memory footprint.

To publish:

```
dotnet publish src/TaskManagerAPI.Api -c Release
```

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

```

Explicación: Este README proporciona una visión general del proyecto, las tecnologías utilizadas, la arquitectura, instrucciones de configuración y ejecución, y otra información relevante. Es completo pero conciso, y utiliza emojis y formato para mejorar la legibilidad.
```
