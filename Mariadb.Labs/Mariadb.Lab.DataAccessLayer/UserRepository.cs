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
        
        private MySqlConnection _conn;
        
        private UserRepository()
        {
            var connString = "Server=localhost;User ID=root;Password=pass.123;Database=LabMariabDB";
            _conn = new MySqlConnection(connString);
        }
       

        public async Task<UserEntity> GetUserById(int id)
        {
            await CheckConnectionStatus();

            // Retrieve all rows
            using (var cmd = new MySqlCommand("SELECT Id, Name, BalanceAmount, DateCreated, DateUpdated FROM User", _conn))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                await reader.ReadAsync();
                
                return new UserEntity()
                {
                    Id = reader.GetInt16(0),
                    Name = reader.GetString(1),
                    BalanceAmount = reader.GetDecimal(2),
                    DateCreated = reader.GetDateTime(3),
                    DateUpdated = reader.IsDBNull(4)?(DateTime?)null:reader.GetDateTime(4),
                };                
            }
        }

        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            var result = new List<UserEntity>();
            await CheckConnectionStatus();

            // Retrieve all rows
            using (var cmd = new MySqlCommand("SELECT Id, Name, BalanceAmount, DateCreated, DateUpdated FROM User", _conn))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                   result.Add(new UserEntity()
                   {
                       Id = reader.GetInt16(0),
                       Name = reader.GetString(1),
                       BalanceAmount = reader.GetDecimal(2),
                       DateCreated = reader.GetDateTime(3),
                       DateUpdated = reader.IsDBNull(4)?(DateTime?)null:reader.GetDateTime(4),
                   });
                }
            }

            return result;
        }

        public async Task CreateUser(UserDto inputObj)
        {
            await CheckConnectionStatus();
            
            // Insert some data
            using (var cmd = new MySqlCommand())
            {
                cmd.Connection = _conn;
                cmd.CommandText = "INSERT INTO User (Name) VALUES (@Name)";
                cmd.Parameters.AddWithValue("Name", inputObj.Name);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public Task UpdateUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        private async Task CheckConnectionStatus()
        {
                        
            if (_conn.State != ConnectionState.Open)
            {
                await _conn.OpenAsync();
            }

        }
    }
}