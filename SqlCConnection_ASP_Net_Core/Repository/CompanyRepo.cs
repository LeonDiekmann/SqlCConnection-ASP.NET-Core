using SqlCConnection_ASP_Net_Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using SqlCConnection_ASP_Net_Core.Interfaces;

namespace SqlCConnection_ASP_Net_Core.Repository
{
    public class CompanyRepo : ICompanyRepo
    {
        IDbContext _dbContext;
        IMessageHelper _messageHelper;
        IGroupHelper _groupHelper;
        public CompanyRepo(IDbContext dbContext, IMessageHelper messageHelper, IGroupHelper groupHelper)
        {
            _dbContext = dbContext;
            _messageHelper = messageHelper;
            _groupHelper = groupHelper;
        }
        public List<Company> Get()
        {
            List<Company> companyList;
            var con = _dbContext.GetCompany();
            try
            {
                using (con)
                {
                    string query = @"   SELECT 
                                    Id, 
                                    Name, 
                                    PostCode, 
                                    City, 
                                    Street, 
                                    HouseNumber, 
                                    Country FROM [dbo].[viCompany]";

                    companyList = con.Query<Company>(query).ToList();
                    _groupHelper.GetGroups();
                    _groupHelper.GetMembers(1);
                    _groupHelper.GetGroupsOfMember(1808455);
                }
            }
            catch (Exception)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.SQLERROR);
            }
            if (companyList == null)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.NOTFOUND);
            }

            return companyList;

        }
        //--------------------------------------------------------------------------------------------------------
        public Company GetById(int? id)
        {
            Company company;
            var con = _dbContext.GetCompany();
            if (id < 1)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.INVALIDEARGUMENT);
            }
            try
            {
                using (con)
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

                    company = con.QueryFirstOrDefault<Company>(query, param);
                    _messageHelper.SendIntercom($"Deine Anfrage hat folgendes ergeben:\n" +
                                                                                        $"ID: {company.Id}\n" +
                                                                                        $"Name: {company.Name}\n" +
                                                                                        $"Postleitzahl: {company.PostCode}\n" +
                                                                                        $"Stadt: {company.City}\n" +
                                                                                        $"Straße: {company.Street}\n" +
                                                                                        $"Hausnummer: {company.HouseNumber}\n" +
                                                                                        $"Land: {company.Country}");
                }
            }
            catch (SqlException)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.SQLERROR);
            }
            catch (Exception)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.ERROR);
            }
            if (company == null)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.NOTFOUND);
            }
            return company;
        }

        public Company Create(CompanyDto companyDto)
        {
            return AddOrUpdate(companyDto);
        }

        public Company Update (CompanyDto companyDto, int id )
        {
            
            return AddOrUpdate(companyDto, id);
        }


        //--------------------------------------------------------------------------------------------------------
        private Company AddOrUpdate(CompanyDto companyDto, int id = -1)
        {
            if (id != -1 && id < 1)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.INVALIDEARGUMENT);
            }

            int? returnId;
            var con = _dbContext.GetCompany();
            try
            {
                using (con)
                {
                    string companySp = "spCompany";

                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    param.Add("@Name", companyDto.Name);
                    param.Add("@DBId", DbType.Int32, direction: ParameterDirection.ReturnValue);

                    var companyAdd = con.Execute(companySp, param, null, null, CommandType.StoredProcedure);
                    returnId = param.Get<int>("@DBId");
                    
                }
            }
            catch (SqlException)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.SQLERROR);
            }
            catch (Exception)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.ERROR);
            }
            if (returnId == null)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.NOTFOUND);
            }
            return GetById(returnId);
        }
        
        //--------------------------------------------------------------------------------------------------------
        public Company Delete(int id = -1)
        {
            Company companyResult;
            var con = _dbContext.GetCompany();
            if (id != -1 && id < 1)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.INVALIDEARGUMENT);
            }
            try
            {
                using (con)
                {
                    string companySp = "spCompanyDelete";

                    var param = new DynamicParameters();
                    param.Add("@Id", id);

                    companyResult = con.QueryFirstOrDefault<Company>(companySp, param, null, null, CommandType.StoredProcedure);

                    
                }
            }
            catch (SqlException)
            {

                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.SQLERROR);
            }
            catch (Exception)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.ERROR);
            }
            if (companyResult == null)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.NOTFOUND);
            }
            return companyResult;
        }

        
    }
}
