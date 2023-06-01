using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;
using alibaba.Services.Models;

namespace alibaba.interfaces
{
    public interface IUserRepo
    {
        Task<DbUser> GetUserById(int userId);
        Task<IEnumerable<string>> GetTokensByType(int type);
        Task<DbUser> GetUserByFirebaseId(string firebaseId);
        Task<int> PostUser(DbUser user);
        Task<int> PostUserAddress(DbUserAddress address);
        Task<bool> UpdateUser(DbUser user);
        Task<string> DeleteUser(int id);
        Task<bool> SetUserStatus(DbUserStatus dbUserStatus);
        Task<IEnumerable<UserAddress>> GetUserAddresses(int userId);
        Task<string> DeleteUserAddresses(int addressId);


        Task<bool> SetUserActiveStatus(DbUserActiveStatus dbUserStatus);
        Task<IEnumerable<DbUser>> FilterUsersList(DbUserCriteria criteria);
    }
}
