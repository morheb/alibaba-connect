

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

    public class TalabakService
    {
        private readonly IMapper _mapper;
        private readonly ITalabakRepo _tRepo;

        public TalabakService(ITalabakRepo tRepo, IMapper mapper)
        {
            _tRepo = tRepo;
            _mapper = mapper;

        }
        public async Task<Talabak> GetTalabakById(int id)
        {

            var res = await _tRepo.GetTalabakByIdAsync(id);
            var rest = _mapper.Map<Talabak>(res);
            return rest;
        }
        
        public async Task<int> GetTalabakStatistics(TalabakCriteria criteria)
        {
            DbTalabakCriteria dbCriteria = _mapper.Map<DbTalabakCriteria>(criteria);


            var res = await _tRepo.GetTalabaksStatistics(dbCriteria);
   
            return res;
        }
        
        public async Task<Response> PostTalabak(Talabak talabak)
        {
            DbTalabak dbRest;
            var dbTalabak = _mapper.Map<DbTalabak>(talabak);
    
            //var dbTalabak = this.ToDbTalabak(talabak);
            var res = await _tRepo.PostTalabakAsync(dbTalabak);
            var response = _mapper.Map<Response>(res);

            return response;
        }

        public async Task<bool> UpdateTalabakStatus(TalabakStatus status)
        {
            var dbtalabak = _mapper.Map<DbTalabakStatus>(status);
            var res = await _tRepo.UpdateTalabakStatusAsync(dbtalabak);

            return res;
        }
        //public async Task<bool>SetTalabakStatus(TalabakStatus status )
        //{
        //    DbTalabakStatus statusDb = _mapper.Map<DbTalabakStatus>(status);
        //    var res = await _pRepo.SetTalabakStatusAsync(statusDb );

        //    return res;
        //}

        public async Task<IEnumerable<Talabak>> FilterTalabak(TalabakCriteria criteria)
        {
            DbTalabakCriteria dbCriteria = _mapper.Map<DbTalabakCriteria>(criteria);
            var res = await _tRepo.FilterTalabaks(dbCriteria);
            IEnumerable<Talabak> talabaks = _mapper.Map<IEnumerable<Talabak>>(res);

            return talabaks;
        }


    }
}
