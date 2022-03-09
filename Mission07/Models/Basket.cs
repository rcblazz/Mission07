﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

//a model for our cart
namespace Mission07.Models
{
    public class Basket
    {
        public List <BasketLineItem> Items { get; set; } = new List<BasketLineItem>();
        
        //Adding an item to the cart, but if the bookid is already there, just add to the quantity
        public virtual void AddItem (Book bo, int qty)
        {
            BasketLineItem line = Items
                .Where(b => b.Book.BookId == bo.BookId)
                .FirstOrDefault();

            if (line == null)
            {
                Items.Add(new BasketLineItem
                {
                    Book = bo,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }

        //Remove a single book
        public virtual void RemoveItem (Book bo)
        {
            Items.RemoveAll(x => x.Book.BookId == bo.BookId);
        }
        
        //Clear the whole cart
        public virtual void ClearBasket()
        {
            Items.Clear();
        }
        //Calculating the total
        public double CalculateTotal() 
        { 
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);

            return sum;
        }
    }

    public class BasketLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
