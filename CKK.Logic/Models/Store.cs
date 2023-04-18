﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    internal class Store
    {
      private int _id;
      private string _name;
      private Product _product1;
      private Product _product2;
      private Product _product3;

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

      public void AddStoreItem(Product product)
      {
         // Assigning product to first available slot
         if (_product1 == null)
         {
            _product1 = product;
         }
         else if (_product2 == null) {
            _product2 = product;
         }
         else if (_product3 == null)
         {
            _product3 = product;
         }
      }

      public void RemoveStoreItem(int productNum) 
      {
         if (productNum == 1)
         {
            _product1 = null;
         }
         else if (productNum == 2)
         {
            _product2 = null;
         }
         else if (productNum == 3)
         {
            _product3 = null;
         }
         else
         {
            Console.WriteLine("Error. Invalid input detected.");
         }
      }

      public Product GetStoreItem(int productNum)
      {
         if (productNum == 1)
         {
            return _product1;
         }
         else if ( productNum == 2)
         {
            return _product2;
         }
         else if (productNum == 3) 
         {
            return _product3;
         }
         else
         {
            return null;
         }
      }

      public Product FindStoreItemById(int id)
      {
         if (_product1.GetId() == id)
         {
            return _product1;
         }
         else if (_product2.GetId() == id)
         {
            return _product1;
         }
         else if (_product3.GetId() == id)
         {
            return _product1;
         }
         else
         {
            return null;
         }
      }
    }
}
