using CKK.Logic.Interfaces;
using System;

namespace CKK.Logic.Models
{
   [Serializable]
   public class ShoppingCartItem : InventoryItem
   {

      public ShoppingCartItem(Product Product, int Quantity) : base(Product, Quantity)
      {
      }

      public decimal GetTotal() => base.Product.Price * base.Quantity;
   }
}
