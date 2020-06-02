using SimpleOAuth.Models;
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
        private readonly TokenRead _tokenRead;

        public UserService(IUserRepository user, TokenRead token)
        {
            _userRepository = user;
            _tokenRead = token;
        }

        public async Task<ResultModel<UserBasic>> BasicRegisterAsync(UserRegister userRegister)
        {
            var result = new ResultModel<UserBasic>();

            try
            {
                var user = new Core.Models.User(userRegister);
                var valid = user.Valid(userRegister.Password);
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

        public async Task<ValidateModel> KeepConnectedAsync(bool keepConnected)
        {
            var result = new ValidateModel(false);

            try
            {
                var id_User = _tokenRead.GetValue("Id_User");

                if (!string.IsNullOrEmpty(id_User) && int.TryParse(id_User, out int userId))
                {
                    if (await _userRepository.KeepConnectedAsync(keepConnected, Convert.ToInt32(userId)) == 1)
                        result.IsValid();
                }
                else
                {
                    result.NotValid();
                    result.SetMessages("Error Token, not found User_Id");
                }

            }
            catch (Exception e)
            {
                result.NotValid(e.GetBaseException().Message);
            }
           
            return result;
        }

        public async Task<ValidateModel> AccepTermAsync()
        {
            var result = new ValidateModel(false);

            try
            {
                var id_User = _tokenRead.GetValue("Id_User");

                if (!string.IsNullOrEmpty(id_User) && int.TryParse(id_User, out int userId))
                {
                    if (await _userRepository.AccepTermAsync(Convert.ToInt32(userId)) == 1)
                        result.IsValid();
                }
                else
                {
                    result.NotValid();
                    result.SetMessages("Error Token, not found User_Id");
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
