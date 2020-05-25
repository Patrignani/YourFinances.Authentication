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
        private SqlConnection _con;
        private bool _disposed = false;
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _con.Close();
                _con.Dispose();
                _safeHandle?.Dispose();
            }

            _disposed = true;
        }

        public void Dispose() => Dispose(true);

        public SqlHelper(string connection)
        {
            _connectionString = connection;
        }

        public SqlConnection OpenConnetction()
        {
            _con= new SqlConnection(_connectionString);
            _con.Open();

            return _con;
        }

        public async Task<SqlConnection> OpenConnetctionAsync()
        {
            _con = new SqlConnection(_connectionString);
           await  _con.OpenAsync();

            return _con;
        }
    }
}
