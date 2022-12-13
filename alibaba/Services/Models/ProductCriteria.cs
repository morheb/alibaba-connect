using System;

namespace alibaba.Services.Models
{
    public class ProductCriteria
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public double Rating { get; set; }
        public int RestaurantId { get; set; }
        public int Category { get; set; }
        public int SubCategory { get; set; }
        public bool isOffer { get; set; }

        public bool Istop { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
