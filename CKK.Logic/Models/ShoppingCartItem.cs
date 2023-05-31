using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
   public class ShoppingCartItem : InventoryItem
   {

      public ShoppingCartItem(Product Product, int Quantity) : base(Product, Quantity)
      {
      }

      public decimal GetTotal() => base.Product.Price * base.Quantity;
   }
}
