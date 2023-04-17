using Dapper;
using MySql.Data.MySqlClient;
using alibaba.Common;
using alibaba.Data;
using alibaba.interfaces;
using alibaba.Common;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace alibaba.Repos
{
    public class UserRepo : IUserRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<UserRepo> _logger;
        public UserRepo(IDbSettings dbSettings, ILogger<UserRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        public async Task<int> PostUser(DbUser user)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);
            var userId = 0;
            var parameters = new DynamicParameters();

            parameters.Add("@id", user.Id);
            parameters.Add("@username", user.UserName);
            parameters.Add("@email", user.Email);
            parameters.Add("@phone", user.PhoneNumber);
            parameters.Add("@address", user.Address);
            parameters.Add("@location", user.Location);
            parameters.Add("@firebaseid", user.FirebaseId);
            parameters.Add("@firebaseToken", user.FirebaseToken);
            parameters.Add("@image", user.Image);
            parameters.Add("@type", user.UserType);
            parameters.Add("@emailVerified", user.EmailVerified);
            parameters.Add("@phoneVerified", user.PhoneVerified);
            try
            {
                userId = await sqlQuery.GetQuery(@"SELECT `AUTO_INCREMENT` FROM INFORMATION_SCHEMA.TABLES WHERE 
                                                TABLE_SCHEMA = 'qeiapmmy_AllBaba_store' AND
                                                TABLE_NAME = 'users'  ", parameters) - 1;

            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
            try
            {
                var res = await sqlQuery.PostQuery(@"insert into users (firebaseId,usertype, address, location,image, username,email ,phonenumber, firebaseToken) VALUES 
                                                    (@firebaseid,@type,@address,@location, @image, @username,@email, @phone , @firebaseToken) ",


               parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }


            return userId+1;
        }



        public async Task<string> DeleteUser(int userId)
        {
            SqlORM<string> sql = new SqlORM<string>(_dbSettings);
            var result = "";

            var parameters = new DynamicParameters();
            parameters.Add("@userId", userId);

            try
            {
                await sql.PostQuery(@"delete FROM users where Id = @userId ", parameters);
            }
          
            catch (Exception ex)
            {
                return (ex.Message);
            }
            return "success";
        }



        public async Task<DbUser> GetUserById(int userId)
        {
            SqlORM<DbUser> sql = new SqlORM<DbUser>(_dbSettings);
            DbUser result = null;

            var parameters = new DynamicParameters();
            parameters.Add("@userId", userId);

            try
            {
                result = await sql.GetQuery(@"SELECT * FROM users where Id = @userId ", parameters);
            }
            catch (MySqlException ex)
            {

                if (ex.Message.Equals("Sequence contains no elements"))
                {
                    var n = ex.Number;
                    result = null;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                {

                    result = null;
                }
            }
            return result;
        }
        
        public async Task<DbUser> GetUserByFirebaseId(string firebaseId)
        {
            SqlORM<DbUser> sql = new SqlORM<DbUser>(_dbSettings);
            DbUser result = null;

            var parameters = new DynamicParameters();
            parameters.Add("@userId", firebaseId);

            try
            {
                result = await sql.GetQuery(@"SELECT * FROM users where firebaseid = @userId ", parameters);
            }
            catch (MySqlException ex)
            {

                if (ex.Message.Equals("Sequence contains no elements"))
                {
                    var n = ex.Number;
                    result = null;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                {

                    result = null;
                }
            }
            return result;
        }
        
        public async Task<DbUser> GetUserByfirebaseId(int userId)
        {
            SqlORM<DbUser> sql = new SqlORM<DbUser>(_dbSettings);
            DbUser result = null;

            var parameters = new DynamicParameters();
            parameters.Add("@uid", userId);

            try
            {
                result = await sql.GetQuery(@"SELECT * FROM users where firebaseid = @userId ", parameters);
            }
            catch (MySqlException ex)
            {

                if (ex.Message.Equals("Sequence contains no elements"))
                {
                    var n = ex.Number;
                    result = null;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                {

                    result = null;
                }
            }
            return result;
        }


        public async Task<bool> UpdateUser(DbUser user)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


            var parameters = new DynamicParameters();
            parameters.Add("@id", user.Id);
            parameters.Add("@username", user.UserName);
            parameters.Add("@email", user.Email);
            parameters.Add("@phone", user.PhoneNumber);
            parameters.Add("@address", user.Address);
            parameters.Add("@rating", user.Rating);
            parameters.Add("@location", user.Location);
            parameters.Add("@firebaseid", user.FirebaseId);
            parameters.Add("@firebaseToken", user.FirebaseToken);
            parameters.Add("@image", user.Image);
            parameters.Add("@type", user.UserType);
            parameters.Add("@emailVerified", user.EmailVerified);
            parameters.Add("@phoneVerified", user.PhoneVerified);


            try
            {
                var res = await sqlQuery.PostQuery(@"UPDATE users SET emailverified = @emailVerified, phoneVerified = @phoneVerified,
                                                             firebaseid =@firebaseid,rating = @rating, email=@email,image= @image, usertype = @type,
                                                            username= @username, address =@address , location = @location,
                                                            phonenumber=@phone,  firebasetoken=@firebasetoken 
                                                            WHERE  id = @id;", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }




            return true;
        }

        public async Task<IEnumerable<DbUser>> FilterUsersList(DbUserCriteria criteria)
        {

            string searchQuery = "";
            SqlORM<DbUser> sqlQuery = new SqlORM<DbUser>(_dbSettings);
            var parameters = new DynamicParameters();
            searchQuery = $"WHERE username like '%{criteria.Name}%'";
            if (!string.IsNullOrEmpty(criteria.Name))
            {
                parameters.Add("@name", criteria.Name);
                searchQuery += $"and  username like '%{criteria.Name}%'";
            }
            if (criteria.IsOnlyActive)
            {
                parameters.Add("@name", criteria.Name);
                searchQuery += $"and  isActive = 1";
            }
            if (!string.IsNullOrEmpty(criteria.PhoneNumber))
            {
                parameters.Add("@pho", criteria.Name);
                searchQuery += $"and  phonenumber='{criteria.PhoneNumber}'";
            }
            if (!string.IsNullOrEmpty(criteria.Email))
            {
                parameters.Add("@name", criteria.Name);
                searchQuery += $" and  email='{criteria.Email}' ";
            }
            if (criteria.Type!=0)
            {
                parameters.Add("@type", criteria.Type);
                searchQuery += $" and  usertype='{criteria.Type}'";
            }
            return await sqlQuery.GetListQuery($@"SELECT  * FROM users {searchQuery} ", parameters);
        }
        public async Task<bool> SetUserStatus(DbUserStatus dbUserStatus)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


            var parameters = new DynamicParameters();
            parameters.Add("@id", dbUserStatus.Id);

            parameters.Add("@isactive", dbUserStatus.IsActive? 1: 0);
            parameters.Add("@status", dbUserStatus.Status);


            try
            {
                var res = await sqlQuery.PostQuery(@"UPDATE users SET isactive =@isactive , status = @status
                                                        
                                                            WHERE  id = @id;", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }




            return true;
        }
        public async Task<bool> SetUserActiveStatus(DbUserActiveStatus dbUserStatus)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


            var parameters = new DynamicParameters();
            parameters.Add("@number", dbUserStatus.PhoneNumber);

            parameters.Add("@isactive", dbUserStatus.IsActive? 1: 0);


            try
            {
                var res = await sqlQuery.PostQuery(@"UPDATE users SET isactive =@isactive
                                                        
                                                            WHERE  phonenumber = @number;", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }




            return true;
        }
    }
}
