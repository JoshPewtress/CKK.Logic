using CKK.DB.Interfaces;
using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using Dapper;
using static Dapper.SqlMapper;

namespace CKK.DB.Repository
{
   public class OrderRepository : IOrderRepository
   {
      private readonly IConnectionFactory _connectionFactory;

      public OrderRepository(IConnectionFactory Conn)
      {
         _connectionFactory = Conn;
      }

      public int Add(Order entity)
      {
         var sql = "INSERT INTO Orders (OrderNumber,CustomerId,ShoppingCartId) VALUES (@OrderNumber,@CustomerId,@ShoppingCartId)";

         using (var connection = _connectionFactory.GetConnection)
         {
            connection.Open();
            var result = connection.Execute(sql, entity);
            return result;
         }
      }

      public int Delete(int id)
      {
         var sql = "DELETE FROM Orders WHERE OrderId = @OrderId";

         using (var connection = _connectionFactory.GetConnection)
         {
            connection.Open();
            var result = connection.Execute(sql, new {OrderId = id});
            return result;
         }
      }

      public List<Order> GetAll()
      {
         var sql = "SELECT * FROM Orders";

         using (var connection = _connectionFactory.GetConnection)
         {
            connection.Open();
            var result = connection.Query<Order>(sql).ToList();
            return result;
         }
      }

      public Order GetById(int id)
      {
         var sql = "SELECT * FROM Orders WHERE OrderId = @OrderId";

         using (var connection = _connectionFactory.GetConnection)
         {
            connection.Open();
            var result = connection.QuerySingleOrDefault<Order>(sql, new { OrderId = id });
            return result;
         }
      }

      public Order GetOrderByCustomerId(int id)
      {
         var sql = "SELECT * FROM Orders WHERE CustomerId = @CustomerId";

         using (var connection = _connectionFactory.GetConnection)
         {
            connection.Open();
            var result = connection.QuerySingleOrDefault<Order>(sql, new { CustomerId = id });
            return result;
         }
      }

      public int Update(Order entity)
      {
         var sql = "UPDATE Orders Set OrderNumber = @OrderNumber, CustomerId = @CustomerId, ShoppingCartId = @ShoppingCartId";

         using (var connection = _connectionFactory.GetConnection)
         {
            connection.Open();
            var result = connection.Execute(sql, entity);
            return result;
         }
      }
   }
}
