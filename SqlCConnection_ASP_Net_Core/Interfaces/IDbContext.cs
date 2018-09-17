using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SqlCConnection_ASP_Net_Core.Models;

namespace SqlCConnection_ASP_Net_Core.Interfaces
{
    public interface IDbContext
    {
        IDbConnection GetCompany();
    }
}
