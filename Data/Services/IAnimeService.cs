using AniWatch.Data.Base;
using AniWatch.Data.ViewModels;
using AniWatch.Models;
using System.Threading.Tasks;

namespace AniWatch.Data.Services
{
    public interface IAnimeService : IEntityBaseRepository<Anime>
    {
        Task<Anime> GetAnimeByIdAsync(int id);
        Task<NewAnimeDropdownsVM> GetNewAnimeDropdownsValues();
        Task AddNewAnimeAsync(NewAnimeVM data);
        Task UpdateAnimeAsync(NewAnimeVM data);
    }
}