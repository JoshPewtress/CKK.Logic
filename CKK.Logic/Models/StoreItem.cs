using CKK.Logic.Interfaces;
using System;

namespace CKK.Logic.Models
{
   [Serializable]
   public class StoreItem : InventoryItem
   {

      public StoreItem(Product Product, int Quantity) : base(Product, Quantity) { }

   }
}
