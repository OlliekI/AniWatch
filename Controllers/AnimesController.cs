using AniWatch.Data;
using AniWatch.Data.Enums;
using AniWatch.Data.Services;
using AniWatch.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AniWatch.Controllers
{
    [Authorize]
    public class AnimesController : Controller
    {
        private readonly IAnimeService _service;
        private readonly AppDbContext _context;

        public AnimesController(IAnimeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            return View(allMovies);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", allMovies);
        }

        //GET: Anime/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetAnimeByIdAsync(id);
            return View(movieDetail);
        }

        //GET: Anime/Create
        public async Task<IActionResult> Create()
        {
            var animeDropdownsData = await _service.GetNewAnimeDropdownsValues();

            ViewBag.Cinemas = new SelectList(animeDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(animeDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(animeDropdownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewAnimeVM movie)
        {
            if (!ModelState.IsValid)
            {
                var animeDropdownsData = await _service.GetNewAnimeDropdownsValues();

                ViewBag.Cinemas = new SelectList(animeDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(animeDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(animeDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await _service.AddNewAnimeAsync(movie);
            return RedirectToAction(nameof(Index));
        }


        //GET: Anime/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var animeDetails = await _service.GetAnimeByIdAsync(id);
            if (animeDetails == null) return View("NotFound");

            var response = new NewAnimeVM()
            {
                Id = animeDetails.Id,
                Name = animeDetails.Name,
                Description = animeDetails.Description,
                Price = animeDetails.Price,
                StartDate = animeDetails.StartDate,
                EndDate = animeDetails.EndDate,
                ImageURL = animeDetails.ImageURL,
                MovieCategory = animeDetails.MovieCategory,
                CinemaId = animeDetails.CinemaId,
                ProducerId = animeDetails.ProducerId,
                ActorIds = animeDetails.Anime_Characters.Select(n => n.CharacterId).ToList(),
            };

            var animeDropdownsData = await _service.GetNewAnimeDropdownsValues();
            ViewBag.Cinemas = new SelectList(animeDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(animeDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(animeDropdownsData.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewAnimeVM anime)
        {
            if (id != anime.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var animeDropdownsData = await _service.GetNewAnimeDropdownsValues();

                ViewBag.Cinemas = new SelectList(animeDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(animeDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(animeDropdownsData.Actors, "Id", "FullName");

                return View(anime);
            }

            await _service.UpdateAnimeAsync(anime);
            return RedirectToAction(nameof(Index));
        }

        // GET: Characters/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var characterDetails = await _service.GetByIdAsync(id);
            if (characterDetails == null)
            {
                return View("NotFound");
            }
            return View(characterDetails);
        }

        // POST: Characters/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var characterDetails = await _service.GetByIdAsync(id);
            if (characterDetails == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}