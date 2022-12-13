

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

    public class AddonService
    {
        private readonly IMapper _mapper;
        private readonly IAddonRepo _pRepo;

        public AddonService(IAddonRepo pRepo, IMapper mapper)
        {
            _pRepo = pRepo;
            _mapper = mapper;

        }
        public async Task<Addon> GetAddonById(int id)
        {

            var prod = await _pRepo.GetAddonByIdAsync(id);
            var addon = _mapper.Map<Addon>(prod);
            return addon;
        }
        
        public async Task<bool> PostAddon(Addon addon)
        {
           
            DbAddon dbRest = _mapper.Map<DbAddon>(addon);
            var prod = await _pRepo.PostAddonAsync(dbRest);
          
            return prod;
        }

        public async Task<bool> UpdateAddon(Addon addon)
        {
            DbAddon dbaddon = _mapper.Map<DbAddon>(addon);
            var prod = await _pRepo.UpdateAddonsAsync(dbaddon);

            return prod;
        }
    
        public async Task<string> DeleteAddon(int id)
        {
            var prod = await _pRepo.DeleteAddonsAsync(id);

            return prod;
        }
    

        public async Task<IEnumerable<Addon>> FilterAddon(AddonCriteria criteria)
        {
            DbAddonCriteria dbCriteria = _mapper.Map<DbAddonCriteria>(criteria);
            var prod = await _pRepo.FilterAddons(dbCriteria);
            IEnumerable<Addon> addons = _mapper.Map<IEnumerable<Addon>>(prod);

            return addons;
        }

    }
}
