using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AniWatch.Models
{
    public class Character_Anime
    {
        public int AnimeId { get; set; }
        public Anime Anime { get; set; }

        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}