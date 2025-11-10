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
