using Microsoft.AspNetCore.Mvc;
using Mission07.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission07.Controllers
{
    public class CartController : Controller
    {
        private ICartRepository repo { get; set; }
        private Basket basket { get; set; }
        public CartController(ICartRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Cart());
        }

        [HttpPost]
        public IActionResult Checkout(Cart cart)
            //make sure there is something in the cart before saving 
        {
            if (basket.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                cart.Lines = basket.Items.ToArray();
                repo.SaveCart(cart);
                basket.ClearBasket();

                return RedirectToPage("/CartSubmitted");
            }
            else
            {
                return View();
            }


            IdentitySeedData.num = IdentitySeedData.num + 5;
        }
    }
}
