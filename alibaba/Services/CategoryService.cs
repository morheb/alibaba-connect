

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

    public class CategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepo _cRepo;

        public CategoryService(ICategoryRepo cRepo, IMapper mapper)
        {
            _cRepo = cRepo;
            _mapper = mapper;

        }
        
        public async Task<bool> PostCategory(Category category)
        {
            DbCategory dbRest;
            var dbCategory = _mapper.Map<DbCategory>(category);
    
            //var dbCategory = this.ToDbCategory(category);
            var res = await _cRepo.PostCategoryAsync(dbCategory);
          
            return res;
        }

        public async Task<bool> UpdateCategory(Category rest)
        {
            DbCategory dbcategory = _mapper.Map<DbCategory>(rest);
            var res = await _cRepo.UpdateCategoryAsync(dbcategory);

            return res;
        }
        public async Task<string> DeleteCategory(int id)
        {
            var res = await _cRepo.DeleteCategory(id);

            return res;
        }


        public async Task<IEnumerable<Category>> FilterCategory(int restaurant)
        {
            var res = await _cRepo.FilterCategories(restaurant);
            IEnumerable<Category> categorys = _mapper.Map<IEnumerable<Category>>(res);

            return categorys;
        }

    }
}
