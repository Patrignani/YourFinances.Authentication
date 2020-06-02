using System;
using System.Collections.Generic;
using System.Text;
using YourFinances.Authentication.Domain.Core.Models;

namespace YourFinances.Authentication.Domain.Core.DTOs
{
    public class AccountRegister
    {
        public string Identification { get; set; }
        public bool Active { get; set; }

    }

    public class AccountBasic : AccountRegister
    {
        public int Id { get; set; }
    }
}
