using AniWatch.Data.Base;
using AniWatch.Data.Enums;
using AniWatch.Data.ViewModels;
using AniWatch.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AniWatch.Data.Services
{
    public class AnimeService : EntityBaseRepository<Anime>, IAnimeService
    {
        private readonly AppDbContext _context;
        public AnimeService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewAnimeAsync(NewAnimeVM data)
        {
            var newMovie = new Anime()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };
            await _context.Anime.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add Anime Characters
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Character_Anime()
                {
                    AnimeId = newMovie.Id,
                    CharacterId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Anime> GetAnimeByIdAsync(int id)
        {
            var movieDetails = await _context.Anime
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Anime_Characters).ThenInclude(a => a.Character)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewAnimeDropdownsVM> GetNewAnimeDropdownsValues()
        {
            var response = new NewAnimeDropdownsVM()
            {
                Actors = await _context.Characters.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateAnimeAsync(NewAnimeVM data)
        {
            var dbMovie = await _context.Anime.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.AnimeId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Anime Characters
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Character_Anime()
                {
                    AnimeId = data.Id,
                    CharacterId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}