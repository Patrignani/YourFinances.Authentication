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
        private readonly SessionUser _sessionUser;


        public UserService(IUserRepository user, SessionUser session)
        {
            _userRepository = user;
            _sessionUser = session;
        }

        public async Task<ResultModel<UserBasic>> BasicRegisterAsync(UserRegister userRegister)
        {
            var user = new UserRegisterInternal
            {
                Email = userRegister.Email,
                Identification = userRegister.Identification,
                Password = userRegister.Password,
                AccountId = null,
                UserId = null
            };

            return await RegisterUserAsync(user);
        }

        public async Task<ResultModel<UserBasic>> InternalRegisterAsync(UserRegister userRegister)
        {

            var result = new ResultModel<UserBasic>();

            var user = new UserRegisterInternal
            {
                Email = userRegister.Email,
                Identification = userRegister.Identification,
                Password = userRegister.Password,
                UserId = _sessionUser.Id,
                AccountId = _sessionUser.AccountId
            };

            result = await RegisterUserAsync(user);

            return result;
        }

        public async Task<ValidateModel> KeepConnectedAsync(bool keepConnected)
        {
            var result = new ValidateModel(false);

            try
            {

                if (await _userRepository.KeepConnectedAsync(keepConnected, _sessionUser.Id) == 1)
                    result.IsValid();
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

                if (await _userRepository.AccepTermAsync(_sessionUser.Id) == 1)
                    result.IsValid();
            }
            catch (Exception e)
            {
                result.NotValid(e.GetBaseException().Message);
            }

            return result;
        }

        async Task<ResultModel<UserBasic>> RegisterUserAsync(UserRegisterInternal userRegister)
        {
            var result = new ResultModel<UserBasic>();

            try
            {

                var user = new Core.Models.User(userRegister);

                var valid = user.Valid(userRegister.Password);
                if (valid.Success)
                {
                    await _userRepository.BasicRegisterAsync(user);

                    if (user.Id > 0)
                    {
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
                        result.SetMessages("Não foi possível realizar o cadastro!");
                    }
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
