using Microsoft.Extensions.Options;
using SqlCConnection_ASP_Net_Core.Interfaces;
using SqlCConnection_ASP_Net_Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SqlCConnection_ASP_Net_Core.Helper
{
    public class DbContext : IDbContext
    {
        private readonly DbSettings _settings;
        public DbContext(IOptions<DbSettings> options)
        {
            _settings = options.Value;
        }
        public IDbConnection GetCompany()
        {
            var con = new SqlConnection(_settings.Company);
            con.Open();
            return con;
        }
    }
}
