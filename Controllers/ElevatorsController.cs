using System;
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
    public class ElevatorsController : ControllerBase
    {
        private readonly RestApiContext context;

        public ElevatorsController(RestApiContext context)
        {
            this.context = context;
        }

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevators()
        {
            return await this.context.Elevators.ToListAsync();
        }

        // GET: api/Elevators/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevator([FromRoute] long id)
        {
            var myElevator = await this.context.Elevators.FindAsync(id);

            if (myElevator == null)
            {
                return NotFound();
            }

            return myElevator;
        }

        // GET: api/Elevators/{id}/status
        [HttpGet("{id}/status")]
        public async Task<ActionResult<string>> GetElevatorStatus([FromRoute] long id)
        {
            var myElevator = await this.context.Elevators.FindAsync(id);

            if (myElevator == null)
            {
                return NotFound();
            }

            return myElevator.status;
        }
        // GET: api/Elevators/ListNonOperational
        [HttpGet("ListNonOperational")]
        public List<Elevator> GetElevatorsListNonOperational()
        {
            var elevators = this.context.Elevators.Where(e => e.status != "active").ToList();
            return elevators;
        }

        // POST: api/Elevators/{id}/status
        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateElevatorStatus([FromRoute] long id, [FromBody] UpdateStatusPayload payload)
        {
            var myElevator = await this.context.Elevators.FindAsync(id);

            if (myElevator == null)
            {
                return NotFound();
            } 
            
            myElevator.status = payload.active ? "active" : "inactive";

            this.context.Elevators.Update(myElevator);
            await this.context.SaveChangesAsync();

            return NoContent();
        }
    }
}