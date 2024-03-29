﻿using System;

namespace alibaba.Data
{
    public class DbUser
    {

        public int Id { get; set; }
        public string FirebaseId {get; set;}
        public int UserType {get; set;}
        public string UserName { get; set; }
        public int Status { get; set; }
        public double Rating { get; set; }
        public bool EmailVerified { get; set; }
        public bool PhoneVerified { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string LocationDescription { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string FirebaseToken { get; set; }


    }

}
