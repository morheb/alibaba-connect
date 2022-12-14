using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface IProductRepo
    {
        Task<DbProduct> GetProductByIdAsync(int prodId);
        Task<DbResponse> DeleteByIdAsync(int prodId);
        Task<bool> PostProductAsync(DbProduct prod);
        Task<bool> UpdateProductAsync(DbProduct prod ); 
        Task<bool> SetOfferAsync(DbProductOfferRequest offer ); 
        //Task<bool> SetProductStatusAsync(DbProductStatus dbProdStatus );
        Task<IEnumerable<DbProduct>> FilterProducts(DbProductCriteria criteria);
    }
}
