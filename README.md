# GameDatabase

GameDatabase is a mini database built on ASP .NET and EF Core. 

EF Core is employed as an object-relational mapper (ORM), while ASP .NET serves the web app. Additionally, Swagger UI is used to ease interactions from and to the database. 

## Folders to Note
Each folder houses its own set of files that specialize in achieving a certain goal.

- The `Model` folder contains all entities (e.g., Games and Engines) and their relations. Just like plain SQL, each entity has its own key and fields.