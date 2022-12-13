using System;
using System.Collections.Generic;

namespace alibaba.Services.Models
{
    public class Addon
    {

        public int Id { get; set; }
        public string Name { get; set; }
    
        public double Price { get; set; }
      
        public string Image { get; set; }
        public int ProductId { get; set; }
    
    }

}
