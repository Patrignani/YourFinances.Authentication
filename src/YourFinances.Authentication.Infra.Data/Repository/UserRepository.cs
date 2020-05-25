using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.Interfaces.Connection;
using YourFinances.Authentication.Domain.Core.Models;

namespace YourFinances.Authentication.Infra.Data.Repository
{
    public class UserRepository
    {
        private readonly ISqlHelper _sql;

        public UserRepository(ISqlHelper sql)
        {
            _sql = sql;
        }

        public async Task Register(User user)
        { 
        
        }

    }
}
