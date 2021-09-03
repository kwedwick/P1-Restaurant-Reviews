# project 1: store web application or restaurant reviews application
July 2021 Arlington .NET / Nick Escalona

## common requirements

* good Git practices
* CI with compile, automated tests, static analysis
* CD to cloud service (like Azure App Service)
### functionality
* client-side validation
* server-side validation
* exception handling
* CSRF prevention
* persistent data; no prices, restaurants, history, etc. hardcoded in C#
* logging of exceptions, EF SQL commands, and other events
* (optional: asynchronous network & file I/O)

### design
* project layout given here is only a suggestion. the general idea of
  separation of concerns is a requirement.
* use EF Core (either database-first approach with a SQL script or code-first approach with migrations)
* use an Azure SQL DB in third normal form; include a database diagram
* don't use public fields
* define and use at least one interface

#### core / domain / business logic
* class library
* contains all business logic
* contains domain classes (restaurant/review/user or customer/order/store/product etc.)
* documentation with `<summary>` XML comments on all public types and members (optional: `<params>` and `<return>`)
* (recommended: has no dependency on UI, data access, or any input/output considerations)

#### user interface
* ASP.NET Core MVC web application
* separate request processing and presentation concerns with MVC pattern
* strongly-typed views
* minimize logic in views
* use dependency injection
* customize the default styling to some extent
* keep CodeNamesLikeThis out of the visible UI
* has only display- and input-related code

#### data access
* class library
* contains EF Core DbContext and entity classes
* contains data access logic but no business logic
* use repository pattern for separation of concerns

#### test
* at least 10 test methods
* focus on unit testing business logic
* data access tests (if present) should not impact the app's actual database

## store web application requirements

### functionality
* place orders to store locations for customers
* add a new customer
* search customers by name
* display details of an order
* display all order history of a store location
* display all order history of a customer
* (optional: order history can be sorted by earliest, latest, cheapest, most expensive)
* (optional: get a suggested order for a customer based on his order history)
* (optional: display some statistics based on order history)

### design

#### customer
* has first name, last name, etc.
* (optional: has a default store location to order from)

#### order
* has a store location
* has a customer
* has an order time (when the order was placed)
* can contain multiple kinds of product in the same order
* rejects orders with unreasonably high product quantities
* (optional: some additional business rules, like special deals)

#### location
* has an inventory
* inventory decreases when orders are accepted
* rejects orders that cannot be fulfilled with remaining inventory
* (optional: for at least one product, more than one inventory item decrements when ordering that product)

#### product (etc.)

## restaurant reviews web application requirements

### overview

The restaurant review application is a software that lets customers leave reviews for restaurants. Designed with functionality that helps choosing the next restaurant to eat at!

### functionality

- add a new user
- ability to search user as admin
- display details of a restaurant for user
- add reviews to a restaurant as a user
- view details of restaurants as a user
- view reviews of restaurants as a user
- calculate reviews’ average rating for each restaurant
- search restaurant (by name, rating, zip code, etc.)

### models

- User
- Restaurant
- Review
- any others you may need
