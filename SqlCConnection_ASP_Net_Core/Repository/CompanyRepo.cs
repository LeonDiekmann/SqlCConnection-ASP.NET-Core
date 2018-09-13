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
        public static List<Company> Read()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.connString))
            {
                string query = @"   SELECT Id, 
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

        public static Company ReadByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.connString))
            {
                string query = @"   SELECT Id, 
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

        public static Company AddOrUpdate(Company company)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.connString))
            {
                string companySelect = "spCompany";

                var param = new DynamicParameters();
                param.Add("@Id", company.Id);
                param.Add("@Name", company.Name);

                var companyResult = conn.QueryFirstOrDefault<Company>(companySelect, param, null, null, CommandType.StoredProcedure);

                return companyResult;
            }
        }
    }
}
