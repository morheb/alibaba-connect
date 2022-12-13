using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface ITalabakRepo
    {
        Task<DbTalabak> GetTalabakByIdAsync(int talabakId);
        Task<int> GetTalabaksStatistics(DbTalabakCriteria criteria);
        Task<DbResponse> PostTalabakAsync(DbTalabak talabak);
        Task<bool> UpdateTalabakStatusAsync(DbTalabakStatus talabak ); 

        Task<IEnumerable<DbTalabak>> FilterTalabaks(DbTalabakCriteria criteria);
    }
}
