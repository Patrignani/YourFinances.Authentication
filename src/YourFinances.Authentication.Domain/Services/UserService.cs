using System;
using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.DTOs;
using YourFinances.Authentication.Domain.Core.DTOs.Object;
using YourFinances.Authentication.Domain.Core.Interfaces.Repository;
using YourFinances.Authentication.Domain.Core.Interfaces.Services;

namespace YourFinances.Authentication.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository user)
        {
            _userRepository = user;
        }

        public async Task<ResultModel<UserBasic>> BasicRegisterAsync(UserRegister userRegister)
        {
            var result = new ResultModel<UserBasic>();

            try
            {
                var user = new Core.Models.User(userRegister);
                var valid = user.Valid();
                if (valid.Success)
                {
                    await _userRepository.BasicRegisterAsync(user);
                    result.SetData(new UserBasic
                    {
                        Active = user.Active,
                        Id = user.Id,
                        Email = user.Email,
                        Identification = user.Identification
                    });
                }
                else
                {
                    result.NotValid();
                    result.SetMessages(valid.Messages);
                }
            }
            catch (Exception e)
            {
                result.NotValid(e.GetBaseException().Message);
            }

            return result;
        }
    }
}
