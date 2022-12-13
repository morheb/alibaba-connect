using System;

namespace alibaba.Data
{
    public class DbUserCriteria
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public bool IsTop { get; set; }
        public bool IsOnlyActive { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public double Rating { get; set; }


    }
}
