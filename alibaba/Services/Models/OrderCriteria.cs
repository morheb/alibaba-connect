using System;

namespace alibaba.Services.Models
{
    public class OrderCriteria
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int RestaurantId { get; set; }
        public bool WithDelivery { get; set; }
        public string DriverNumber { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
