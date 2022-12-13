using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface IAddonRepo
    {
        Task<DbAddon> GetAddonByIdAsync(int Id);
        Task<bool> PostAddonAsync(DbAddon addon);
        Task<bool> UpdateAddonsAsync(DbAddon addon ); 
        Task<string> DeleteAddonsAsync(int id ); 

        Task<IEnumerable<DbAddon>> FilterAddons(DbAddonCriteria criteria);
    }
}
