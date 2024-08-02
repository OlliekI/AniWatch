using AniWatch.Data.Services;
using AniWatch.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AniWatch.Data.Base;
using AniWatch.Data.Enums;

namespace AniWatch.Data.Services
{
    public class CharactersService : EntityBaseRepository<Character>, ICharactersService
    {
        public CharactersService(AppDbContext context) : base(context) { }
    }
}