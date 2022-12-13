

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

    public class ConstantService
    {
        private readonly IMapper _mapper;
        private readonly IConstantRepo _cRepo;

        public ConstantService(IConstantRepo cRepo, IMapper mapper)
        {
            _cRepo = cRepo;
            _mapper = mapper;

        }
        public async Task<double> GetConstantById(int id)
        {

           return await _cRepo.GetConstant(id);
           
        }
   
        public async Task<bool> PostConstant(NewConstantRequest request)
        {
    
            var res = await _cRepo.PostCostantAsync( request.Value, request.Id, request.name );
          
            return res;
        }

        public async Task<bool> UpdateConstant(ChangeConstantRequest request )
        {
            var constant = await GetConstantById(request.Id);

            var val = 0.0;
            var id = 0;
            if (request.isPercentage)
            {
                constant = constant + (constant * request.Value);

            }
            else
            {
                constant = request.Value;
            }
            if (request.Value == 0)
            {
                constant = request.Value;

            }
            var res = await _cRepo.UpdateConstantAsync(constant, request.Id);

            return res;
        }
      


    }
}
