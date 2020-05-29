using System;
using System.Collections.Generic;
using System.Text;

namespace YourFinances.Authentication.Domain.Core.Models
{
    public class Client
    {
        protected Client()
        { 
        
        }

        public Client(int id, string identification, string clientSecret, string clientId, bool active)
        {
            Id = id;
            ClientSecret = clientSecret;
            ClientId = clientId;
            Active = active;
            Identification = identification;
        }

        public int Id { get; private set; }
        public string Identification { get; private set; }
        public string ClientSecret { get; private set; }
        public string ClientId { get; private set; }
        public bool Active { get; private set; }
        
    }
}
