using System.Collections.Generic;

namespace AniWatch.Models
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public double GrandTotal { get; set; }
    }
}
