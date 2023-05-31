﻿using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
   public class ShoppingCartItem : InventoryItem
   {

      public ShoppingCartItem(Product product, int Quantity) : base()
      {
      }

      public decimal GetTotal() => base.Product.Price * base.Quantity;
   }
}
