using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlCConnection_ASP_Net_Core.Helper;
using SqlCConnection_ASP_Net_Core.Interfaces;
using SqlCConnection_ASP_Net_Core.Models;
using SqlCConnection_ASP_Net_Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TobitLogger.Core;
using TobitLogger.Core.Models;
using TobitWebApiExtensions.Extensions;

namespace SqlCConnection_ASP_Net_Core.Controllers
{
    [Produces("application/json")]
    [Route("companies")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepo _companyRepo;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyRepo companyRepo, ILoggerFactory loggerFactory)
        {
            _companyRepo = companyRepo;
            _logger = loggerFactory.CreateLogger<CompanyController>();
        }
        //--------------------------------------------------------------------------------------------------------
        [HttpGet()]
        public IActionResult Get()
        {
            List<Company> result;
            _logger.LogInformation("Get request started");
            try
            {
                result = _companyRepo.Get();
            }
            catch (Helper.RepoException<Helper.UpdateResultType> ex)
            {
                var logObj = new ExceptionData(ex);
                logObj.CustomNumber = 12345;
                logObj.CustomText = "get error";
                switch (ex.Type)
                {
                    case Helper.UpdateResultType.SQLERROR:
                        _logger.Error(logObj);
                        return StatusCode(StatusCodes.Status409Conflict);
                    case Helper.UpdateResultType.NOTFOUND:
                        _logger.Error(logObj);
                        return StatusCode(StatusCodes.Status404NotFound);
                    case Helper.UpdateResultType.ERROR:
                        _logger.Error(logObj);
                        return StatusCode(StatusCodes.Status400BadRequest);
                    default:
                        break;
                }
                return BadRequest();
                throw;
            }
            return Ok(result);
        }
        //--------------------------------------------------------------------------------------------------------
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Company result;
            _logger.LogInformation("Get request started");
            try
            {
                result = _companyRepo.GetById(id);
            }
            catch (Helper.RepoException<Helper.UpdateResultType> ex)
            {
                var logObj = new ExceptionData(ex);
                logObj.CustomNumber = 12345;
                logObj.CustomText = "getbyid error";
                switch (ex.Type)
                {
                    case Helper.UpdateResultType.SQLERROR:
                        _logger.Error(logObj);
                        return StatusCode(StatusCodes.Status406NotAcceptable);
                    case Helper.UpdateResultType.NOTFOUND:
                        _logger.Error(logObj);
                        return StatusCode(StatusCodes.Status404NotFound);
                    case Helper.UpdateResultType.INVALIDEARGUMENT:
                        _logger.Error(logObj);
                        return StatusCode(StatusCodes.Status409Conflict);
                    case Helper.UpdateResultType.ERROR:
                        _logger.Error(logObj);
                        return StatusCode(StatusCodes.Status400BadRequest);
                    default:
                        break;
                }
                return BadRequest();
                throw;
            }
            return Ok(result);
        }
        //--------------------------------------------------------------------------------------------------------
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Add([FromBody] CompanyDto companyDto)
        {
            _logger.LogInformation("Get request started");
            Company result;
                try
                {
                    result = _companyRepo.Create(companyDto);
                }
                catch (Helper.RepoException<Helper.UpdateResultType> ex)
                {
                    var logObj = new ExceptionData(ex);
                    logObj.CustomNumber = 12345;
                    logObj.CustomText = "getbyid error";
                    switch (ex.Type)
                    {
                        case Helper.UpdateResultType.SQLERROR:
                            _logger.Error(logObj);
                            return StatusCode(StatusCodes.Status406NotAcceptable);
                        case Helper.UpdateResultType.NOTFOUND:
                            _logger.Error(logObj);
                            return StatusCode(StatusCodes.Status404NotFound);
                        case Helper.UpdateResultType.INVALIDEARGUMENT:
                            _logger.Error(logObj);
                            return StatusCode(StatusCodes.Status409Conflict);
                        case Helper.UpdateResultType.ERROR:
                            _logger.Error(logObj);
                            return StatusCode(StatusCodes.Status400BadRequest);
                        default:
                            break;
                    }
                    return BadRequest();
                    throw;
                }
                return Ok(result);
        }
        //--------------------------------------------------------------------------------------------------------
        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] CompanyDto companyDto, int id)
        {
            _logger.LogInformation("Get request started");
            Company result;
                try
                {
                    result = _companyRepo.Update(companyDto, id);
                }
                catch (Helper.RepoException<Helper.UpdateResultType> ex)
                {
                    var logObj = new ExceptionData(ex);
                    logObj.CustomNumber = 12345;
                    logObj.CustomText = "update Error";
                    switch (ex.Type)
                    {
                        case Helper.UpdateResultType.SQLERROR:
                            _logger.Error(logObj);
                            return StatusCode(StatusCodes.Status406NotAcceptable);
                        case Helper.UpdateResultType.NOTFOUND:
                            _logger.Error(logObj);
                            return StatusCode(StatusCodes.Status404NotFound);
                        case Helper.UpdateResultType.INVALIDEARGUMENT:
                            _logger.Error(logObj);
                            return StatusCode(StatusCodes.Status409Conflict);
                        case Helper.UpdateResultType.ERROR:
                            _logger.Error(logObj);
                            return StatusCode(StatusCodes.Status400BadRequest);
                        default:
                            break;
                    }
                    return BadRequest();
                    throw;
                }
                return Ok(result);
        }
        //--------------------------------------------------------------------------------------------------------
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
                var result = _companyRepo.Delete(id);
                return Ok(result);            
        }

        

    }
}
