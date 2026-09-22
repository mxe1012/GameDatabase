# GameDatabase

GameDatabase is a mini database built on ASP .NET and EF Core. 

EF Core is employed as an object-relational mapper (ORM), while ASP .NET serves the web app. Additionally, Swagger UI is used to ease interactions with the database via CRUD. 

## Folders and Files
Each folder houses its own set of files that specialize in achieving a certain goal.

- The `Controllers` folder contains files that build CRUD endpoints for each entity. Currently, the database supports `GET`. `POST`, `PUT`, and `DELETE`.

- The `Data` folder contains the database context file. This file is crucial as it allows the user to instrumentalize the ORM, and each table is initiallized within the file.

- Data Transfer Objects (DTOs) can found in the `DTOs` folder. DTOs relay information from and to the database. The user writes to the database using `Request` DTOs, while infomration is read and returned from the database by `Resonse` DTOs.

- The `Model` folder contains all entities (e.g., Games and Engines) and their relations. Just like plain SQL, each entity has its own primary key, foreign key(s), and other data fields unique to the entity. 

- `Services` contains files responsible for handling business logic for each entity. Each file makes use of its respective repository file found in `Repositories` utilizing the ORM. 