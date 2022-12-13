using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface IConstantRepo
    {
        Task<bool> PostCostantAsync(double value,  int id, string name);
        Task<bool> UpdateConstantAsync(double value, int id);
        Task<double> GetConstant(int id);

    }
}
