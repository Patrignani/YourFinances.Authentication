using System;
using YourFinances.Authentication.Domain.Core.DTOs;
using YourFinances.Authentication.Domain.Core.DTOs.Object;

namespace YourFinances.Authentication.Domain.Core.Models
{
    public class Account
    {
        protected Account()
        { }

        public Account(int id, string identification, bool active)
        {
            Id = id;
            Identification = identification;
            Active = Active;
        }

        public Account(AccountRegister register)
        {
            Identification = register.Identification;
            Active = register.Active;
        }

        public int Id { get; private set; }
        public string Identification { get; private set; }
        public bool Active { get; private set; }
        public DateTime DateEdition { get; private set; }
        public int UserEditionId { get; private set; }

        public void SetId(int id) => Id = id;

        public ValidateModel Valid()
        {
            var validate = new ValidateModel();

            if (string.IsNullOrEmpty(Identification))
            {
                validate.NotValid("Identificação Obrigatória.");
            }

            return validate;
        }

    }
}
