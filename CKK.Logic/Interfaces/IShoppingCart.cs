﻿using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Interfaces
{
   public interface IShoppingCart
   {
      ShoppingCartItem AddProduct(Product prod, int quantity);

      ShoppingCartItem RemoveProduct(int id, int quantity);

      decimal GetTotal();

      ShoppingCartItem GetProductById(int id);
   }
}
