using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface IRatingRepo
    {
        Task<bool> PostRatingAsync(DbRating rating);
        //Task<bool> UpdateAddonsAsync(DbAddon addon ); 
        //Task<string> DeleteAddonsAsync(int id ); 

        //Task<IEnumerable<DbAddon>> FilterAddons(DbAddonCriteria criteria);
    }
}
