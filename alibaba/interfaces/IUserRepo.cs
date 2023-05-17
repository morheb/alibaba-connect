using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface IUserRepo
    {
        Task<DbUser> GetUserById(int userId);
        Task<IEnumerable<string>> GetTokensByType(int type);
        Task<DbUser> GetUserByFirebaseId(string firebaseId);
        Task<int> PostUser(DbUser user);
        Task<bool> UpdateUser(DbUser user);
        Task<string> DeleteUser(int id);
        Task<bool> SetUserStatus(DbUserStatus dbUserStatus);
        Task<bool> SetUserActiveStatus(DbUserActiveStatus dbUserStatus);
        Task<IEnumerable<DbUser>> FilterUsersList(DbUserCriteria criteria);
    }
}
