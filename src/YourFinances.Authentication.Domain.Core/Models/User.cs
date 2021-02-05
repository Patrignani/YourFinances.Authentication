using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using YourFinances.Authentication.Domain.Core.DTOs.Object;

namespace YourFinances.Authentication.Domain.Core.Models
{
    public class User
    {
        public User(DTOs.UserRegisterInternal register)
        {
            Identification = register.Identification;
            Email = register.Email;
            Password = register.Password;
            AcceptTerm = false;
            Active = false;
            UserEditionId = register.UserId;
            AccountId = register.AccountId;
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

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        protected User()
        {
        }

        private string _password;
        public int Id { get; private set; }
        public string Identification { get; private set; }
        public bool Active { get; private set; }
        public bool AcceptTerm { get; private set; }
        public int? AccountId { get; private set; }
        public string Email { get; private set; }
        public DateTime DateEdition { get; private set; }
        public int? UserEditionId { get; private set; }
        public bool KeepConnected { get; private set; }

        public string Password
        {
            get { return _password; }
            private set
            {
                SHA512 sha512 = SHA512.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(value[value.Length - 1] + value + value[0]);
                byte[] hash = sha512.ComputeHash(bytes);
                _password = GetStringFromHash(hash);
            }
        }

        public ICollection<Account> Accounts { get; private set; }

        public void SetId(int id) => Id = id;
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public ValidateModel Valid(string password = "")
        {

            var validate = new ValidateModel();


            if (string.IsNullOrEmpty(Identification))
            {
                validate.NotValid("Identificação Obrigatória.");
            }

            if (string.IsNullOrEmpty(Email))
            {
                validate.NotValid("Email Obrigatória.");
            }
            else
            {
                if (!IsValidEmail(Email))
                {
                    validate.NotValid("Email inválido.");
                }
            }

            if (string.IsNullOrEmpty(Password))
            {
                validate.NotValid("Senha Obrigatória.");
            }
            else if (!string.IsNullOrEmpty(password))
            {

                var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
                Match match = regex.Match(password);

                if (!match.Success)
                {
                    validate.NotValid("Senha deve ter no mínimo de oito caracteres, pelo menos, uma letra maiúscula, uma letra minúscula, um número e um caractere especial.");
                }
            }

            return validate;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // public virtual Account Account { get; set; }

    }
}
