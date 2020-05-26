using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.Models;

namespace YourFinances.Authentication.Domain.Core.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task BasicRegisterAsync(User user);
    }
}
