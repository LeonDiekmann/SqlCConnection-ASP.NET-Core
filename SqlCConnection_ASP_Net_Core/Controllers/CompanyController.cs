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
        [HttpGet()]
        public IActionResult Get()
        {
            var result = CompanyRepo.Read();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = CompanyRepo.ReadByID(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Company company)
        {
            var result = CompanyRepo.AddOrUpdate(company);
            if (result == null)
            {

            }
            return Ok(result);
        }

    }
}
