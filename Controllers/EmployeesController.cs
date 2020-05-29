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

        // GET: api/Employee
        [HttpGet]
        public ActionResult<List<Employee>> GetAll()
        {
            var employees = context.Employees;
            List<Employee> list_employees = new List<Employee>();

            if (employees == null)
            {
                return NotFound();
            }
            foreach (var employee in employees)
            {


                list_employees.Add(employee);

            }

            return list_employees;
        }
    }
}