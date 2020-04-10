using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restapi.Contexts;
// using restapi.Models;
using restapi.Payloads;

namespace restapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly RestApiContext context;

        public BuildingsController(RestApiContext context)
        {
            this.context = context;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
        {
            return await this.context.Buildings.ToListAsync();
        }

        // GET: api/Buildings/ListWithInterventions
        [HttpGet("ListWithInterventions")]
        public List<Building> GetBuildingsIntervention()
        {
            var buildings = this.context.Buildings.Where(b => b.Batteries.Any(bat => bat.status == "intervention" || bat.Columns.Any(c => c.status == "intervention" || c.Elevators.Any(e => e.status == "intervention")))).ToList();
            return buildings;
        }
    }   
}