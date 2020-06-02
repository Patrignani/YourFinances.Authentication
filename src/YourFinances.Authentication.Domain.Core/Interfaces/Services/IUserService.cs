using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.DTOs;
using YourFinances.Authentication.Domain.Core.DTOs.Object;

namespace YourFinances.Authentication.Domain.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<ResultModel<UserBasic>> BasicRegisterAsync(UserRegister userRegister);
        Task<ValidateModel> KeepConnectedAsync(bool keepConnected);
        Task<ValidateModel> AccepTermAsync();
        Task<ResultModel<UserBasic>> InternalRegisterAsync(UserRegister userRegister);
    }
}
