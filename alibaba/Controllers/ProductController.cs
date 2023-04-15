using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;
using alibaba.Services.Models;

namespace alibaba.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _pService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, ProductService pService)
        {
            _pService = pService;
            _logger = logger;
        }

        [Route("getproduct/{id}")]
        [HttpGet]
        public async Task<Product> GetProductById([FromRoute] int id)
        {
            var result = await _pService.GetProductById(id);
            return result;
        }[Route("getproductsList")]
        [HttpPost]
        public async Task<IEnumerable<Product>> GetProductListByIds([FromBody] List<int> ids)
        {
            var result = await _pService.GetProductListByIds(ids);
            return result;
        }
        
        [Route("deleteproduct/{id}")]
        [HttpDelete]
        public async Task<Response> DeleteProductById([FromRoute] int id)
        {
            var result = await _pService.DeleteProductById(id);
            return result;
        }


        [HttpPost("Add")]
        public async Task<bool> RegisterProduct([FromBody] Product product)
        {
            var result = await _pService.PostProduct(product);
            return result;
        }

        [HttpPut("updateProduct")]
        public async Task<string> Updateproduct([FromBody] Product product)
        {
            var result = await _pService.GetProductById(product.Id);
            if (result != null)
            {
                await _pService.UpdateProduct(product);
                return "success";
            }

            return "product doesnt exisit";

        }
        
        [HttpPut("setproductoffer")]
        public async Task<string> SetOffer([FromBody] ProductOfferRequest offer)
        {
            var result = await _pService.GetProductById(offer.ProductId);
            if (result != null)
            {
                await _pService.SetProductOffer(offer);
                return "success";
            }

            return "product doesnt exisit";

        }
        
        
        [HttpPut("ChangeProductsPries")]
        public async Task<ChangePricesResponse> SetOffer([FromBody] ChangePricesRequest request)
        {
         try
            {
               var result =  await _pService.ChangePrices(request);
                return result;
            }
            catch
            {
                return new ChangePricesResponse()
                {
                    Products = null,
                    Success = false,
                    Error = "Error occured"
                };

            }  

        }


        [HttpPost("FilterProduct")]
        public async Task<IEnumerable<Product>> FilterProducts([FromBody] ProductCriteria criteria)
        {

            var res = await _pService.FilterProduct(criteria);
            return res;

        }


    }
}