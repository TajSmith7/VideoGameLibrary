using VideoGameLibrary.Data;
using VideoGameLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameLibrary.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAllGamesAsync();
        Task<Game?> GetGameByIdAsync(int id);
        Task AddGameAsync(Game game);
        Task UpdateGameAsync(Game game);
        Task DeleteGameAsync(int id);
        Task<List<Game>> GetNewestGamesAsync();

    }

    public class GameService : IGameService
    {
        private readonly VideoGameLibraryContext _context;

        public GameService(VideoGameLibraryContext context)
        {
            _context = context;
        }

        //get all games with genres and platforms
        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await _context.Games
                .Include(g => g.GameGenres)
                    .ThenInclude(gg => gg.Genre)
                .Include(g => g.GamePlatforms)
                    .ThenInclude(gp => gp.Platform)
                .ToListAsync();
        }

        //get game by id
        public async Task<Game?> GetGameByIdAsync(int id)
        {
            return await _context.Games
                .Include(g => g.GameGenres)
                    .ThenInclude(gg => gg.Genre)
                .Include(g => g.GamePlatforms)
                    .ThenInclude(gp => gp.Platform)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task AddGameAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGameAsync(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGameAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Game>> GetNewestGamesAsync()
        {
            return await _context.Games
                .FromSqlRaw("EXEC GetNewestGames")
                .ToListAsync();
        }
    }
}
