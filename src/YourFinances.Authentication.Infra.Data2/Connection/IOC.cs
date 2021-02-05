using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourFinances.Authentication.Domain.Core.Interfaces.Connection;

namespace YourFinances.Authentication.Infra.Data.Connection
{
    public static class IOC
    {
        public static IServiceCollection RegisterData(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<ISqlHelper, SqlHelper>(_ => new SqlHelper(configuration.GetConnectionString("Authenticate")));

            return service;
        }
    }
}
