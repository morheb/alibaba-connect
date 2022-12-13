using System;
using System.Collections.Generic;

namespace alibaba.Services.Models
{
    public class ChangePricesResponse
    {

     public IEnumerable<Product> Products { get; set; }
     public bool Success { get; set; }
     public string Error { get; set; }
    }

}
