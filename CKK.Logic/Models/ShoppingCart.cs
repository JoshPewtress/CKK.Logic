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

            // Validate quantity is not negative **WIP CODE**
            /*if (quantity >= 0)
            {
               // Found product, add to quantity
               if (item.Product.Id == prod.Id)
               {
                  item.Quantity = item.Quantity + quantity;
                  return item;
               }
            }*/
         }

         ShoppingCartItem newCartItem = new ShoppingCartItem(prod, quantity);
         Products.Add(newCartItem);
         return newCartItem;
      }

      public ShoppingCartItem RemoveProduct(int id, int quantity)
      {
         if (quantity < 0)
         {
            throw new ArgumentOutOfRangeException("Quantity cannot be negative.");
         }

         // Iterate through _products list
         foreach (var item in Products)
         {
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

            /* Validate provided quantity **WIP CODE**
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
            }*/
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
