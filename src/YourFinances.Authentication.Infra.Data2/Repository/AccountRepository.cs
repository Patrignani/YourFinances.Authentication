using Dapper;
using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.Interfaces.Connection;
using YourFinances.Authentication.Domain.Core.Interfaces.Repository;
using YourFinances.Authentication.Domain.Core.Models;

namespace YourFinances.Authentication.Infra.Data.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly ISqlHelper _sql;

        public AccountRepository(ISqlHelper sql)
        {
            _sql = sql;
        }

        public async Task RegisterAccount(Account account, int userId)
        {
            using (var con = await _sql.OpenConnetctionAsync())
            {
                account.SetId(await con.ExecuteScalarAsync<int>("Account_Create_sp", new
                {
                    account.Identification,
                    account.Active,
                    UserId = userId
                }, commandType: System.Data.CommandType.StoredProcedure));
            }
        }

    }
}
