using CKK.Logic.Interfaces;
using System;

namespace CKK.Logic.Models
{
   [Serializable]
   public class Product
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public decimal Price { get; set; }
      public int Quantity { get; set; }
   }
}
