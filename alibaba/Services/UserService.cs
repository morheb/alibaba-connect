

namespace alibaba
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using MySql.Data;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using System.Threading.Tasks;
    using alibaba.Data;
    using alibaba.interfaces;
    using alibaba.Services.Models;

    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _uRepo;

        public UserService(IUserRepo uRepo, IMapper mapper)
        {
            _uRepo = uRepo;
            _mapper = mapper;

        }
        public async Task<User> GetUserById(int id)
        {

            var res = await _uRepo.GetUserById(id);
            var user = _mapper.Map<User>(res);
            return user;
        }
        public async Task<IEnumerable<string>> GetTokensByType(int type)
        {

            var res = await _uRepo.GetTokensByType(type);
         
            return res;
        }
        public async Task<User> GetUserByFirebaseId(string id)
        {

            var res = await _uRepo.GetUserByFirebaseId(id);
            var user = _mapper.Map<User>(res);
            return user;
        }

        public async Task<int> PostUser(User user)
        {

            DbUser dbUser = _mapper.Map<DbUser>(user);
            var res = await _uRepo.PostUser(dbUser);

            return res;
        }
        
        public async Task<int> PostUserAddress(UserAddress user)
        {

            var dbUser = _mapper.Map<DbUserAddress>(user);
            var res = await _uRepo.PostUserAddress(dbUser);

            return res;
        }

        public async Task<bool> UpdateUser(User user)
        {
            DbUser userDb = _mapper.Map<DbUser>(user);
            var res = await _uRepo.UpdateUser(userDb);

            return res;
        }
        public async Task<bool> SetUserStatus(UserStatus status)
        {
            DbUserStatus statusDb = _mapper.Map<DbUserStatus>(status);
            var res = await _uRepo.SetUserStatus(statusDb);

            return res;
        }
        
        public async Task<string> SetUserActiveStatus(UserActiveStatus status)
        {
            try
            {
                DbUserActiveStatus statusDb = _mapper.Map<DbUserActiveStatus>(status);
                var res = await _uRepo.SetUserActiveStatus(statusDb);
            }
            catch
            {
                return "wrong number";
            }


            return "success";
        }
        
        public async Task<IEnumerable<UserAddress>> GetUserAddresses(int userId)
        {
            var res = await _uRepo.GetUserAddresses(userId);
            return res ;
        }

         public async Task<string> DeleteUserAddresses(int userId, int addressId)
        {
            var res = await _uRepo.DeleteUserAddresses(userId, addressId);
            return res;
        }


        public async Task<IEnumerable<User>> FilterUsers(UserCriteria criteria)
        {
            DbUserCriteria dbCriteria = _mapper.Map<DbUserCriteria>(criteria);
            var res = await _uRepo.FilterUsersList(dbCriteria);
            IEnumerable<User> users = _mapper.Map<IEnumerable<User>>(res);

            return users;
        }
        
        public async Task<string> DeleteUser(int id)
        {
            var res = await _uRepo.DeleteUser(id);

            return res;
        }

    }
}
