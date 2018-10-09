using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Mariadb.Lab.DataAccessLayer.DataTransferObjects;
using Mariadb.Lab.DataAccessLayer.Entities;
using MySql.Data.MySqlClient;

namespace Mariadb.Lab.DataAccessLayer
{
    public class UserWithDapperRepository : IUserRepository
    {
        private static readonly Lazy<UserWithDapperRepository> Lazy =
            new Lazy<UserWithDapperRepository>(() => new UserWithDapperRepository());

        public static UserWithDapperRepository Instance
        {
            get { return Lazy.Value; }
        }
        private string _connStrinng;

        private UserWithDapperRepository()
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

        public Task<IEnumerable<UserEntity>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task CreateUser(UserDto inputObj)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(UserDto inputObj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUserCountBySPWithOutputValue()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUserCountBySPWithReturnValue()
        {
            throw new NotImplementedException();
        }
    }
}