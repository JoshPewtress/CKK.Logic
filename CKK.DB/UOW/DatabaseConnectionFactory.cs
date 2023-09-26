using CKK.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Configuration.Internal;

namespace CKK.DB.UOW
{
   public class DatabaseConnectionFactory : IConnectionFactory
   {
      public static string CnnVal(string name)
      {
         return ConfigurationManager.ConnectionStrings[name].ConnectionString;
      }

      private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=StucturedProjectDB;Integrated Security=True";

      public IDbConnection GetConnection
      {
         get
         {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            return conn;
         }
      }
   }
}
