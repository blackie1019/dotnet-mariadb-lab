using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Mariadb.Lab.DataAccessLayer.DataTransferObjects;
using Mariadb.Lab.DataAccessLayer.Entities;
using MySql.Data.MySqlClient;

namespace Mariadb.Lab.DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        private static readonly Lazy<UserRepository> Lazy =
            new Lazy<UserRepository>(() => new UserRepository());

        public static UserRepository Instance
        {
            get { return Lazy.Value; }
        }
        private string _connStrinng;

        private UserRepository()
        {
            _connStrinng = "Server=localhost;User ID=root;Password=pass.123;Database=LabMariabDB";
        }
       

        public async Task<UserEntity> GetUserById(int id)
        {
            using (var conn = new MySqlConnection(_connStrinng))
            {
                await conn.OpenAsync();
                
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "SELECT Id, Name, BalanceAmount, DateCreated, DateUpdated FROM User WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("Id", id);

                    var reader = await cmd.ExecuteReaderAsync();

                    // Retrieve first rows
                    await reader.ReadAsync();

                    return new UserEntity()
                    {
                        Id = reader.GetInt16(0),
                        Name = reader.GetString(1),
                        BalanceAmount = reader.GetDecimal(2),
                        DateCreated = reader.GetDateTime(3),
                        DateUpdated = reader.IsDBNull(4) ? (DateTime?) null : reader.GetDateTime(4),
                    };
                }
            }
        }

        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            var result = new List<UserEntity>();
            using (var conn = new MySqlConnection(_connStrinng))
            {
                await conn.OpenAsync();
                
                using (var cmd = new MySqlCommand("SELECT Id, Name, BalanceAmount, DateCreated, DateUpdated FROM User",
                    conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    // Retrieve all rows
                    while (await reader.ReadAsync())
                    {
                        result.Add(new UserEntity()
                        {
                            Id = reader.GetInt16(0),
                            Name = reader.GetString(1),
                            BalanceAmount = reader.GetDecimal(2),
                            DateCreated = reader.GetDateTime(3),
                            DateUpdated = reader.IsDBNull(4) ? (DateTime?) null : reader.GetDateTime(4),
                        });
                    }
                }
            }

            return result;
        }

        public async Task CreateUser(UserDto inputObj)
        {
            using (var conn = new MySqlConnection(_connStrinng))
            {
                await conn.OpenAsync();
                // Insert some data
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO User (Name) VALUES (@Name)";
                    cmd.Parameters.AddWithValue("Name", inputObj.Name);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateUser(UserDto inputObj)
        {
            using (var conn = new MySqlConnection(_connStrinng))
            {
                await conn.OpenAsync();
                
                // Insert some data
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "UPDATE User SET Name = @Name, BalanceAmount = @BalanceAmount, DateUpdated = NOW() WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("Name", inputObj.Name);
                    cmd.Parameters.AddWithValue("BalanceAmount", inputObj.BalanceAmount);
                    cmd.Parameters.AddWithValue("Id", inputObj.Id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task DeleteUser(int id)
        {
            using (var conn = new MySqlConnection(_connStrinng))
            {
                await conn.OpenAsync();
                
                // Insert some data
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "DELETE FROM User WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}