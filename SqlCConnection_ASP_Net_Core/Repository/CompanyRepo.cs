using SqlCConnection_ASP_Net_Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace SqlCConnection_ASP_Net_Core.Repository
{
    public class CompanyRepo
    {
        
        public List<Company> Read()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.connString))
            {
                string query = @"   SELECT 
                                    Id, 
                                    Name, 
                                    PostCode, 
                                    City, 
                                    Street, 
                                    HouseNumber, 
                                    Country FROM [dbo].[viCompany]";

                var companyList = conn.Query<Company>(query).ToList();
                return companyList;
            }

        }

        public Company ReadByID(int id)
        {
            if (id < 1)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.INVALIDEARGUMENT);
            }
            using (SqlConnection conn = new SqlConnection(Properties.Resources.connString))
            {
                string query = @"   SELECT 
                                    Id, 
                                    Name, 
                                    PostCode, 
                                    City, 
                                    Street, 
                                    HouseNumber, 
                                    Country FROM [dbo].[viCompany] WHERE Id = @Id";
                var param = new DynamicParameters();
                param.Add("@Id", id);

                var company = conn.QueryFirstOrDefault<Company>(query, param);

                return company;
            }
        }

        public Company Create(CompanyDto companyDto)
        {
            return AddOrUpdate(companyDto);
        }

        public Company Update (CompanyDto companyDto, int id )
        {
            return AddOrUpdate(companyDto, id);
        }

        

        private static Company AddOrUpdate(CompanyDto companyDto, int id = -1)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.connString))
            {
                string companySp = "spCompany";

                var param = new DynamicParameters();
                param.Add("@Id", id);
                param.Add("@Name", companyDto.Name);

                var companyAdd = conn.QueryFirstOrDefault<Company>(companySp, param, null, null, CommandType.StoredProcedure);
                
                return companyAdd;                
            }
        }

        public Company Delete(int id = -1)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.connString))
            {
                string companySp = "spCompanyDelete";

                var param = new DynamicParameters();
                param.Add("@Id", id);

                var companyResult = conn.QueryFirstOrDefault<Company>(companySp, param, null, null, CommandType.StoredProcedure);

                return companyResult;
            }
        }
    }
}
