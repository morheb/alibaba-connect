using System;
using System.Collections.Generic;

namespace alibaba.Data
{
    public class DbProductOfferRequest
    {

        public int ProductId { get; set; }
        public int RestaurantId { get; set; }

        public double Offer { get; set; }
        public DateTime OfferExpiry { get; set; }
    }


}
