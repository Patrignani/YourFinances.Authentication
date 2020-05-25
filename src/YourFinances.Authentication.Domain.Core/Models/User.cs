using System.Security.Cryptography;
using System.Text;

namespace YourFinances.Authentication.Domain.Core.Models
{
    public class User
    {

        public User(DTOs.UserRegister register)
        {
            Identification = register.Identification;
            Email = register.Email;
            Password = register.Password;
            AcceptTerm = false;
            Active = false;
        }

        public User(DTOs.UserLogin login)
        {
            Email = login.Email;
            Password = login.Password;
        }

        public User(int id, string identification, bool active, bool acceptTerm, int accountId, string email, string password)
        {
            Id = id;
            Identification = identification;
            Active = active;
            AcceptTerm = acceptTerm;
            AccountId = accountId;
            Email = email;
            Password = password;
        }

        private string _password;
        public int Id { get; private set; }
        public string Identification { get; private set; }
        public bool Active { get; private set; }
        public bool AcceptTerm { get; private set; }
        public int? AccountId { get; private set; }
        public string Email { get; private set; }
        public string Password
        {
            get { return _password; }
            private set
            {
                SHA512 sha512 = SHA512Managed.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                byte[] hash = sha512.ComputeHash(bytes);
                _password= GetStringFromHash(hash);
            }
        }


        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        // public virtual Account Account { get; set; }

    }
}
