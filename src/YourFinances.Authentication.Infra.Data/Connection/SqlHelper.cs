using Dapper;
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

        //public async Task<T> ReadProcAsync<T>(string proc, object param)
        //{
        //    using (var con = await OpenConnetctionAsync())
        //    {
        //        using (var multi = await con.QueryMultipleAsync(proc, param, commandType: System.Data.CommandType.StoredProcedure))
        //        {
        //            var data1 = (await multi.ReadFirstOrDefaultAsync());
        //            var data2 = (await multi.ReadFirstOrDefaultAsync());
        //        }
        //    }
        //}
    }
}
