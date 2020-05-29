using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.Models;

namespace YourFinances.Authentication.Domain.Core.Interfaces.Repository
{
    public interface IAuthRepository
    {
        Task<Client> LoginClientAsync(string clientId, string clientSecret);
        Task<Password> LoginPasswordAsync(string clientId, string clientSecret, User user);
    }
}
