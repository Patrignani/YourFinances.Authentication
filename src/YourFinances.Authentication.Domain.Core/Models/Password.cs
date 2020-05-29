using System;
using System.Collections.Generic;
using System.Text;

namespace YourFinances.Authentication.Domain.Core.Models
{
    public class Password
    {
        protected Password()
        { 

        }

        public int Id { get; private set; }
        public string Identification { get; private set; }
        public string Email { get; private set; }
        public bool AcceptTerm { get; private set; }
        public int AccountId { get; private set; }
        public string RefreshToken { get; private set; }
        public string ClientIdentification { get; private set; }

    }
}
