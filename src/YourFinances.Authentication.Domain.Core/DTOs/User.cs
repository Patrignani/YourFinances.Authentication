﻿namespace YourFinances.Authentication.Domain.Core.DTOs
{
    public class UserBasic
    {
        public int Id { get;  set; }
        public string Identification { get;  set; }
        public bool Active { get;  set; }
        public string Email { get; set; }
    }

    public class UserRegister
    {
        public string Identification { get; set; }
        public string Email { get;  set; }
        public string Password { get; set; }
    }

    public class UserRegisterInternal : UserRegister
    {

        public int? AccountId { get; set; }
        public int? UserId { get; set; }
    }

    public class SessionUser
    { 
        public int Id { get; set; }
        public int AccountId { get; set; }
    }

    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
