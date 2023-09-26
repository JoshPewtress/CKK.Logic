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
            var result = connection.Execute(sql, entity);
            return result;
         }
      }

      public ShoppingCartItem AddToCart(int ShoppingCartId, int ProductId, int quantity)
      {
         using (var conn = _connectionFactory.GetConnection)
         {
            ProductRepository _productRepository = new ProductRepository(_connectionFactory);
            var item = _productRepository.GetById(ProductId);
            var productItems = GetProducts(ShoppingCartId).Find(x => x.ProductId == ProductId);

            var shopItem = new ShoppingCartItem()
            {
               ShoppingCartId = ShoppingCartId,
               ProductId = ProductId,
               Quantity = quantity
            };

            if (item.Quantity >= quantity)
            {
               if (productItems != null)
               {
                  // Product already in cart so update quantity
                  var test = Update(shopItem);
               }
               else
               {
                  // New product for the cart so add it
                  var test = Add(shopItem);
               }
            }

            return shopItem;
         }
      }

      public int ClearCart(int ShoppingCartId)
      {
         var sql = "DELETE FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";

         using (var conn = _connectionFactory.GetConnection)
         {
            var result = conn.Execute(sql, new { ShoppingCartId = ShoppingCartId });
            return result;
         }
      }

      public List<ShoppingCartItem> GetProducts(int ShoppingCartId)
      {
         var sql = "SELECT * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";

         using (var conn = _connectionFactory.GetConnection)
         {
            return conn.Query<ShoppingCartItem>(sql, new { ShoppingCartId = ShoppingCartId }).ToList();
         }
      }

      public decimal GetTotal(int ShoppingCartId)
      {
         using (var conn = _connectionFactory.GetConnection)
         {
            var shoppingCarts = GetProducts(ShoppingCartId);
            decimal total = 0;

            foreach (var item in shoppingCarts)
            {
               total += item.GetTotal();
            }

            return total;
         }
      }

      public void Ordered(int ShoppingCartId)
      {
         var sql = "DELETE * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";

         using (var conn = _connectionFactory.GetConnection)
         {
            conn.Execute(sql, new { ShoppingCartId = ShoppingCartId });
         }
      }

      public int Update(ShoppingCartItem entity)
      {
         var sql = "UPDATE ShoppingCartItems SET ShoppingCartId = @ShoppingCartId, ProductId = @ProductId, Quantity = @Quantity";

         using (var conn = _connectionFactory.GetConnection)
         {
            var result = conn.Execute(sql, entity);
            return result;
         }
      }
   }
}
