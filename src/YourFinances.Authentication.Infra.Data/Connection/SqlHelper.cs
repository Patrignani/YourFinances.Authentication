using Microsoft.Win32.SafeHandles;
using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using YourFinances.Authentication.Domain.Core.Interfaces.Connection;

namespace YourFinances.Authentication.Infra.Data.Connection
{
    internal class SqlHelper : ISqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(string connection)
        {
            _connectionString = connection;
        }

        public SqlConnection OpenConnetction()
        {
            var con = new SqlConnection(_connectionString);
            con.Open();

            return con;
        }

        public async Task<SqlConnection> OpenConnetctionAsync()
        {
            var con  = new SqlConnection(_connectionString);
            await con.OpenAsync();

            return con;
        }
    }
}
