using System;
using System.Collections.Generic;

namespace alibaba.Data
{
    public class DbTalabak
    {

        public int Id { get; set; }
        public string SenderName { get; set; } 
        public string SenderPhone { get; set; }
        public int Status { get; set; }
        public string Object { get; set; }
        public string Note { get; set; }
        public string Time { get; set; }
        public int SenderId { get; set; }
        public string SenderLocation { get; set; }
        public string SenderLocationInfo { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverLocation { get; set; }
        public string ReceiverLocationInfo { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public double Fees { get; set; }
        public int DriverId { get; set; }



    }

}
