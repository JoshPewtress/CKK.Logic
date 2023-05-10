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
         // Validate quantity is positive and a number
         if (quantity >= 0 && quantity <= int.MaxValue) 
         {
            // Check if prod already exists
            if (_products.Contains(GetProductById(prod.GetId())))
            {
               GetProductById(prod.GetId()).SetQuantity(GetProductById(prod.GetId()).GetQuantity() + quantity);
               return GetProductById(prod.GetId());
            }
            else
            {
               ShoppingCartItem newCartItem = new ShoppingCartItem(prod, quantity);
               _products.Add(newCartItem);
               return newCartItem;
            }
         }

         return null;
      }

      public ShoppingCartItem RemoveProduct(int id, int quantity)
      {    
         // Validate provided quantity
         if (quantity >= 0)
         {
            // Checks if list contains the Id
            if (_products.Contains(GetProductById(id)))
            {
               // Item quantity is enough to remove
               if (GetProductById(id).GetQuantity() - quantity > 0)
               {
                  GetProductById(id).SetQuantity(GetProductById(id).GetQuantity() - quantity);
                  return GetProductById(id);
               }
               // Quantity to remove is too high, remove item
               else
               { 
                  _products.Remove(GetProductById(id));

                  ShoppingCartItem newItem = new ShoppingCartItem(null, 0);
                  return newItem;
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
