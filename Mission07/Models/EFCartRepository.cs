using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission07.Models
{
    public class EFCartRepository : ICartRepository
    {
        private BookstoreContext context;
        public EFCartRepository (BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Cart> Carts => context.Carts.Include(x => x.Lines).ThenInclude(x => x.Book);

        //if there isn't already a cart item, add it
        public void SaveCart(Cart cart)
        {
            context.AttachRange(cart.Lines.Select(x => x.Book));

            if (cart.CartId == 0)
            {
                context.Carts.Add(cart);
            }

            context.SaveChanges();
        }
    }
}
