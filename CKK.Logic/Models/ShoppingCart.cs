using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
   public class ShoppingCart
   {
      private Customer Customer;
      private List<ShoppingCartItem> Products = new List<ShoppingCartItem>();

      public ShoppingCart(Customer cust)
      {
         Customer = cust;
      }

      public int GetCustomerId()
      {
         return Customer.Id;
      }

      public ShoppingCartItem AddProduct(Product prod, int quantity)
      {
         // Check list for product matching Id
         foreach (var item in Products)
         {
            // Validate quantity is not negative
            if (quantity >= 0)
            {
               // Found product, add to quantity
               if (item.Product.Id == prod.Id)
               {
                  item.Quantity = item.Quantity + quantity;
                  return item;
               }
            }
         }

         ShoppingCartItem newCartItem = new ShoppingCartItem(prod, quantity);
         Products.Add(newCartItem);
         return newCartItem;
      }

      public ShoppingCartItem RemoveProduct(int id, int quantity)
      {
         // Iterate through _products list
         foreach (var item in Products)
         {
            // Validate provided quantity
            if (quantity >= 0)
            {
               // Checks if list contains the Id
               if (item.Product.Id == id)
               {
                  // Item quantity is enough to remove
                  if (item.Quantity >= quantity)
                  {
                     item.Quantity = item.Quantity - quantity;
                     return item;
                  }
                  // Quantity to remove is too high, remove item
                  else
                  {
                     Products.Remove(item);
                     return new ShoppingCartItem(new Product(), 0);
                  }
               }
            }
         }
         // Product was not found
         return null;
      }

      public ShoppingCartItem GetProductById(int id)
      {
         var product =
            from item in Products
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
            (from item in Products
             where item.GetTotal() > 0
             select item.GetTotal()).Sum();

         return total;
      }

      public List<ShoppingCartItem> GetProducts()
      {
         return Products;
      }
   }
}
