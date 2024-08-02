using AniWatch.Models;
using System.Collections.Generic;

namespace AniWatch.Data.ViewModels
{
    public class NewAnimeDropdownsVM
    {
        public NewAnimeDropdownsVM()
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Character>();
        }

        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Character> Actors { get; set; }
    }
}