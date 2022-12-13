

namespace alibaba
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using MySql.Data;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using System.Threading.Tasks;
    using alibaba.Data;
    using alibaba.interfaces;
    using alibaba.Services.Models;

    public class CommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepo _cRepo;

        public CommentService(ICommentRepo cRepo, IMapper mapper)
        {
            _cRepo = cRepo;
            _mapper = mapper;

        }
        
        public async Task<bool> PostComment(Comment comment)
        {
            DbComment dbRest;
            var dbComment = _mapper.Map<DbComment>(comment);
    
            //var dbComment = this.ToDbComment(comment);
            var res = await _cRepo.PostCommentAsync(dbComment);
          
            return res;
        }

    
        public async Task<string> DeleteComment(int id)
        {
            var res = await _cRepo.DeleteComment(id);

            return res;
        }


        public async Task<IEnumerable<Comment>> FilterComment(CommentCriteria criteria)
        {
            DbCommentCriteria dbCriteria = _mapper.Map<DbCommentCriteria>(criteria);

            var res = await _cRepo.FilterComments(dbCriteria);
            IEnumerable<Comment> comments = _mapper.Map<IEnumerable<Comment>>(res);

            return comments;
        }

    }
}
