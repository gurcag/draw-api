# draw-api


## **Tech Stack**

In the DrawAPI > src > Draw.API path, you can find the API project.

In the DrawAPI > test > Draw.API.Tests path, there is a UnitTest project.

Draw.API is an ASP.NET Core Web API application built on .NET 8.

### NuGet Packages and VS Extensions:

Microsoft.EntityFrameworkCore (as the ORM tool)

Microsoft.EntityFrameworkCore.Tools (for using CLI commands)

Microsoft.EntityFrameworkCore.Sqlite (as the DB Provider)

SQLite & SQL Server Compact Toolbox (for viewing EF DB Migrations as a Database UI)

The application is configured to run only on the HTTPS protocol and through https://localhost:9999 (configured in launchSettings.json).

API endpoints can be visualized on /swagger/index.html using Swagger.

The data for countries, teams, DrawOptionsEntity, and UserEntity, conveyed through the DrawDbContext.OnModelCreating() method, is added to the project as initial data using EF DB Migration.

### Run the following commands in the Package Manager Console: 

Add-Migration DrawDbInitialMigration

Update-Database.

Draw.API.Tests is an xUnit UnitTest project.


## **Endpoints**


The application is configured to run only on the HTTPS protocol and through https://localhost:9999 (configured in launchSettings.json).

API endpoints can be visualized on /swagger/index.html using Swagger.

/ (HTTP GET): Returns a "Draw API is up!" message to indicate that the application is running.

/api/countries (HTTP GET): Returns country information from the database. Returns HTTP 200 OK or HTTP 404 NotFound.

/api/teams (HTTP GET): Returns team information from the database. Returns HTTP 200 OK or HTTP 404 NotFound.

/api/drawOptions (HTTP GET): Instead of taking the number of groups as a parameter, it is preferred to keep it as a configuration in the database. Id and NumberOfGroups fields can be viewed with SQL Server Compact Toolbox. Returns HTTP 200 OK or HTTP 404 NotFound.

/api/Draws/{drawId} (HTTP GET): Returns the result of the performed draw based on the drawId parameter. Returns HTTP 200 OK or HTTP 404 NotFound.

/api/Draws (HTTP POST): HTTP POST endpoint for performing a draw. The request body is sent as a JSON with drawOptionsId, username, and password fields. If Username and Password are not valid, returns HTTP 401 Unauthorized; if DrawOptionsId is invalid, returns HTTP 400 BadRequest; if the operation is successful, returns HTTP 201 Created with HTTP HeaderLocation: /api/Draws/{drawId}.


## **Use Case Scenario**

Based on the scenario conveyed in the Study Case file:

In addition to the conveyed data of 8 countries and 32 teams, DrawOptions, Users, and Draws DB tables and data were created.

First, to set the number of groups for the draw, a call to /api/drawOptions is made.

The user information performing the draw is transmitted simply through /api/draws without an Auth structure.

To perform the draw, a /api/draw HTTP POST call is made with the body: {"drawOptionsId": 2, "username": "gurcag", "password": "yaman"}.

Upon a successful operation, an HTTP 201 Created response is received, and additionally, there is no need for a /api/Draws/{drawId} request.


## **Draw Performing Function**

Utilities.DrawUtilities.PerformDraw() is an extension method that performs the drawing.

The function is developed entirely based on the conveyed rules.

The drawing randomly selects a team from each country in sequence and dynamically works based on the conveyed number of groups.

The grouping and shuffling of teams by countries are done in the PerformDraw() method at lines 11 and 19.

As a use case, the group count parameter is only kept in the DrawOptions table in the DB as 4 and 8, but the function can work with 2, 4, 8, and 16.


## **Notes**

Dependency Injection for IDrawRepository and IBusinessService services is registered as Scoped (program.cs).

EF Core and Sqlite are preferred on the DB side. DrawDbContext is registered as a DbContext service (program.cs).

Controllers are based on the abstract class BaseController.

Business/Domain operations are inside the BusinessService and separated with IDrawRepository.

Data layer is inside DrawRepository. Repository Pattern is implemented, but I didn't implement the GenericRepository + UnitOfWork + Specification Pattern as I found it over-engineering for this work.

Models are named as DTOs (between Client and Controller), Models (Business layer), and Entity (Data layer).

FluentValidation or similar 3rd party tools are not used inside Controller methods, as I didn't see the need for it.

AutoMapper or similar mapping libraries are not used for mapping between models; hardcoded mapping is done as there are a few models.

Built on a Monolith structure instead of DDD or N-Tier architectures.

Authentication Middleware is not configured.

Logging and caching structures are not added.
MediatR is not used.
