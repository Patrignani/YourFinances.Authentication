﻿using ModularSystem.Messaging.RabbitMQ.Core.EventBus;
using SimpleOAuth.Models;
using System;
using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.Command.Account;
using YourFinances.Authentication.Domain.Core.DTOs;
using YourFinances.Authentication.Domain.Core.DTOs.Object;
using YourFinances.Authentication.Domain.Core.Interfaces.Repository;
using YourFinances.Authentication.Domain.Core.Interfaces.Services;

namespace YourFinances.Authentication.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRabbitMQEventBus _bus;
        private readonly IAccountRepository _account;
        private readonly TokenRead _tokenRead;

        public AccountService(IAccountRepository account, TokenRead token, IRabbitMQEventBus bus)
        {
            _account = account;
            _tokenRead = token;
            _bus = bus;
        }

        public async Task<ResultModel<AccountBasic>> RegisterAccountAsync(AccountRegister accountRegister)
        {
            var result = new ResultModel<AccountBasic>();

            try
            {
                var id_User = _tokenRead.GetValue("Id_User");
                if (!string.IsNullOrEmpty(id_User) && int.TryParse(id_User, out int userId))
                {
                    var account = new Core.Models.Account(accountRegister);
                    var valid = account.Valid();
                    if (valid.Success)
                    {
                        await _account.RegisterAccount(account, userId);
                        if (account.Id > 0)
                        {
                            await _bus.PublishForgetAsync(new AccountCommandInsert { 
                                AccountId = account.Id
                            });

                            result.SetData(new AccountBasic
                            {
                                Active = account.Active,
                                Id = account.Id,
                                Identification = account.Identification
                            });
                        }
                        else
                        {
                            result.NotValid();
                            result.SetMessages("Não foi possíovel realizar o cadastro!");
                        }
                    }
                    else
                    {
                        result.NotValid();
                        result.SetMessages(valid.Messages);
                    }
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
