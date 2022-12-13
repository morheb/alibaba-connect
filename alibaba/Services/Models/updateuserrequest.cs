using System;

namespace alibaba.Services.Models
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string FirebaseToken { get; set; }
        public string ForgetPasswordcode { get; set; }
        public DateTime ResetTokenExpires { get; set; }
        public int CodeEmailVerification { get; set; }
        public int CodephoneVerification { get; set; }

    }
}
