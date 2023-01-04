using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface IBrandRepo
    {
        Task<bool> PostBrandAsync(DbBrand brand);
        Task<bool> UpdateBrandAsync(DbBrand cat);
        Task<IEnumerable<DbBrand>> FilterCategories(int resId);
        Task<string> DeleteBrand(int id);

    }
}
