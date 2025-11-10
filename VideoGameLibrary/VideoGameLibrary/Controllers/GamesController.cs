using Microsoft.AspNetCore.Mvc;
using System;
using VideoGameLibrary.Models;
using VideoGameLibrary.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace VideoGameLibrary.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
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
            if (game == null) return NotFound();
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
                await _gameService.AddGameAsync(game);
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        /** Update Game **/
        // GET: /Games/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null) return NotFound();
            return View(game);
        }

        // POST: /Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Game game)
        {
            if (id != game.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                await _gameService.UpdateGameAsync(game);
                return RedirectToAction(nameof(Index));
            }

            return View(game);
        }

        /** Delete Game **/
        // GET: /Games/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null) return NotFound();
            return View(game);
        }

        // POST: /Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _gameService.DeleteGameAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
