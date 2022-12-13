using System;

namespace alibaba.Data
{
    public class DbTalabakCriteria
    {
        public int SenderId { get; set; }
        public int DriverId { get; set; }
        public int Status { get; set; }
     
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
