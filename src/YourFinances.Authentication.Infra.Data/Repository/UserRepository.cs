using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.Interfaces.Connection;
using YourFinances.Authentication.Domain.Core.Models;
using Dapper;
using YourFinances.Authentication.Domain.Core.Interfaces.Repository;

namespace YourFinances.Authentication.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlHelper _sql;

        public UserRepository(ISqlHelper sql)
        {
            _sql = sql;
        }

        public async Task BasicRegisterAsync(User user)
        { 
            using (var con = await _sql.OpenConnetctionAsync())
            {
               user.SetId(await con.ExecuteScalarAsync<int>("sp_User_Basic_Register", new 
               {
                   Password= user.Password, 
                   Email=user.Email,
                   Identification= user.Identification
               }, commandType: System.Data.CommandType.StoredProcedure));
            }
        }

    }
}
