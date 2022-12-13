using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface ICategoryRepo
    {
        Task<bool> PostCategoryAsync(DbCategory category);
        Task<bool> UpdateCategoryAsync(DbCategory cat);
        Task<IEnumerable<DbCategory>> FilterCategories(int resId);
        Task<string> DeleteCategory(int id);

    }
}
