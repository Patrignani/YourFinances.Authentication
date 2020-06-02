using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleOAuth;
using SimpleOAuth.Models;
using YourFinances.Authentication.Domain.Core.DTOs;
using YourFinances.Authentication.Infra.CrossCutting;

namespace YourFinances.Authentication.Server.Middleware.Base
{
    public static class StartupMiddleware
    {
        public static IServiceCollection ConfigureApllicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfiguration = new AuthConfiguration
            {
                ExpireTimeMinutes_Client = configuration.GetValue<int>("AuthConfiguration:ExpireTimeMinutes_Client"),
                ExpireTimeMinutes_Password = configuration.GetValue<int>("AuthConfiguration:ExpireTimeMinutes_Password"),
                ExpireTimeMinutes_Refresh = configuration.GetValue<int>("AuthConfiguration:ExpireTimeMinutes_Refresh"),
                Router = configuration.GetValue<string>("AuthConfiguration:Router"),
                TokenKey = configuration.GetValue<string>("AuthConfiguration:TokenKey"),
                RefreshToken_TimeValidHour = configuration.GetValue<int>("AuthConfiguration:RefreshToken_TimeValidHour"),
            };


            services
                 .AddScoped(serviceProvider => {
                     var token = serviceProvider.GetRequiredService<TokenRead>();
                     var value = new SessionUser();

                     if (token != null && token.Claims != null)
                     {
                         var user_Id = token.GetValue("Id_User");
                         var account_Id = token.GetValue("Account_Id");

                         if (int.TryParse(user_Id, out int userId) && int.TryParse(account_Id, out int accountId))
                         {
                             value.Id = userId;
                             value.AccountId = accountId;
                         }
                     }

                     return value;
                 })
                .Register(configuration)
                .AddCorsServices(configuration)
                .AddResponseCompression()
                .AddSimpleOAuth(option =>
                {
                    option.AddKeyToken(authConfiguration.TokenKey);
                    option.AddAuthRouter(authConfiguration.Router);
                })
                .AddSingleton(_ => authConfiguration);

            return services;
        }

        public static IApplicationBuilder ConfigureApllication(this IApplicationBuilder app)
        {
            app.AddCorsApllication()
               .UseResponseCompression()
               .UseSimpleOAuth();

            return app;
        }
    }
}
