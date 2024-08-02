using System.ComponentModel.DataAnnotations;

namespace AniWatch.Models
{
    public class CartItem
    {
        [Key] public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get { return Quantity * Price; } }
        public string Image {  get; set; }
        
        public CartItem() { }

        public CartItem(Anime movie) { 
        MovieId = movie.Id;
            MovieName = movie.Name;
            Price = movie.Price;
            Quantity = 1;
            Image = movie.ImageURL;
        }
    }
}
