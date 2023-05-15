

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

    public class ProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo _pRepo;

        public ProductService(IProductRepo pRepo, IMapper mapper)
        {
            _pRepo = pRepo;
            _mapper = mapper;

        }
        public async Task<Product> GetProductById(int id)
        {

            var prod = await _pRepo.GetProductByIdAsync(id);
            var product = _mapper.Map<Product>(prod);
            return product;
        }
        
        public async Task<IEnumerable<Product>> GetProductListByIds(List<int> ids)
        {

            var prod = await _pRepo.GetProductListByIdsAsync(ids);
            var product = _mapper.Map<IEnumerable<Product>>(prod);
            return product;
        }
        
        public async Task<Response> DeleteProductById(int id)
        {

            var res
                = await _pRepo.DeleteByIdAsync(id);
            var product = _mapper.Map<Response>(res);
            return product;
        }
          public async Task<Response> UpdatePrices(int restId, double percentage)
        {

            var res
                = await _pRepo.UpdatePrices(id, percentage);
            var product = _mapper.Map<Response>(res);
            return product;
        }
        
        public async Task<bool> PostProduct(Product product)
        {
           
            DbProduct dbRest = _mapper.Map<DbProduct>(product);
            var prod = await _pRepo.PostProductAsync(dbRest);
          
            return prod;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            DbProduct dbproduct = _mapper.Map<DbProduct>(product);
            var prod = await _pRepo.UpdateProductAsync(dbproduct);

            return prod;
        }
        public async Task<bool> SetProductOffer(ProductOfferRequest offer)
        {
            DbProductOfferRequest dbOffer = _mapper.Map<DbProductOfferRequest>(offer);
            var prod = await _pRepo.SetOfferAsync(dbOffer);

            return prod;
        }
        public async Task<ChangePricesResponse> ChangePrices(ChangePricesRequest request)
        {
            var prods = await _pRepo.FilterProducts(new DbProductCriteria()
            {
                RestaurantId = request.RestaurantId
            });
            foreach(DbProduct p in prods)
            {
                p.Price = p.Price + (p.Price * (request.Percentage / 100));
              await  _pRepo.UpdateProductAsync(p);
            }

             prods = await _pRepo.FilterProducts(new DbProductCriteria()
            {
                RestaurantId = request.RestaurantId
            });
            IEnumerable<Product> products = _mapper.Map<IEnumerable<Product>>(prods);

            return new ChangePricesResponse() { 
            Products = products
            };

        }
        //public async Task<bool>SetProductStatus(ProductStatus status )
        //{
        //    DbProductStatus statusDb = _mapper.Map<DbProductStatus>(status);
        //    var prod = await _pRepo.SetProductStatusAsync(statusDb );

        //    return prod;
        //}

        public async Task<IEnumerable<Product>> FilterProduct(ProductCriteria criteria)
        {
            DbProductCriteria dbCriteria = _mapper.Map<DbProductCriteria>(criteria);
            var prods = await _pRepo.FilterProducts(dbCriteria);
            try
            {
                if (prods != null)
                {
                    foreach (DbProduct prod in prods)
                    {
                        if (!string.IsNullOrEmpty(prod.OfferExpiryString))
                        {
                            var parts = prod.OfferExpiryString.Split(' ');
                            DateTime date = DateTime.Parse(parts[0]);

                            var time = parts[1].Split('-');
                            Console.WriteLine(time[0]);
                            Console.WriteLine(time[1]);
                            Console.WriteLine(time[2]);
                            TimeSpan ts = new TimeSpan(int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
                            prod.OfferExpiry = date + ts;

                        }
                    }
                }
                else
                {
                    prods = new List<DbProduct>();
                }
                
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            IEnumerable<Product> products = _mapper.Map<IEnumerable<Product>>(prods);

            return products;
        }

    }
}
