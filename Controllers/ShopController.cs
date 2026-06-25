using Microsoft.AspNetCore.Mvc;
using Homework06Models;
using System.Text.Json;

namespace Homework06Controller
{
    public class ShopController : Controller
    {
        public IActionResult Products()
        {
            var products = new List<Product>
            {
                new Product { Name = "T-shirt", Price = 2000 },
                new Product { Name = "Shoes", Price = 5000 },
                new Product { Name = "Bag", Price = 3000 }
            };
            return View(products);
        }

        public IActionResult AddToCart(string name, double price)
        {
            List<Product> cart = new List<Product>();

            var cookieData = Request.Cookies["UserCart"];
            if (cookieData != null)
                cart = JsonSerializer.Deserialize<List<Product>>(cookieData);

            cart.Add(new Product { Name = name, Price = price });

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(2);

            Response.Cookies.Append("UserCart", JsonSerializer.Serialize(cart), options);

            return RedirectToAction("Products");
        }

        public IActionResult Cart()
        {
            var cookieData = Request.Cookies["UserCart"];
            List<Product> cart;

            if (cookieData == null)
                cart = new List<Product>();
            else
                cart = JsonSerializer.Deserialize<List<Product>>(cookieData);

            return View(cart);
        }

        public IActionResult ClearCart()
        {
            Response.Cookies.Delete("UserCart");
            return RedirectToAction("Cart");
        }
    }
}
