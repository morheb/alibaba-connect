using System;

namespace alibaba.Services.Models
{
    public class RestaurantCriteria
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public double Rating { get; set; }
        public bool Istop { get; set; }
        public int PageSize { get; set; }
        public bool IsActive { get; set; }

        public int PageNumber { get; set; }
    }
}
