using System.Collections.Generic;
using System.Threading.Tasks;
using Mariadb.Lab.DataAccessLayer.DataTransferObjects;
using Mariadb.Lab.DataAccessLayer.Entities;

namespace Mariadb.Lab.DataAccessLayer
{
    public interface IUserRepository
    {
       Task<IEnumerable<UserEntity>> GetUsers();
       Task<UserEntity> GetUserById(int id);
       Task CreateUser(UserDto inputObj);
       Task UpdateUser(UserDto inputObj);
       Task DeleteUser(int id);
       Task<int> GetUserCountBySPWithOutputValue();
       Task<int> GetUserCountBySPWithReturnValue();
    }
}