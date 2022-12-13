

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

    public class BannerService
    {
        private readonly IMapper _mapper;
        private readonly IBannerRepo _bRepo;

        public BannerService(IBannerRepo cRepo, IMapper mapper)
        {
            _bRepo = cRepo;
            _mapper = mapper;

        }
        public async Task<string> GetBannerById(int id)
        {

           return await _bRepo.GetBanner(id);
           
        }
   
        public async Task<bool> PostBanner(Banner request)
        {
    
            var res = await _bRepo.PostBannerAsync( request.Url, request.Id );
          
            return res;
        }
        public async Task<string> DeleteBanner(int id)
        {
    
            var res = await _bRepo.DeleteBannerAsync(id );
          
            return res;
        }

        public async Task<bool> UpdateBanner(Banner request )
        {
           
            var res = await _bRepo.UpdateBannerAsync(request.Url, request.Id);

            return res;
        }
      
        
        public async Task<IEnumerable<Banner>> GetAllBannersAsync( )
        {
           
            var res = await _bRepo.GetAllBanners();
            IEnumerable<Banner> banners = _mapper.Map<IEnumerable<Banner>>(res);

            return banners;
        }
      


    }
}
