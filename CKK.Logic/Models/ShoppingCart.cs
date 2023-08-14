using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Models
{
   public class ShoppingCart : IShoppingCart
   {
      int ShoppingCartId { get; set; }
      int CustomerId { get; set; }
      public Customer Customer { get; set; }
      public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

      public ShoppingCart(Customer cust)
      {
         Customer = cust;
      }

      public ShoppingCartItem AddProduct(Product prod, int quantity)
      {
         // Check list for product matching Id
         foreach (var item in ShoppingCartItems)
         {
            // Throw exception if quantity is less than or equal to 0
            if (quantity <= 0)
            {
               throw new InventoryItemStockTooLowException("Quantity must be greater than 0.");
            }
            else if (quantity > 0)
            {
               if (item.Product.Id == prod.Id)
               {
                  item.Quantity = item.Quantity + quantity;
                  return item;
               }
            }
         }

         ShoppingCartItem newCartItem = new ShoppingCartItem(prod, quantity);
         ShoppingCartItems.Add(newCartItem);
         return newCartItem;
      }

      public ShoppingCartItem RemoveProduct(int id, int quantity)
      {
         if (quantity < 0)
         {
            throw new ArgumentOutOfRangeException("Quantity cannot be negative.");
         }

         // Iterate through _products list
         foreach (var item in ShoppingCartItems)
         {
            if (quantity >= 0)
            {
               // Checks if list contains the Id
               if (item.Product.Id == id)
               {
                  // Item quantity is enough to remove
                  if (item.Quantity > quantity)
                  {
                     item.Quantity = item.Quantity - quantity;
                     return item;
                  }
                  // Quantity to remove is too high, remove item
                  else
                  {
                     ShoppingCartItems.Remove(item);
                     return new ShoppingCartItem(new Product(), 0);
                  }
               }
            }
         }

         // Product was not found
         throw new ProductDoesNotExistException("Product does not exist.");
      }

      public ShoppingCartItem GetProductById(int id)
      {
         if (id < 0)
         {
            throw new InvalidIdException("Id cannot be less than 0");
         }

         var product =
            from item in ShoppingCartItems
            where item.Product.Id.Equals(id)
            select item;

         if (product.Any())
         {
            return product.FirstOrDefault();
         }

         return null;
      }

      public decimal GetTotal()
      {
         // Queries _product for the total of each element and uses Sum() to add them
         var total =
            (from item in ShoppingCartItems
             where item.GetTotal() > 0
             select item.GetTotal()).Sum();

         return total;
      }
   }
}
