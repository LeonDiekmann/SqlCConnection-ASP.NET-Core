using SqlCConnection_ASP_Net_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlCConnection_ASP_Net_Core.Interfaces
{
    public interface ICompanyRepo
    {
        List<Company> Get();
        Company GetById(int? id);
        Company Create(CompanyDto companyDto);
        Company Update(CompanyDto companyDto, int id);
        Company Delete(int id = -1);
    }
}