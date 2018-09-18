﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlCConnection_ASP_Net_Core.Helper;
using SqlCConnection_ASP_Net_Core.Interfaces;
using SqlCConnection_ASP_Net_Core.Models;
using SqlCConnection_ASP_Net_Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlCConnection_ASP_Net_Core.Controllers
{
    [Produces("application/json")]
    [Route("companies")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepo _companyRepo;

        public CompanyController(ICompanyRepo companyRepo)
        {
            _companyRepo = companyRepo;
        }
        //static CompanyRepo _companyRepo;
        //public static CompanyRepo GetInstance()
        //{
        //    if (_companyRepo == null)
        //        _companyRepo = new CompanyRepo();
        //    return _companyRepo;
        //}
        //--------------------------------------------------------------------------------------------------------
        [HttpGet()]
        public IActionResult Get()
        {
            
            List<Company> result;

            try
            {
                result = _companyRepo.Get();

            }
            catch (Helper.RepoException<Helper.UpdateResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.UpdateResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    case Helper.UpdateResultType.NOTFOUND:
                        return StatusCode(StatusCodes.Status404NotFound);
                    case Helper.UpdateResultType.ERROR:
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
            try
            {
                result = _companyRepo.GetById(id);
            }
            catch (Helper.RepoException<Helper.UpdateResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.UpdateResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status406NotAcceptable);
                    case Helper.UpdateResultType.NOTFOUND:
                        return StatusCode(StatusCodes.Status404NotFound);
                    case Helper.UpdateResultType.INVALIDEARGUMENT:
                        return StatusCode(StatusCodes.Status409Conflict);
                    case Helper.UpdateResultType.ERROR:
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
        [HttpPost]
        public IActionResult Add([FromBody] CompanyDto companyDto)
        {
            var authValue = Request.Headers["authorization"].ToString();
            bool auth;
            try
            {
                auth = Authorization.decode(authValue);
            }
            catch (Helper.RepoException<Helper.UpdateResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.UpdateResultType.INVALIDEARGUMENT:
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    default:
                        break;
                }
                return BadRequest();
                throw;
            }
            Company result;
            
            if (auth == true)
            {
                try
                {
                    result = _companyRepo.Create(companyDto);
                }
                catch (Helper.RepoException<Helper.UpdateResultType> ex)
                {
                    switch (ex.Type)
                    {
                        case Helper.UpdateResultType.SQLERROR:
                            return StatusCode(StatusCodes.Status406NotAcceptable);
                        case Helper.UpdateResultType.NOTFOUND:
                            return StatusCode(StatusCodes.Status404NotFound);
                        case Helper.UpdateResultType.INVALIDEARGUMENT:
                            return StatusCode(StatusCodes.Status409Conflict);
                        case Helper.UpdateResultType.ERROR:
                            return StatusCode(StatusCodes.Status400BadRequest);
                        default:
                            break;
                    }
                    return BadRequest();
                    throw;
                }
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
        //--------------------------------------------------------------------------------------------------------
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] CompanyDto companyDto, int id)
        {
            var authValue = Request.Headers["authorization"].ToString();
            bool auth;
            try
            {
                auth = Authorization.decode(authValue);
            }
            catch (Helper.RepoException<Helper.UpdateResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.UpdateResultType.INVALIDEARGUMENT:
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    default:
                        break;
                }
                return BadRequest();
                throw;
            }
            Company result;
            if (auth == true)
            {
                try
                {
                    result = _companyRepo.Update(companyDto, id);
                }
                catch (Helper.RepoException<Helper.UpdateResultType> ex)
                {
                    switch (ex.Type)
                    {
                        case Helper.UpdateResultType.SQLERROR:
                            return StatusCode(StatusCodes.Status406NotAcceptable);
                        case Helper.UpdateResultType.NOTFOUND:
                            return StatusCode(StatusCodes.Status404NotFound);
                        case Helper.UpdateResultType.INVALIDEARGUMENT:
                            return StatusCode(StatusCodes.Status409Conflict);
                        case Helper.UpdateResultType.ERROR:
                            return StatusCode(StatusCodes.Status400BadRequest);
                        default:
                            break;
                    }
                    return BadRequest();
                    throw;
                }
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
        //--------------------------------------------------------------------------------------------------------
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var authValue = Request.Headers["authorization"].ToString();
            bool auth;
            try
            {
                auth = Authorization.decode(authValue);
            }
            catch (Helper.RepoException<Helper.UpdateResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.UpdateResultType.INVALIDEARGUMENT:
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    default:
                        break;
                }
                return BadRequest();
                throw;
            }
            if (auth == true)
            {
                var result = _companyRepo.Delete(id);
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            
        }

        

    }
}
