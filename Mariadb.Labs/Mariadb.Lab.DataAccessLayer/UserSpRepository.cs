using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Mariadb.Lab.DataAccessLayer
{
    public class UserSpRepository
    {
        private static readonly Lazy<UserSpRepository> Lazy =
            new Lazy<UserSpRepository>(() => new UserSpRepository());

        public static UserSpRepository Instance
        {
            get { return Lazy.Value; }
        }

        private string _connStrinng;

        private UserSpRepository()
        {
            _connStrinng = "Server=localhost;User ID=root;Password=pass.123;Database=Test;";
        }

        public async Task AddNewUser(string name, MySqlConnection sharedConnection = null, MySqlTransaction sharedTransaction =null)
        {
            async Task ExecuteSp(MySqlConnection conn)
            {
                // Calling SP with return value
                using (var cmd = conn.CreateCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AddNewUser";
                    cmd.Parameters.AddWithValue("userName", name);
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

        public async Task<string> GetNewUser(MySqlConnection sharedConnection = null, MySqlTransaction sharedTransaction =null)
        {
            async Task<string> ExecuteSp(MySqlConnection conn)
            {
                // Calling SP with return value
                using (var cmd = conn.CreateCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetNewUser";
                    if (sharedTransaction != null)
                    {
                        cmd.Transaction = sharedTransaction;
                        
                    }

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        return reader.GetString(1);
                    }
                }
            }

            if (sharedConnection == null)
            {
                using (var conn = new MySqlConnection(_connStrinng))
                {
                    await conn.OpenAsync();
                    return await ExecuteSp(conn);
                }
            }

            return await ExecuteSp(sharedConnection);
        }
        
    }
}