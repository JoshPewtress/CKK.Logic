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
      private ShoppingCartItem _product1;
      private ShoppingCartItem _product2;
      private ShoppingCartItem _product3;

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
         if (_product1 == null)
         {
            _product1.SetProduct(prod);
            _product1.SetQuantity(quantity);
            return _product1;
         }
         else if (_product2 == null)
         {
            _product2.SetProduct(prod);
            _product2.SetQuantity(quantity);
            return _product2;
         }
         else if (_product3 == null)
         {
            _product3.SetProduct(prod);
            _product3.SetQuantity(quantity);
            return _product3;
         }
         else
         {
            return null;
         }
      }

      public ShoppingCartItem AddProduct(Product prod)
      {
         if (_product1 == null)
         {
            _product1.SetProduct(prod);
            return _product1;
         }
         else if (_product2 == null)
         {
            _product2.SetProduct(prod);
            return _product2;
         }
         else if (_product3 == null)
         {
            _product3.SetProduct(prod);
            return _product3;
         }
         else
         {
            return null;
         }
      }

      public ShoppingCartItem RemoveProduct(Product prod, int quantity = 1)
      {
         ShoppingCartItem item = null;

         if (_product1 != null && _product1.GetProduct().GetId() == prod.GetId())
         {
            item = _product1;
         }
         else if (_product2 != null && _product2.GetProduct().GetId() == prod.GetId())
         {
            item = _product2;
         }
         else if (_product3 != null && _product3.GetProduct().GetId() == prod.GetId())
         {
            item = _product3;
         }

         if (item != null)
         {
            int currentQuantity = item.GetQuantity();
            if (currentQuantity <= quantity)
            {
               if (item == _product1)
               {
                  _product1 = null;
               }
               else if (item == _product2)
               {
                  _product2 = null;
               }
               else if (item == _product3)
               {
                  _product3 = null;
               }
            }
            else
            {
               item.SetQuantity(currentQuantity - quantity);
            }
         }

         return item;
      }

      public ShoppingCartItem GetProductById(int id)
      {
         if (_product1.GetProduct().GetId() == id)
         {
            return _product1;
         }
         else if (_product2.GetProduct().GetId() == id)
         {
            return _product2;
         }
         else if (_product3.GetProduct().GetId() == id)
         {
            return _product3;
         }
         else
         {
            return null;
         }
      }

      public decimal GetTotal()
      {
         decimal total = 0;

         if (_product1 != null)
         {
            total += _product1.GetTotal();
         }
         else if (_product2!= null)
         {
            total += _product2.GetTotal();
         }
         else if (_product3 != null)
         {
            total += _product3.GetTotal();
         }

         return total;
      }

      public ShoppingCartItem GetProduct(int productNum)
      {
         switch (productNum)
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
