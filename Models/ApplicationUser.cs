using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AniWatch.Models
{
    public class ApplicationUser 
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }
    }
}
