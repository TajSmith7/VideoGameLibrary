## Project Summary

The Video Game Library is an ASP.NET Core MVC application that allows users to store and manage information about video games, their genres, and platforms. Users can add new games, edit existing ones, and view related genres and platforms. This project demonstrates key ASP.NET Core MVC concepts, including entity modeling, separation of concerns with dependency injection, CRUD operations, diagnostics, logging, and the use of stored procedures. It also includes deployment to Azure to showcase a production-ready environment.

## Weekly Feature Planning

| Week | Concept | Feature | Goal | Acceptance Criteria | Evidence | Test Plan |
|------|---------|---------|------|------------------|---------|-----------|
| 10 | Modeling | Create Game, Genre, Platform entities | Store games and associate genres/platforms | - [ ] Tables created<br>- [ ] Many-to-many relationships work | Screenshots; write-up | Run migrations; check DB tables exist |
| 11 | Separation of Concerns / DI | Add GameService | Move game logic out of controller | - [ ] Service registered in DI<br>- [ ] Controller uses service<br>- [ ] Games can be retrieved | Screenshots; write-up | Call service endpoints; verify results |
| 12 | CRUD | Create/Edit/Delete forms for games | Users can manage games | - [ ] Forms display<br>- [ ] Validation works<br>- [ ] Changes persist in DB | Screenshots; write-up | Add/edit/delete a game; check DB |
| 13 | Diagnostics | `/healthz` endpoint | Check DB connectivity | - [ ] Returns healthy if DB is up<br>- [ ] Returns unhealthy if DB down | Screenshots; write-up | Stop DB and test endpoint |
| 14 | Logging | Log game CRUD actions | Track structured logs | - [ ] Logs for each CRUD action<br>- [ ] Logs include game ID | Screenshots; write-up | Perform CRUD; check logs |
| 15 | Stored Procedures | Get number of games per genre | Demonstrate calling a stored procedure and displaying results | - [ ] SP executes<br>- [ ] Results displayed in app | SQL script; code snippet calling SP; screenshot of results | Add games with genres; run SP in DB; verify displayed results |
| 16 | Deployment | Deploy to Azure | Make app publicly accessible | - [ ] App Service created<br>- [ ] App works online | Screenshots; write-up; URL | Visit URL; test `/healthz` and game list |

---
## Week 10 – Modeling

For Week 10, I set up the basic data model for the **VideoGameLibrary** app. I created three main entities: **Game**, **Genre**, and **Platform**, and added simple fields for each. For example, `Name` is required for all three entities, ensuring that every record has a meaningful identifier. The `Game` entity also includes optional fields for `ReleaseDate` and `Description` to provide additional details when available, without forcing unnecessary data entry. These fields help capture the essential information for each game while keeping the database simple and easy to manage at this stage.

Because games can belong to multiple genres and be available on multiple platforms, I added two join tables: **GameGenre** and **GamePlatform**. These tables enforce the many-to-many relationships between games, genres, and platforms, making sure that each combination is unique and properly structured.

I placed the entity classes in the **Models** folder and created a **VideoGameLibraryContext** in a **Data** folder. The DbContext includes `DbSet` properties for all entities and configures the composite keys for the join tables. I also registered the context in `Program.cs` using a connection string stored in `appsettings.json`.

Finally, I created and applied the initial migration. This successfully generated the database with all tables and relationships, providing a solid foundation for implementing CRUD operations, logging, and other features in the following weeks. 

#### Database Tables

**Games Table**  
![Games Table](screenshots/gameTable.png)

**Genres Table**  
![Genres Table](screenshots/genreTable.png)

**Platforms Table**  
![Platforms Table](screenshots/platformTable.png)

**GameGenres Table**  
![GameGenres Table](screenshots/gameGenreTable.png)

**GamePlatforms Table**  
![GamePlatforms Table](screenshots/gamePlatformTable.png)

---
### Week 11 – Separation of Concerns / Dependency Injection

For Week 11, I implemented **Separation of Concerns** and **Dependency Injection** in the VideoGameLibrary app. The goal was to create any non-UI logic in a dedicated service, keeping controllers thin and focused only on handling HTTP requests and responses. To do this, I created a new service called `GameService` with an interface `IGameService`. The interface defines the contract for the service, including methods to retrieve all games and get a game by its ID. This ensures that controllers only depend on the interface, not the implementation, making the application more modular and easier to maintain.

The `GameService` contains all the logic for querying the database through `VideoGameLibraryContext`, including loading related data like genres and platforms. I registered the service in the dependency injection container using `AddScoped` in Program.cs, which provides one service instance per HTTP request. Then, I injected `IGameService` into the `GamesController` constructor and used it in both the `Index` and `Details` actions. This keeps the controller lightweight, with no direct database access.

To test the service, I added a test game to the database. Visiting `/Games` displays all games using the service, and `/Games/Details/{id}` shows the details for a single game. This setup demonstrates that the service is properly injected, the controller is thin, and all logic is separated from the UI.

#### Evidence

**Games Service**  
![Games Service](screenshots/gameService.png)

**Games Controller**  
![Games Controller](screenshots/gamesController.png)

**Games Page**  
![Games Page](screenshots/gamesPage.png)

**Game Details Page**  
![Game Details Page](screenshots/gameDetailsPage.png)
