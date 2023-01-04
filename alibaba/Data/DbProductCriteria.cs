using System;

namespace alibaba.Data
{
    public class DbProductCriteria
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int RestaurantId { get; set; }
        public int Category { get; set; }
        public bool isOffer { get; set; }
        public int SubCategory { get; set; }
        public string Name { get; set; }
        public bool IsTop { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegiterian { get; set; }
        public bool IsOrganic { get; set; }
        public bool IsDiaryFree { get; set; }
        public bool IsZeroSugar { get; set; }
        public double Rating { get; set; }


    }
}
