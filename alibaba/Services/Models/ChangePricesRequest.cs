using System;
using System.Collections.Generic;

namespace alibaba.Services.Models
{
    public class ChangePricesRequest
    {

        public int RestaurantId { get; set; }
       
        public double Percentage { get; set; }
    }

}
