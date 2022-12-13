using System;

namespace alibaba.Data
{
    public class DbRestaurantCriteria
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Name { get; set; }
        public bool IsTop { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public double Rating { get; set; }


    }
}
