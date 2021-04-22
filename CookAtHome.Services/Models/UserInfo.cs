using System;
using System.Collections.Generic;
using System.Text;

namespace CookAtHome.Services.Models
{
    public class UserInfo
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
