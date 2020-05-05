using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restapi.Contexts;
using restapi.Payloads;

namespace restapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly RestApiContext context;

        public EmployeesController(RestApiContext context)
        {
            this.context = context;
        }

        // GET: api/Employees/Authenticate
        [HttpGet("Authenticate")]
        public async Task<ActionResult<Employee>> AuthenticateEmployee([FromBody] VerifyEmployeePayload payload)
        {
            var myEmployee = await this.context.Employees.Where(x => x.email.Equals(payload.email)).FirstOrDefaultAsync();
            if(myEmployee != null)
            {
                return myEmployee;
            }
            else
            {
                return NotFound();
            }
        }
    }
}