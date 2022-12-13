using System;
using System.Collections.Generic;

namespace alibaba.Services.Models
{
    public class Restaurant
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Whatsapp { get; set; }
        public string Address { get; set; }
        public string FirebaseToken { get; set; }
        public string OwnerUid { get; set; }

        public List<int> Products { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string Logo { get; set; }
        public double Rating { get; set; }
        public DateTime WorkingHoursStart { get; set; }
        public DateTime WorkingHoursEnd { get; set; }
        public string Location { get; set; }
        public  bool IsActive { get; set; }
        public string LocationDescription { get; set; }

    }

}
