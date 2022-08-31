using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.UserPostModel
{
    public class ResponseModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
