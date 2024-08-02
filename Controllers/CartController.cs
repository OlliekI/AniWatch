using AniWatch.Data.Enums;
using AniWatch.Infrastructure;
using AniWatch.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AniWatch.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext context;

        public CartController(AppDbContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session
                .GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new CartViewModel()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Price * x.Quantity)
            };
            return View(cartVM);
        }

        public async Task<IActionResult> Add(int id)
        {

            Anime movie = await context.Anime.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.
                GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(x =>
            x.MovieId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(movie));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Index");

            return ViewComponent("Index");


        }

        public IActionResult Decrease(int id)
        {


            List<CartItem> cart = HttpContext.Session.
                GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(x =>
            x.MovieId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(x => x.MovieId == id);
            }

            HttpContext.Session.SetJson("Cart", cart);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);

            }

            return RedirectToAction("Index");

        }

        public IActionResult Remove(int id)
        {

            List<CartItem> cart = HttpContext.Session.
                GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(x => x.MovieId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);

            }

            return RedirectToAction("Index");

        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");


            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return Redirect(Request.Headers["Referer"].ToString());

            return Ok();

        }



    }
}
