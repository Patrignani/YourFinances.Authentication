using SimpleOAuth.Interfaces;
using SimpleOAuth.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.DTOs;
using YourFinances.Authentication.Domain.Core.Interfaces.Repository;
using YourFinances.Authentication.Domain.Core.Models;

namespace YourFinances.Authentication.Domain.Services
{
    public class AuthorizationRoles : IAuthorizationRoles
    {
        private readonly IAuthRepository _auth;
        private readonly AuthConfiguration _authConfiguration;

        public AuthorizationRoles(IAuthRepository auth, AuthConfiguration authConfiguration)
        {
            _auth = auth;
            _authConfiguration = authConfiguration;
        }

        public async Task<AuthorizationRolesClient> ClientCredentialsAuthorizationAsync(OAuthClient client)
        {
            var result = new AuthorizationRolesClient();
            try
            {
                var clientValue = await _auth.LoginClientAsync(client.Client_id, client.Client_secret);
                result.Authorized = clientValue != null && clientValue.Id > 0;
                if (result.Authorized)
                {
                    result.ExpireTimeMinutes = _authConfiguration.ExpireTimeMinutes_Client;
                    result.Claims = new Claim[]
                    {
                        new Claim("Client",clientValue.Identification)
                    };
                }
            }
            catch (Exception e)
            {
                result.Authorized = false;
                result.Errors.Add(e.GetBaseException().Message);
            }

            return result;
        }

        public async Task<AuthorizationRolesPassword> PasswordAuthorizationAsync(OAuthPassword oauthPassword)
        {
            var result = new AuthorizationRolesPassword();
            try
            {
                var user = new User(oauthPassword.Username, oauthPassword.Password);
                var password = await _auth.LoginPasswordAsync(oauthPassword.Client_id, oauthPassword.Client_secret, user);
                result.Authorized = password != null && password.Id > 0;

                if (result.Authorized)
                {
                    result.ExpireTimeMinutes = _authConfiguration.ExpireTimeMinutes_Password;
                    result.Claims = new Claim[]
                    {
                        new Claim("Client_Identification",password.ClientIdentification),
                        new Claim("Id_User",password.Id.ToString()),
                        new Claim("Email",password.Email),
                        new Claim("User_Identification",password.Identification),
                        new Claim("AcceptTerm", password.AcceptTerm ? "true": "false"),
                        new Claim("AccountId",password.AccountId.ToString()),
                    };
                    result.RefreshToken = password.RefreshToken;
                }
            }
            catch (Exception e)
            {
                result.Authorized = false;
                result.Errors.Add(e.GetBaseException().Message);
            }

            return result;
        }

        public Task<AuthorizationRolesRefresh> RefreshTokenCredentialsAuthorizationAsync(OAuthRefreshToken refreshToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
