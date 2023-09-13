using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;

namespace CKK.DB.Repository
{
   public class ShoppingCartRepository : IShoppingCartRepository
   {
      private readonly IConnectionFactory _connectionFactory;

      public ShoppingCartRepository(IConnectionFactory Conn)
      {
         _connectionFactory = Conn;
      }

      public int Add(ShoppingCartItem entity)
      {
         var sql = "INSERT INTO ShoppingCartItems (ShoppingCartId, ProductId, Quantity) " +
                   "VALUES (@ShoppingCartId, @ProductId, @Quantity)";

         using (var connection = _connectionFactory.GetConnection)
         {
            connection.Open();
            var result = connection.Execute(sql, entity);
            return result;
         }
      }

      public ShoppingCartItem AddToCart(int ShoppingCartId, int ProductId, int quantity)
      {
         var cartCheck = "SELECT * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";

         using (var connection = _connectionFactory.GetConnection)
         {
            connection.Open();
            var existingShoppingCart = connection.QuerySingleOrDefault<ShoppingCartItem>(cartCheck, new { ShoppingCartId = ShoppingCartId });

            if (existingShoppingCart != null)
            {
               
            }
         }
      }

      public int ClearCart(int ShoppingCartId)
      {
         throw new NotImplementedException();
      }

      public List<ShoppingCartItem> GetProducts(int ShoppingCartId)
      {
         throw new NotImplementedException();
      }

      public decimal GetTotal(int ShoppingCartId)
      {
         throw new NotImplementedException();
      }

      public void Ordered(int ShoppingCartId)
      {
         throw new NotImplementedException();
      }

      public int Update(ShoppingCartItem entity)
      {
         throw new NotImplementedException();
      }
   }
}
