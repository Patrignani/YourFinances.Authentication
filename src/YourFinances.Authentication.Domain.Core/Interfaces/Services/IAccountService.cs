using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.DTOs;
using YourFinances.Authentication.Domain.Core.DTOs.Object;

namespace YourFinances.Authentication.Domain.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<ResultModel<AccountBasic>> RegisterAccountAsync(AccountRegister accountRegister);
    }
}
