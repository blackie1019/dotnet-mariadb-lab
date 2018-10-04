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
       Task UpdateUser(int id);
       Task DeleteUser(int id);
    }
}