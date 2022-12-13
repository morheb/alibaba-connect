using System;
using System.Collections.Generic;

namespace alibaba.Services.Models
{
    public class AddonOrder
    {


        public int OrderId { get; set; }
        public int AddonId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }


    }

}
