CREATE PROCEDURE GetNewestGames
AS
BEGIN
    SELECT TOP 5 Id, Name, ReleaseDate, Description
    FROM Games
    ORDER BY ReleaseDate DESC;
END;