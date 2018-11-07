using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Mariadb.Lab.DataAccessLayer
{
    public class ProductSpRepository
    {
        private static readonly Lazy<ProductSpRepository> Lazy =
            new Lazy<ProductSpRepository>(() => new ProductSpRepository());

        public static ProductSpRepository Instance
        {
            get { return Lazy.Value; }
        }

        private string _connStrinng;

        private ProductSpRepository()
        {
            _connStrinng = "Server=localhost;User ID=root;Password=pass.123;Database=Test;";
        }

        public async Task AddNewProduct(string name, MySqlConnection sharedConnection = null, MySqlTransaction sharedTransaction =null)
        {
            async Task ExecuteSp(MySqlConnection conn)
            {
                // Calling SP with return value
                using (var cmd = conn.CreateCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AddNewProduct";
                    cmd.Parameters.AddWithValue("productName", name);
                    if (sharedTransaction != null)
                    {
                        cmd.Transaction = sharedTransaction;
                    }
                    await cmd.ExecuteNonQueryAsync();
                }
            }

            
            if (sharedConnection == null)
            {
                using (var conn = new MySqlConnection(_connStrinng))
                {
                    await conn.OpenAsync();
                    await ExecuteSp(conn);
                }
            }
            else
            {

                await ExecuteSp(sharedConnection);
            }
        }
    }
}