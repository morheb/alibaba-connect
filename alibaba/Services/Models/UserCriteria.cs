using System;

namespace alibaba.Services.Models
{
    public class UserCriteria
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        public double Rating { get; set; }
        public bool Istop { get; set; }
        public bool IsOnlyActive { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
