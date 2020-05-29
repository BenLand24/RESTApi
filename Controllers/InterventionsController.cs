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
    public class InterventionsController : ControllerBase
    {
        private readonly RestApiContext context;

        public InterventionsController(RestApiContext context)
        {
            this.context = context;
        }

        // GET: api/Interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventions()
        {
            return await this.context.Interventions.ToListAsync();
        }

        // GET: api/Interventions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetInterventions([FromRoute] long id)
        {
            var myIntervention = await this.context.Interventions.FindAsync(id);

            if (myIntervention == null)
            {
                return NotFound();
            }

            return myIntervention;
        }

        // GET: api/Interventions/Pending
        [HttpGet("Pending")]
        public List<Intervention> GetInterventionsStatus()
        {
            var interventions = this.context.Interventions.Where(i => i.status == "Pending" && i.start_date == null).ToList();
            return interventions;
        }

        // PUT: api/Interventions/{id}/ChangeStatus
        [HttpPut("{id}/ChangeStatus")]
        public async Task<ActionResult<Intervention>> UpdateInterventionsStatus([FromRoute] long id, [FromBody] UpdateInterventionsPayload payload)
        {
            var myIntervention = await this.context.Interventions.FindAsync(id);

            if (myIntervention == null)
            {
                return NotFound();
            }
            if (payload.status == "InProgress")
            {
                myIntervention.status = payload.status;
                myIntervention.start_date = DateTime.Now;
            }
            else if (payload.status == "Completed")
            {
                myIntervention.status = payload.status;
                myIntervention.end_date = DateTime.Now;
            }
            else
            {
                return BadRequest();
            }
            this.context.Interventions.Update(myIntervention);
            await this.context.SaveChangesAsync();

            return await this.context.Interventions.FindAsync(id);
        }
    }
}