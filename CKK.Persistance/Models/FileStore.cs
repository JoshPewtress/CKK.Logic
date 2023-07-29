using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic;
using CKK.Logic.Interfaces;
using CKK.Persistance.Interfaces;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CKK.Logic.Models;
using CKK.Logic.Exceptions;

namespace CKK.Persistance.Models
{
   public class FileStore : IStore, ISavable, ILoadable
   {
      private List<StoreItem> Items = new List<StoreItem>();
      public string FilePath { get; }
      private int IdCounter = 0;

      public FileStore()
      {
         // Call to CreatePath method
         CreatePath();

         // Set the filePath after ensuring the folder exists
         FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar +
         "Persistance" + Path.DirectorySeparatorChar + "StoreItems.dat";
      }

      public StoreItem AddStoreItem(Product prod, int quantity)
      {
         if (quantity <= 0)
         {
            throw new InventoryItemStockTooLowException("Quantity cannot be negative or 0.");
         }

         // Checks for existing item
         foreach (var item in Items)
         {
            // If ID Matches existing ID, update quantity
            if (item.Product.Id == prod.Id)
            {
               item.Quantity += quantity;
               Save(); // Calls save after modifying data
               return item;
            }
         }

         // If Product ID is 0 give it a unique ID
         if (prod.Id == 0)
         {
            prod.Id = GenerateUniqueId();
         }

         // Did not exist, Create new StoreItem and add it to the List
         StoreItem newStoreItem = new StoreItem(prod, quantity);
         Items.Add(newStoreItem);
         Save(); // Calls save after modifying data
         return newStoreItem;
      }

      private int GenerateUniqueId()
      {
         return ++IdCounter;
      }

      public StoreItem RemoveStoreItem(int id, int quantity)
      {
         if (quantity < 0)
         {
            throw new ArgumentOutOfRangeException("Quantity cannot be a negative number.");
         }

         // Check for matching StoreItem
         foreach (var item in Items)
         {
            if (item.Product.Id == id)
            {
               // Found matching ID, ensuring no negative quantity after change
               if (item.Quantity < quantity)
               {
                  item.Quantity = 0;
               }
               else
               {
                  item.Quantity -= quantity;
               }

               Save(); // Calls Save after modifying data
               return item;
            }
         }

         // No Matching item found
         throw new ProductDoesNotExistException("Product does not exist.");
      }

      public StoreItem FindStoreItemById(int id)
      {
         if (id < 0)
         {
            throw new InvalidIdException();
         }

         // Queries the Items list to find a matchi8ng ID
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
      
      public StoreItem DeleteStoreItem(int id)
      {
         StoreItem foundItem = null;
         foreach (var item in Items)
         {
            if (item.Product.Id == id)
            {
               foundItem = item;
               break;
            }
         }

         if (foundItem != null)
         {
            Items.Remove(foundItem);
            Save(); // Call to Save after modifying data
         }

         return foundItem;
      }

      /*
       * Load method reads the file and casts it back to a List<StoreItem>
       */
      public void Load()
      {
         // Verify FilePath exists
         if (File.Exists(FilePath))
         {
            // Create FileStream object to read the serialized data
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
               // BinaryFormatter object
               BinaryFormatter binaryFormatter = new BinaryFormatter();

               // Deserialize the file and cast it back to a List<StoreItem>
               Items = (List<StoreItem>)(binaryFormatter.Deserialize(fileStream));
            }
         }
      }

      /*
       * Save method writes the List<> Items to the Persistance File
       */
      public void Save()
      {
         // FileStream object to be able to write data to the file
         using (FileStream fileStream = new FileStream(FilePath, FileMode.Create))
         {
            // BinaryFormatter object
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // Serialize the Items list and write it to the file stream
            binaryFormatter.Serialize(fileStream, Items);
         }
      }

      /*
       * CreatePath method verifies the Persistance folder exists in the correct location
       * and creates it if it does not.
       */
      private void CreatePath()
      {
         // Store location of Persistance folder
         string persistanceFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            Path.DirectorySeparatorChar + "Persistance";

         // Check to see if Persistance folder exists, create it if not
         if (!Directory.Exists(persistanceFolderPath))
         {
            Directory.CreateDirectory(persistanceFolderPath);
         }
      }

   }
}
