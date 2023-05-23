using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
   public class Store : Entity
   {

      private List<StoreItem> Items = new List<StoreItem>();

      public StoreItem AddStoreItem(Product prod, int quantity)
      {
         // Checks for existing item
         foreach (var item in Items)
         {
            // If ID matches existing ID, update quantity
            if (item.Product.Id == prod.Id)
            {
               item.Quantity = (item.Quantity + quantity);
               return item;
            }
         }
         // Did not exist, Create new StoreItem, Add to List
         StoreItem newStoreItem = new StoreItem(prod, quantity);
         Items.Add(newStoreItem);

         return newStoreItem;
      }

      public StoreItem RemoveStoreItem(int id, int quantity)
      {
         // Check for matching StoreItem
         foreach (var item in Items)
         {
            if (item.Product.Id == id)
            {
               // Found matching Id, ensuring no negative quantity after change
               if (item.Quantity < quantity)
               {
                  item.Quantity = 0;
                  return item;
               }

               item.Quantity = item.Quantity - quantity;
               return item;
            }
         }

         // No matching item found
         return null;
      }

      public StoreItem FindStoreItemById(int id)
      {
         // Queries the _items list to a matching ID
         var storeItem =
            from item in Items
            where item.Product.Id.Equals(id)
            select item;

         // If match was found return it
         if (storeItem.Any())
         {
            return storeItem.FirstOrDefault();
         }

         return null;
      }

      public List<StoreItem> GetStoreItems()
      {
         return Items;
      }

   }
}
