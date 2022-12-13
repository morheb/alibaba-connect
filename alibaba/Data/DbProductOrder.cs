using System;
using System.Collections.Generic;

namespace alibaba.Data
{
    public class DbProductOrder
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }

    }

}
