using AniWatch.Data.Base;
using AniWatch.Data.Enums;
using AniWatch.Models;

namespace AniWatch.Data.Services
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context)
        {
        }
    }
}