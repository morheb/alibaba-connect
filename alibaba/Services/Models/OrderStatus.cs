namespace alibaba.Services.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public bool  WithDelivery { get; set; }
        public int DriverId { get; set; }

    }
}
