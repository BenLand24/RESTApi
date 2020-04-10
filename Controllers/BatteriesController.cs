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
    public class BatteriesController : ControllerBase
    {
        private readonly RestApiContext context;

        public BatteriesController(RestApiContext context)
        {
            this.context = context;
        }

        // GET: api/Batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
        {
            return await this.context.Batteries.ToListAsync();
        }

        // GET: api/Batteries/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Battery>> GetBattery(long id)
        {
            var myBattery = await this.context.Batteries.FindAsync(id);

            if (myBattery == null)
            {
                return NotFound();
            }

            return myBattery;
        }

        // GET: api/Batteries/{id}/status
        [HttpGet("{id}/Status")]
        public async Task<ActionResult<string>> GetBatteryStatus([FromRoute] long id)
        {
            var myBattery = await this.context.Batteries.FindAsync(id);

            if (myBattery == null)
            {
                return NotFound();
            }

            return myBattery.status;
        }

        // POST: api/Batteries/{id}/status
        [HttpPut("{id}/Status")]
        public async Task<ActionResult> UpdateBatteryStatus([FromRoute] long id, [FromBody] UpdateStatusPayload payload)
        {
            var myBattery = await this.context.Batteries.FindAsync(id);

            if (myBattery == null)
            {
                return NotFound();
            } 
            
            myBattery.status = payload.active ? "active" : "inactive";

            this.context.Batteries.Update(myBattery);
            await this.context.SaveChangesAsync();

            return NoContent();
        }
    }
}