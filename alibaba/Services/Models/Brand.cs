using System;
using System.Collections.Generic;

namespace alibaba.Services.Models
{
    public class Brand
    {

        public int Id { get; set; }
        public int RestaurantId { get; set; }

        public string ImgUrl { get; set; }

        public string Name { get; set; }
       
    }

}
