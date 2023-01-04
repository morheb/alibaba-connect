

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

    public class BrandService
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepo _cRepo;

        public BrandService(IBrandRepo cRepo, IMapper mapper)
        {
            _cRepo = cRepo;
            _mapper = mapper;

        }
        
        public async Task<bool> PostBrand(Brand category)
        {
            DbBrand dbRest;
            var dbBrand = _mapper.Map<DbBrand>(category);
    
            //var dbBrand = this.ToDbBrand(category);
            var res = await _cRepo.PostBrandAsync(dbBrand);
          
            return res;
        }

        public async Task<bool> UpdateBrand(Brand rest)
        {
            DbBrand dbcategory = _mapper.Map<DbBrand>(rest);
            var res = await _cRepo.UpdateBrandAsync(dbcategory);

            return res;
        }
        public async Task<string> DeleteBrand(int id)
        {
            var res = await _cRepo.DeleteBrand(id);

            return res;
        }


        public async Task<IEnumerable<Brand>> FilterBrand(int restaurant)
        {
            var res = await _cRepo.FilterCategories(restaurant);
            IEnumerable<Brand> categorys = _mapper.Map<IEnumerable<Brand>>(res);

            return categorys;
        }

    }
}
