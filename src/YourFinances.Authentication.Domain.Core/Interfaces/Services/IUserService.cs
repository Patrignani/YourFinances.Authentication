﻿using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.DTOs;
using YourFinances.Authentication.Domain.Core.DTOs.Object;

namespace YourFinances.Authentication.Domain.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<ResultModel<UserBasic>> BasicRegisterAsync(UserRegister userRegister);
    }
}