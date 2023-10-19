﻿using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;
using System;

namespace CKK.Logic.Models
{
   [Serializable]
   public class ShoppingCartItem
   {
        public Product Product { get; set; }
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        private int quantity { get; set; }
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (value >= 0)
                {
                    quantity = value;
                }
                else
                {
                    throw new InventoryItemStockTooLowException();
                }
            }
        }

        public decimal GetTotal()
        {
            if (Product != null)
            {
                return Product.Price * quantity;
            }

            return 0;
        }    
   }
}
