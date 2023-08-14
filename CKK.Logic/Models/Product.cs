using CKK.Logic.Interfaces;
using System;

namespace CKK.Logic.Models
{
   [Serializable]
   public class Product : Entity
   {
      public decimal price { get; set; }
   }
}
