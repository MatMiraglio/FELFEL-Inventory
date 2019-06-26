# FELFEL-Inventory
Inventory Management System

API built with asp.net core 2.0

Structure:

1 Domain: Business rules

2 Use cases: What our program can do following the business rules

3 Ports: Applications that conect the user to the use cases. In this case an API but we could easly add others like an MVC app.

4 External: Any frameworks or IO mechanisms. ORMs, DBs and any framewok should be here and not depended directly on our application. 


Dependency Rule:
The code on one layer can only depend on code in a lower layer 4 -> 3 - > 2 -> 1

The domain and the use cases are not allowed to depend on external frameworks or ports.

Tools and Techniques used:

ASP.NET Core

Entity Framework Core

SQL Server

Unit of work and repository patterns

XUnit and MOQ
