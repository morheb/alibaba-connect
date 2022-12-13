using System;

namespace alibaba.Services.Models
{
    public class TalabakCriteria
    {
        public int SenderId { get; set; }
        public int DriverId { get; set; }
        public int Status { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
