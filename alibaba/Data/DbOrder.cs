﻿using System;
using System.Collections.Generic;

namespace alibaba.Data
{
    public class DbOrder
    {

        public int Id { get; set; }
        public List<DbProductOrder> Products { get; set; }
        public List<DbAddonOrder> Addons { get; set; }
        public int Status { get; set; }
        public double Price { get; set; }
        public string RestLocation  { get; set; }
        public int Type { get; set; }
        public int RestaurantId { get; set; }
        public string Time { get; set; }
        public bool WithDelivery { get; set; }

        public string RestaurantName { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }

        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public int ExtraFees { get; set; }
        public int DeliveryFees { get; set; }
        public string DriverNumber { get; set; }


    }


}
