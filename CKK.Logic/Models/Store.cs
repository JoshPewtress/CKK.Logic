using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Store
    {
      private int _id;
      private string _name;

      private List<StoreItem> _items = new List<StoreItem>();

      public int GetId()
      {
         return _id;
      }

      public void SetId(int id)
      {
         _id = id;
      }

      public string GetName()
      {
         return _name;
      }

      public void SetName(string name)
      {
         _name = name;
      }

      public StoreItem AddStoreItem(Product prod, int quantity)
      {
         // Checks for existing item
         foreach (var item in _items)
         {
            // If ID matches existing ID, update quantity
            if (item.GetProduct().GetId() == prod.GetId())
            {
               item.SetQuantity(item.GetQuantity() + quantity);
               return item;
            }
         }

         // Did not exist, Create new StoreItem, Add to List
         StoreItem newStoreItem = new StoreItem(prod, quantity);
         _items.Add(newStoreItem);

         return newStoreItem;
      }
      
      public StoreItem RemoveStoreItem(int id, int quantity)
      {
         // Check for matching StoreItem
         foreach (var item in _items)
         {
            if (item.GetProduct().GetId() == id)
            {
               // Found matching Id, ensuring no negative quantity after change
               if (item.GetQuantity() < quantity)
               {
                  item.SetQuantity(0);
                  return item;
               }

               item.SetQuantity(item.GetQuantity() - quantity);
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
            from item in _items
            where item.GetProduct().GetId().Equals(id)
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
         return _items;
      }

   }
}
