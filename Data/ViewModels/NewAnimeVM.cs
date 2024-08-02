using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace AniWatch.Data.ViewModels
{
    public class NewAnimeVM
    {
        public int Id { get; set; }

        [Display(Name = "Anime name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Anime description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Anime poster URL")]
        [Required(ErrorMessage = "Anime poster URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Anime start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Anime end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Anime category is required")]
        public AnimeCategory MovieCategory { get; set; }

        //Relationships
        [Display(Name = "Select actor(s)")]
        [Required(ErrorMessage = "Anime actor(s) is required")]
        public List<int> ActorIds { get; set; }

        [Display(Name = "Select a cinema")]
        [Required(ErrorMessage = "Anime cinema is required")]
        public int CinemaId { get; set; }

        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Anime producer is required")]
        public int ProducerId { get; set; }
    }
}