namespace alibaba.Data
{
    public class DbOrderStatus
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public bool WithDelivery { get; set; }

        public int DriverId { get; set; }


    }
}
