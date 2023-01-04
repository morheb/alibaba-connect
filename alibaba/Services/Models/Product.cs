using System;
using System.Collections.Generic;

namespace alibaba.Services.Models
{
    public class Product
    {

        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public int SubCategory { get; set; }
        public double Price { get; set; }
        public double Offer { get; set; }
        public DateTime OfferExpiry { get; set; }
        public string Ingredients { get; set; }
        public bool  IsVegan { get; set; }
        public bool  IsVegiterian { get; set; }
        public bool IsDiaryFree { get; set; }
        public bool  IsZeroSugar { get; set; }
        public bool IsOrganic { get; set; }
        public double  Calories { get; set; }
        public string Image { get; set; }
        public int ETA { get; set; }
        public double Rating { get; set; }   
        public string Description { get; set; }

    }

}
