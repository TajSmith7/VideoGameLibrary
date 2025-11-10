using VideoGameLibrary.Data;
using VideoGameLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameLibrary.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAllGamesAsync();
        Task<Game?> GetGameByIdAsync(int id);
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
    }
}
