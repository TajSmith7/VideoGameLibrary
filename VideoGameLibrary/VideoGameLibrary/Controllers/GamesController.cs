using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using VideoGameLibrary.Models;
using VideoGameLibrary.Services;

namespace VideoGameLibrary.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GamesController> _logger;

        public GamesController(IGameService gameService, ILogger<GamesController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        /** Read Game **/
        /** GET : /Games **/
        public async Task<IActionResult> Index()
        {
            var games = await _gameService.GetAllGamesAsync();
            return View(games);
        }

        /** Game Details **/
        // GET: /Games/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null)
            {
                _logger.LogWarning("Details requested for non-existent game with ID {GameId}", id);
                return NotFound();
            }
            return View(game);
        }

        /** Create Game **/
        // GET: /Games/Create
        public IActionResult Create() // GET create page
        {
            return View();
        }

        /** Create Game POST **/
        // POST: /Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Game game)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _gameService.AddGameAsync(game);
                    _logger.LogInformation("Game created successfully: {@Game}", new { game.Id, game.Name });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating game: {@Game}", new { game.Name });
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            else
            {
                _logger.LogWarning("Create game validation failed: {@Game}", new { game.Name });
            }

            return View(game);
        }

        /** Update Game **/
        // GET: /Games/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null)
            {
                _logger.LogWarning("Edit requested for non-existent game with ID {GameId}", id);
                return NotFound();
            }
            return View(game);
        }

        // POST: /Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Game game)
        {
            if (id != game.Id)
            {
                _logger.LogWarning("Edit failed: route ID {RouteId} does not match game ID {GameId}", id, game.Id);
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _gameService.UpdateGameAsync(game);
                    _logger.LogInformation("Game updated successfully: {@Game}", new { game.Id, game.Name });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating game: {@Game}", new { game.Id, game.Name });
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            else
            {
                _logger.LogWarning("Edit game validation failed: {@Game}", new { game.Id, game.Name });
            }

            return View(game);
        }

        /** Delete Game **/
        // GET: /Games/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null)
            {
                _logger.LogWarning("Delete requested for non-existent game with ID {GameId}", id);
                return NotFound();
            }
            return View(game);
        }

        // POST: /Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _gameService.DeleteGameAsync(id);
                _logger.LogInformation("Game deleted successfully: GameId {GameId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting game with ID {GameId}", id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
