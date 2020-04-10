using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restapi.Contexts;
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
            return await this.context.Interventions
                .Where(intervention => intervention.start_date == null 
                    && intervention.status == "Pending")
                .ToListAsync();
        }

        // POST: api/Interventions/{id}/status
        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateInterventionStatus([FromRoute] long id, [FromBody] UpdateInterventionsPayload payload)
        {
            var myIntervention = await this.context.Interventions.FindAsync(id);

            if (myIntervention == null)
            {
                return NotFound();
            } 
            
            if(payload.status.Equals("InProgress", System.StringComparison.InvariantCultureIgnoreCase))
            {
                myIntervention.start_date = System.DateTime.Now;
                myIntervention.status = "InProgress";
            }
            else if(payload.status.Equals("Completed", System.StringComparison.InvariantCultureIgnoreCase))
            {
                myIntervention.end_date = System.DateTime.Now;
                myIntervention.status = "Completed";
            }
            else
            {
                return BadRequest();
            }

            this.context.Interventions.Update(myIntervention);
            await this.context.SaveChangesAsync();

            return NoContent();
        }
    }
}