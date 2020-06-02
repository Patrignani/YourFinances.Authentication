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
                var userClaims = await _auth.LoginPasswordAsync(oauthPassword.Client_id, oauthPassword.Client_secret, user);
                result.Authorized = userClaims != null && userClaims.Id > 0;

                if (result.Authorized)
                {
                    if (userClaims.KeepConnected)
                        result.ExpireTimeMinutes = 360 *24*60;
                    else
                        result.ExpireTimeMinutes = _authConfiguration.ExpireTimeMinutes_Password;

                    result.Claims = new Claim[]
                    {
                        new Claim("Client_Identification",userClaims.ClientIdentification),
                        new Claim("Id_User",userClaims.Id.ToString()),
                        new Claim("Email",userClaims.Email),
                        new Claim("User_Identification",userClaims.Identification),
                        new Claim("AcceptTerm", userClaims.AcceptTerm ? "true": "false"),
                        new Claim("AccountId",userClaims.AccountId.ToString()),
                        new Claim("RefreshToken",userClaims.RefreshToken),
                    };
                    result.RefreshToken = userClaims.RefreshToken;
                }
            }
            catch (Exception e)
            {
                result.Authorized = false;
                result.Errors.Add(e.GetBaseException().Message);
            }

            return result;
        }

        public async Task<AuthorizationRolesRefresh> RefreshTokenCredentialsAuthorizationAsync(OAuthRefreshToken refreshToken)
        {
            var result = new AuthorizationRolesRefresh();
            try
            {
                var userClaims = await _auth.LoginRefreshTokenAsync(refreshToken.Client_id, refreshToken.Client_secret, refreshToken.Refresh_token);
                result.Authorized = userClaims != null && userClaims.Id > 0;

                if (result.Authorized)
                {
                    result.ExpireTimeMinutes = _authConfiguration.ExpireTimeMinutes_Password;
                    result.Claims = new Claim[]
                    {
                        new Claim("Client_Identification",userClaims.ClientIdentification),
                        new Claim("Id_User",userClaims.Id.ToString()),
                        new Claim("Email",userClaims.Email),
                        new Claim("User_Identification",userClaims.Identification),
                        new Claim("AcceptTerm", userClaims.AcceptTerm ? "true": "false"),
                        new Claim("AccountId",userClaims.AccountId.ToString()),
                        new Claim("RefreshToken",userClaims.RefreshToken),
                    };
                    result.RefreshToken = userClaims.RefreshToken;
                }
            }
            catch (Exception e)
            {
                result.Authorized = false;
                result.Errors.Add(e.GetBaseException().Message);
            }

            return result;
        }
    }
}
