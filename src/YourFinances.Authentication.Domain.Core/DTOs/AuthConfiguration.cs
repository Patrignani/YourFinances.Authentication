using System;
using System.Collections.Generic;
using System.Text;

namespace YourFinances.Authentication.Domain.Core.DTOs
{
    public class AuthConfiguration
    {
        public int ExpireTimeMinutes_Client { get; set; }
        public int ExpireTimeMinutes_Password { get; set; }
        public int ExpireTimeMinutes_Refresh { get; set; }
        public string TokenKey { get; set; }
        public string Router { get; set; }
        public int RefreshToken_TimeValidHour { get; set; }


    }
}
