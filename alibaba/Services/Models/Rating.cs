using System;
using System.Collections.Generic;

namespace alibaba.Services.Models
{
    public class Rating
    {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Stars { get; set; }
        public int UserId { get; set; }

    }

}
