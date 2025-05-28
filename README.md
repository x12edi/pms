# Portfolio Management System (PMS)
- Overview
The Portfolio Management System (PMS) is a comprehensive web application designed to manage investment portfolios, allowing users to insert and monitor stock trade data, mutual funds, and other asset classes (e.g., bonds, ETFs, real estate). It includes a portfolio dashboard for tracking performance and allocation. The system is built using ASP.NET Core, Entity Framework Core (EF Core), and follows a layered architecture with the Repository and Unit of Work (UoW) patterns. The current stage focuses on the data layer, implemented in the PMS.Domain and PMS.Infrastructure projects, with a temporary PMS.Web project for database migrations.
# Prerequisites
- .NET 8 SDK: Download
- SQL Server LocalDB: Included with Visual Studio or install via SQL Server Express
- EF Core Tools: Installed via NuGet
# Current Status
- Completed:
Data layer with 13 entities, EF Core PmsDbContext, and Repository/UoW pattern.
SQL Server LocalDB database (PmsDb) with migrations and sample data.
migrations and seeding.
