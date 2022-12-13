using System;
using System.Collections.Generic;

namespace alibaba.Services.Models
{
    public class ProductOfferRequest
    {

        public int ProductId { get; set; }
        public int RestaurantId { get; set; }
       
        public double Offer { get; set; }
        public DateTime OfferExpiry { get; set; }
    }

}
