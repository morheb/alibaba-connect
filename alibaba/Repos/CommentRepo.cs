using Dapper;
using MySql.Data.MySqlClient;
using alibaba.Common;
using alibaba.Data;
using alibaba.interfaces;
using alibaba.Common;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using alibaba.Services.Models;
using System.Collections.Generic;

namespace alibaba.Repos
{
    public class CommentRepo : ICommentRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<CommentRepo> _logger;
        public CommentRepo(IDbSettings dbSettings, ILogger<CommentRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        public async Task<bool> PostCommentAsync(DbComment com)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            var parameters = new DynamicParameters();


            parameters.Add("@username", com.UserName);
            parameters.Add("@targetId", com.TargetId);
            parameters.Add("@content", com.Content);
            parameters.Add("@targetType", com.Type);
            parameters.Add("@targetName", com.TargetName);


            try
            {
                var res = await sqlQuery.PostQuery(@"insert into comments (username, type, content,targetId, targetName) VALUES
                                                                               (@username,@targetType, @content, @targetId, @targetName) ",
               parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }


            return true;
        }






        public async Task<IEnumerable<DbComment>> FilterComments(DbCommentCriteria criteria)
        {
            SqlORM<DbComment> sql = new SqlORM<DbComment>(_dbSettings);
            var parameters = new DynamicParameters();
            string query = " ";
            if(criteria.TargetId != 0)
            {
                query = $"where type  = {criteria.TargetType} and targetId = {criteria.TargetId}";

            }
            try
            {
                return await sql.GetListQuery($@"SELECT userName,content, date, id , targetId, targetName, type from comments
                                                {query} LIMIT {criteria.PageSize * (criteria.PageNumber - 1)},{criteria.PageNumber * criteria.PageSize}

                                                ", parameters);
            }
            catch(Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        public async Task<string> DeleteComment(int comId)
        {
            SqlORM<string> sql = new SqlORM<string>(_dbSettings);
            var result = "";

            var parameters = new DynamicParameters();
            parameters.Add("@comId", comId);

            try
            {
                await sql.PostQuery(@"delete FROM comments where Id = @comId ", parameters);
            }

            catch (Exception ex)
            {
                return (ex.Message);
            }
            return "success";
        }


    }
}
