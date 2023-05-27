

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

    public class RestaurantService
    {
        private readonly IMapper _mapper;
        private readonly IRestaurantRepo _rRepo;

        public RestaurantService(IRestaurantRepo rRepo, IMapper mapper)
        {
            _rRepo = rRepo;
            _mapper = mapper;

        }
        public async Task<Restaurant> GetRestaurantById(int id)
        {

            var res = await _rRepo.GetRestaurantByIdAsync(id);
            //var r = res.WorkingHoursEnd.TimeOfDay;
            //var now = DateTime.Now.TimeOfDay;
            //if (r < now)
            //{
            //    res.Status = 0;
            //}
            var rest = _mapper.Map<Restaurant>(res);

            return rest;
        } 
        public async Task<Restaurant> GetMyRestaurantById(string id)
        {

            var res = await _rRepo.GetMyRestaurantByIdAsync(id);

            var r = res.WorkingHoursEnd.TimeOfDay;
            var now = DateTime.Now.TimeOfDay;
            
            var rest = _mapper.Map<Restaurant>(res);

            return rest;
        }
        
        public async Task<Response> PostRestaurant(Restaurant rest)
        {
           
            DbRestaurant dbRest = _mapper.Map<DbRestaurant>(rest);
            var dbres = await _rRepo.PostRestaurantAsync(dbRest);
            var  res = _mapper.Map<Response>(dbres);

            return res;
        }

        public async Task<bool> UpdateRestaurant(Restaurant rest )
        {
            DbRestaurant dbrestaurant = _mapper.Map<DbRestaurant>(rest);
            var res = await _rRepo.UpdateRestaurantAsync(dbrestaurant );

            return res;
        }
        public async Task<bool>SetRestaurantStatus(RestaurantStatus status )
        {
            DbRestaurantStatus statusDb = _mapper.Map<DbRestaurantStatus>(status);
            var res = await _rRepo.SetRestaurantStatusAsync(statusDb );

            return res;
        }
        
        public async Task<IEnumerable<Restaurant>>FilterRestaurant(RestaurantCriteria criteria )
        {
            DbRestaurantCriteria dbCriteria = _mapper.Map<DbRestaurantCriteria>(criteria);
            var res = await _rRepo.FilterRestaurantsList(dbCriteria );
            foreach(DbRestaurant rest in res)
            {
                var r = rest.WorkingHoursEnd.TimeOfDay;
                var now = DateTime.Now.TimeOfDay;
                if (r < now)
                {
                    rest.Status = 0;
                }
            }
            IEnumerable<Restaurant> restaurants = _mapper.Map<IEnumerable<Restaurant>>(res);

            return restaurants;
        }

    }
}
