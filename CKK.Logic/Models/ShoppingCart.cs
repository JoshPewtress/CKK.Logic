using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
   public class ShoppingCart
   {
      private Customer _Customer;
      private ShoppingCartItem _product1;
      private ShoppingCartItem _product2;
      private ShoppingCartItem _product3;

      public ShoppingCart(Customer cust) 
      {
         _Customer = cust;
      }

      // Returns customer ID that was given from constructor
      public int GetCustomerId()
      {
         return _Customer.GetId();
      }
      
      // GetProductById returns the product that matches the id that was entered
      public ShoppingCartItem GetProductById(int id)
      {
         if (id == _product1.GetProduct().GetId())
         {
            return _product1;
         }
         else if (id == _product2.GetProduct().GetId())
         {
            return _product2;
         }
         else if (id == _product3.GetProduct().GetId())
         {
            return _product3;
         }

         return null;
      }

      // AddProduct method overload
      public ShoppingCartItem AddProduct(Product prod)
      {
         return AddProduct(prod, 1);
      }

      /* 
       * AddProduct method adds quantity to the product if quantity is valid
       * and returns the changed product or null
      */ 
      public ShoppingCartItem AddProduct(Product prod, int quantity)
      {
         // Checking for valid quantity
         if (quantity < 0)
         {
            return null;
         }

         // Checking if product exists and adding quantity to it if it does
         if (_product1 != null && prod.GetId() == _product1.GetProduct().GetId())
         {
            // Getting quantity from product and adding quantity to it
            _product1.SetQuantity(_product1.GetQuantity() + quantity);

            return _product1;
         }
         else if (_product2 != null && prod.GetId() == _product2.GetProduct().GetId())
         {
            _product2.SetQuantity(_product2.GetQuantity() + quantity);

            return _product2;
         }
         else if (_product3 != null && prod.GetId() == _product3.GetProduct().GetId())
         {
            _product3.SetQuantity(_product3.GetQuantity() + quantity);

            return _product3;
         }
         // No matches, checking for empty product and create a new product if one is found
         else
         {
            // Checking for empty product
            if (_product1 == null)
            {
               // Setting product to prod and quantity to quantity
               _product1 = new ShoppingCartItem(prod, quantity);

               return _product1;
            }
            else if (_product2 == null)
            {
               _product2 = new ShoppingCartItem(prod, quantity);

               return _product2;
            }
            else if (_product3 == null)
            {
               _product3 = new ShoppingCartItem(prod, quantity);

               return _product3;
            }

            // Unable to change anything
            return null;
         }

         
      }

      /*
       * RemoveProduct method checks for valid quantity and removes it from a product
       */ 
      public ShoppingCartItem RemoveProduct(Product prod, int quantity)
      {
         // Verifying positive quantity
         if (quantity <= 0)
         {
            return null;
         }

         // Checking if product exists and removes quantity from it if it does
         if (prod.GetId() == _product1.GetProduct().GetId())
         {
            // Getting quantity from product and removing quantity from it
            _product1.SetQuantity(_product1.GetQuantity() - quantity);

            return _product1;
         }
         else if (prod.GetId() == _product2.GetProduct().GetId())
         {
            _product2.SetQuantity(_product2.GetQuantity() - quantity);

            return _product2;
         }
         else if (prod.GetId() == _product3.GetProduct().GetId())
         {
            _product3.SetQuantity(_product3.GetQuantity() - quantity);

            return _product3;
         }
         else
         {
            return null;
         }
      }

      // GetTotal returns the total cost of all products
      public decimal GetTotal()
      {
         // Variable to store value
         decimal total = 0;

         // If product is empty, total will not store its total
         if (_product1 != null)
         {
            total += _product1.GetTotal();
         }
         if (_product2 != null)
         {
            total += _product2.GetTotal();
         }
         if (_product3 != null)
         {
            total += _product3.GetTotal();
         }

         return total;
      }

      // GetProduct returns the product that matches prodNum
      public ShoppingCartItem GetProduct(int prodNum)
      {
         // Verifying input is correct
         if (prodNum < 1 && prodNum > 3)
         {
            return null;
         }

         switch (prodNum)
         {
            case 1:
               return _product1;
            case 2:
               return _product2;
            case 3:
               return _product3;
            default:
               return null;
         }
      }
   }
}
