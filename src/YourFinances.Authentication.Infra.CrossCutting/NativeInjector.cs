using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleOAuth.Interfaces;
using YourFinances.Authentication.Domain.Core.Interfaces.Repository;
using YourFinances.Authentication.Domain.Core.Interfaces.Services;
using YourFinances.Authentication.Domain.Services;
using YourFinances.Authentication.Infra.Data.Connection;
using YourFinances.Authentication.Infra.Data.Repository;

namespace YourFinances.Authentication.Infra.CrossCutting
{
    public static class NativeInjector
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterData(configuration);

            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            //Service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationRoles, AuthorizationRoles>();

            return services;
        }
    }
}
