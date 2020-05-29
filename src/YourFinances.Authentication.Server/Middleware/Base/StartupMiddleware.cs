using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleOAuth;
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
