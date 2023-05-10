using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
   public class ShoppingCart
   {
      private Customer _customer;
      private List<ShoppingCartItem> _products = new List<ShoppingCartItem>();

      public ShoppingCart(Customer cust) 
      {
         _customer = cust;
      }

      public int GetCustomerId()
      {
         return _customer.GetId();
      }

      public ShoppingCartItem AddProduct(Product prod, int quantity)
      {
         // Check list for product matching Id
         foreach (var item in _products)
         {
            // Validate quantity is not negative
            if (quantity >= 0)
            {
               // Found product, add to quantity
               if (item.GetProduct().GetId() == prod.GetId())
               {
                  item.SetQuantity(item.GetQuantity() + quantity);
                  return item;
               }
            }
         }

         ShoppingCartItem newCartItem = new ShoppingCartItem(prod, quantity);
         _products.Add(newCartItem);
         return newCartItem;
      }

      public ShoppingCartItem RemoveProduct(int id, int quantity)
      {
         // Iterate through _products list
         foreach (var item in _products)
         {
            // Validate provided quantity
            if (quantity >= 0)
            {
               // Checks if list contains the Id
               if (item.GetProduct().GetId() == id)
               {
                  // Item quantity is enough to remove
                  if (item.GetQuantity() >= quantity)
                  {
                     item.SetQuantity(item.GetQuantity() - quantity);
                     return item;
                  }
                  // Quantity to remove is too high, remove item
                  else
                  {
                     _products.Remove(item);
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
            from item in _products
            where item.GetProduct().GetId().Equals(id)
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
            (from item in _products
            where item.GetTotal() > 0
            select item.GetTotal()).Sum();

         return total;
      }

      public List<ShoppingCartItem> GetProducts()
      {
         return _products;
      }
   }
}
