using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.Models;

namespace YourFinances.Authentication.Domain.Core.Interfaces.Repository
{
    public interface IAccountRepository
    {
        Task RegisterAccount(Account account, int userId);
    }
}
