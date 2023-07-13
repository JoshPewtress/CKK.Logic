using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;
using System.Threading;

namespace CKK.Logic.Models
{
   public class Store : Entity, IStore
   {

      private List<StoreItem> Items = new List<StoreItem>();
      private static int uniqueIdCounter = 0;

      public StoreItem AddStoreItem(Product prod, int quantity)
      {
         if (quantity <= 0)
         {
            throw new InventoryItemStockTooLowException("Quantity cannot be negative or 0.");
         }

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

         if (prod.Id == 0)
         {
            prod.Id = GenerateUniqueId();
         }

         // Did not exist, Create new StoreItem, Add to List
         StoreItem newStoreItem = new StoreItem(prod, quantity);
         Items.Add(newStoreItem);

         return newStoreItem;
      }

      private int GenerateUniqueId()
      {
         return Interlocked.Increment(ref uniqueIdCounter);
      }

      public StoreItem RemoveStoreItem(int id, int quantity)
      {
         if (quantity < 0)
         {
            throw new ArgumentOutOfRangeException("Quantity cannot be a negative number");
         }

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
         throw new ProductDoesNotExistException("Product does not exist.");
      }

      public StoreItem FindStoreItemById(int id)
      {
         if (id < 0)
         {
            throw new InvalidIdException("Id cannot be less than 0");
         }

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

      // Checks Items list for a matching ID and removes the item from the store
      public StoreItem DeleteStoreItem(int id)
      {
         foreach (var item in Items)
         {
            if (item.Product.Id == id)
            {
               Items.Remove(item);
            }
         }

         return null;
      }

   }
}
