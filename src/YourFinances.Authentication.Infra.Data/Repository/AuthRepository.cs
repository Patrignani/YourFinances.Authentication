using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.DTOs;
using YourFinances.Authentication.Domain.Core.Interfaces.Connection;
using YourFinances.Authentication.Domain.Core.Interfaces.Repository;
using YourFinances.Authentication.Domain.Core.Models;

namespace YourFinances.Authentication.Infra.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ISqlHelper _sql;
        private readonly AuthConfiguration _authConfiguration;

        public AuthRepository(ISqlHelper sql, AuthConfiguration authConfiguration)
        {
            _sql = sql;
            _authConfiguration = authConfiguration;
        }

        public async Task<Client> LoginClientAsync(string clientId, string clientSecret)
        {
            Client client;
            using (var con = await _sql.OpenConnetctionAsync())
            {
                client =await con.QueryFirstOrDefaultAsync<Client>("Auth_Client_sp", new
                {
                   ClientId = clientId,
                   ClientSecrete = clientSecret
               }, commandType: System.Data.CommandType.StoredProcedure);
            }

            return client;
        }

        public async Task<UserClaims> LoginPasswordAsync(string clientId, string clientSecret,User user)
        {
            UserClaims password = null;
            using (var con = await _sql.OpenConnetctionAsync())
            {
                var aa = await con.QueryFirstAsync("Auth_Password_sp", new
                {
                    ClientId = clientId,
                    ClientSecrete = clientSecret,
                    user.Password,
                    user.Email,
                    ExpirationDate = DateTime.UtcNow.AddHours(_authConfiguration.RefreshToken_TimeValidHour)

                }, commandType: System.Data.CommandType.StoredProcedure);

                if (aa is UserClaims)
                    password = (UserClaims)aa;
                else if(aa is string)
                    throw new System.InvalidOperationException((string)aa);
            }

            return password;
        }

        public async Task<UserClaims> LoginRefreshTokenAsync(string clientId, string clientSecret, string refreshToken)
        {

            UserClaims password;
            using (var con = await _sql.OpenConnetctionAsync())
            {
                password = await con.QueryFirstOrDefaultAsync<UserClaims>("Auth_RefreshToken_sp", new
                {
                    ClientId = clientId,
                    ClientSecrete = clientSecret,
                    RefreshToken = refreshToken,
                    ExpirationDate = DateTime.UtcNow.AddHours(_authConfiguration.RefreshToken_TimeValidHour)

                }, commandType: System.Data.CommandType.StoredProcedure);
            }

            return password;
        }
    }
}
