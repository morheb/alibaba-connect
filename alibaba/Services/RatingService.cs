

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

    public class RatingService
    {
        private readonly IMapper _mapper;
        private readonly IRatingRepo _pRepo;

        public RatingService(IRatingRepo pRepo, IMapper mapper)
        {
            _pRepo = pRepo;
            _mapper = mapper;

        }
              public async Task<bool> PostRatingAsync(Rating rating)
        {
           
            DbRating dbRest = _mapper.Map<DbRating>(rating);
            var prod = await _pRepo.PostRatingAsync(dbRest);
          
            return prod;
        }
    }
}
