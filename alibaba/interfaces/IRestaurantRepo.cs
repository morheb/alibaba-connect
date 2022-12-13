using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface IRestaurantRepo
    {
        Task<DbRestaurant> GetRestaurantByIdAsync(int restId);
        Task<DbRestaurant> GetMyRestaurantByIdAsync(string uId);
        Task<DbResponse> PostRestaurantAsync(DbRestaurant rest);
        Task<bool> UpdateRestaurantAsync(DbRestaurant rest ); 
        Task<bool> SetRestaurantStatusAsync(DbRestaurantStatus dbRestStatus );
        Task<IEnumerable<DbRestaurant>> FilterRestaurantsList(DbRestaurantCriteria criteria);
    }
}
