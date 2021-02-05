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
                user.SetId(await con.ExecuteScalarAsync<int>("User_Basic_Register_sp", new
                {
                    user.Password,
                    user.Email,
                    user.Identification,
                    user.UserEditionId,
                    user.AccountId
                }, commandType: System.Data.CommandType.StoredProcedure));
            }
        }

        public async Task<int> KeepConnectedAsync(bool connected, int userId)
        {
            var sql = "UPDATE [USER] SET [KeepConnected]=@KeepConnected WHERE [ID]=@Id";
            int result = 0;

            using (var con = await _sql.OpenConnetctionAsync())
            {
                result = await con.ExecuteAsync(sql, new
                {
                    KeepConnected = connected ? 1 : 0,
                    Id = userId
                });
            }

            return result;
        }

        public async Task<int> AccepTermAsync(int userId)
        {
            var sql = "UPDATE [USER] SET [AcceptTerm]=@AcceptTerm WHERE [ID]=@Id";
            int result = 0;

            using (var con = await _sql.OpenConnetctionAsync())
            {
                result = await con.ExecuteAsync(sql, new
                {
                    AcceptTerm = 1,
                    Id = userId
                });
            }

            return result;
        }

    }
}
