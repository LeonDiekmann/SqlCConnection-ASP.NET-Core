using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlCConnection_ASP_Net_Core.Models;
using SqlCConnection_ASP_Net_Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlCConnection_ASP_Net_Core.Controllers
{
    [Route("companies")]
    public class CompanyController : Controller
    {
        static CompanyRepo _companyRepo;
        public static CompanyRepo GetInstance()
        {
            if (_companyRepo == null)
                _companyRepo = new CompanyRepo();
            return _companyRepo;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            var result = GetInstance().Read();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = GetInstance().ReadByID(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CompanyDto companyDto)
        {
            Company result;

            try
            {
                result = GetInstance().Create(companyDto);
            }
            catch (Helper.RepoException<Helper.UpdateResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.UpdateResultType.OK:
                        break;
                    case Helper.UpdateResultType.SQLERROR:
                        break;
                    case Helper.UpdateResultType.NOTFOUND:
                        break;
                    case Helper.UpdateResultType.INVALIDEARGUMENT:
                        return StatusCode(StatusCodes.Status409Conflict);
                    case Helper.UpdateResultType.ERROR:
                        break;
                    default:
                        break;
                }
                return BadRequest();
                throw;
            }
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] CompanyDto companyDto, int id)
        {
            var result = GetInstance().Update(companyDto, id);
            if (result == null)
            {
                BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = GetInstance().Delete(id);
            return Ok(result);
        }

        

    }
}
