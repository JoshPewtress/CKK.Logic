using CKK.Logic.Interfaces;
using System;

namespace CKK.Logic.Models
{
   [Serializable]
   public class Product : Entity
   {
      private decimal price;

      public decimal Price
      {
         get
         {
            return price;
         }
         set
         {
            if (value < 0)
            {
               throw new ArgumentOutOfRangeException("Price cannot be a negative number."); 
            }
            price = value;
         }
      }
   }
}
