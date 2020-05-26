using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace YourFinances.Authentication.Domain.Core.Interfaces.Connection
{
    public interface ISqlHelper
    {
        SqlConnection OpenConnetction();
        Task<SqlConnection> OpenConnetctionAsync();
    }
}
