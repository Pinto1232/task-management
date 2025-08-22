# task-management
This is a full-Stack web application that serves as task management inventory system for small enterprises.

# My Tech Stack

* Backend
  * .Net core 8.1
  * Entity Framework Core 9
  * Microsfot SQL Server Database
  * Authentication
  * JWT Token Authentication & Authorization
  * Role-based access (USER, ADMIN)
  * Secure password hashing using BCrypt.NET-Core


* Frontend
  * React.js
  * React Query Tanskstak Toolkit (RTK Query)
  * Typescript
  * Vite
  * CSS3

* Architecture
  * Controller Based Architecture (Not complex), but I could you clean architecture if needed.

* Installation
  * Clone the repository.
  * Run `dotnet run` in backend folder to start the server.
  * Run `npm install && npm start` in frontend folder to start the client.
  * Make sure to set up the database connection string in the backend's `appsettings.json` file.
  * Make sure to create a database named "TaskManagement" in your SQL Server instance before running the server.
  * Make sure to update the database connection string in the frontend's `.env.local` file with your own credentials.

* Deployment
  * The backend is deployed on Azure App Service. If required, I can deploy it on AWS or GCP.
  * The database is hosted on Microsoft Azure SQL Database. If required, I can deploy it on AWS or GCP.
  * The frontend is deployed on Vercel. If required, I can deploy it on AWS or GCP.

The backend will be run on port 5131 and the frontend will be run on port 3000.
