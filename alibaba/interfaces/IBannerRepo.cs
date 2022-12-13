using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface IBannerRepo
    {
        Task<bool> PostBannerAsync(string url,  int id);
        Task<string> DeleteBannerAsync(  int id);
        Task<bool> UpdateBannerAsync(string url, int id);
        Task<string> GetBanner(int id);
        Task<IEnumerable<DbBanner>> GetAllBanners();

    }
}
