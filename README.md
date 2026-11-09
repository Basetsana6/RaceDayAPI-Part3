# RaceDayAPI-Part3
RaceDayAPI and MVC application for PROG6212 POE Part 3

1. Project Overview

RaceDay is a web-based race event management system developed for PROG6212.

The application allows participants to register and log in, view available sporting events and register for races. Organisers can manage race events, create race categories and manage race results.

The system was developed using ASP.NET Core MVC, ASP.NET Core Web API and SQL Server.

2. Technologies Used

* C#
* ASP.NET Core MVC
* ASP.NET Core Web API
* .NET 8
* Entity Framework Core
* SQL Server LocalDB
* HTML and CSS
* Bootstrap
* Azure Blob Storage SDK
* Azurite Storage Emulator
* GitHub Actions
* Docker configuration files

3. Main System Features

User Registration and Login

* Participant registration.
* User login and password verification.
* Role-based functionality for participants and organisers.

Organiser Features

* Organiser dashboard.
* Create race events.
* View and manage events.
* Edit and delete events.
* Create and manage race categories.
* Enter and manage race results.

Participant Features

* Participant dashboard.
* View available events.
* Register for race events.
* View personal event enrolments.
* Access race result functionality.

Database

The application uses SQL Server LocalDB with Entity Framework Core.

Database migrations are used to create and update the database structure.

4. Project Structure

The solution contains the following projects and supporting files:

* RaceDayAPI: Web API, controllers, models and database functionality.
* RaceDayMVC: MVC website, controllers, views and services.
* RaceDayAPI.Test: Automated test project.
* .github/workflows: GitHub Actions build and test workflow.
* Dockerfile: Container configuration for the API and MVC projects.
* docker-compose.yml: Docker Compose configuration.
* .dockerignore: Files excluded from Docker builds.

5. Requirements

To run the application locally, the following software is required:

* Visual Studio 2022.
* .NET 8 SDK.
* SQL Server LocalDB.
* Node.js (for Azurite).
* Azurite Storage Emulator (for local Blob Storage testing).

6. Database Configuration

The application uses SQL Server LocalDB.

The configured database connection uses:

(localdb)\MSSQLLocalDB

The database name is:

RaceDayDB

To create or update the database, open Visual Studio Package Manager Console, select the RaceDayAPI project and run:

Update-Database

The database migration must be applied before running the application for the first time.

7. Running the Application

1. Open RaceDayAPI.sln in Visual Studio 2022.
2. Ensure the database connection is configured.
3. Set RaceDayAPI and RaceDayMVC as startup projects.
4. Start the application using F5.

The local development addresses are:

* API Swagger: https://localhost:7184/swagger
* MVC Website: https://localhost:7264

8. Azure Blob Storage

Azure Blob Storage functionality was implemented using the Azure.Storage.Blobs NuGet package.

An image upload endpoint was added to the API to support event image uploads.

Azurite was installed and configured for local Blob Storage testing.

The local emulator uses:

http://127.0.0.1:10000

Current limitation: The image upload endpoint returned an HTTP 500 error during testing. The upload functionality therefore requires further troubleshooting. A successful upload to a real Azure Storage Account has not been verified.

9. Docker Configuration

Docker configuration files were prepared for the API and MVC applications.

The solution includes:

* API Dockerfile.
* MVC Dockerfile.
* Docker Compose configuration.
* Docker ignore configuration.

Current limitation: Docker Desktop could not be installed successfully in the managed Windows virtual machine. Container builds and execution have therefore not been verified.

10. GitHub Actions

A GitHub Actions workflow was prepared to automate:

* Repository checkout.
* .NET 8 setup.
* Solution restoration.
* Solution build.
* Automated test execution.

The workflow is configured in .github/workflows/dotnet.yml.

11. Testing

The application was built and tested locally.

The following were verified:

* User registration and login.
* Organiser login.
* Event creation, viewing, updating and deletion.
* SQL Server database creation and migration.
* Event data persistence after restarting the applications.

Other functionality was implemented but requires further end-to-end verification.

12. Conclusion

RaceDay provides a platform for managing sporting events, participants, categories and race results.

The project demonstrates the use of ASP.NET Core MVC, Web API, Entity Framework Core and SQL Server in developing a web-based event management system.

Additional deployment and cloud storage functionality was prepared, with further testing required for Docker and Azure Blob Storage.
