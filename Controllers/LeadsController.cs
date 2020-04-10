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
    public class LeadsController : ControllerBase
    {
        private readonly RestApiContext context;

        public LeadsController(RestApiContext context)
        {
            this.context = context;
        }

        // GET: api/Leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lead>>> GetLeads()
        {
            return await this.context.Leads.ToListAsync();
        }

        // GET: api/Leads/LatestLeads
        [HttpGet("LatestLeads")]
        public List<Lead> GetLatestLeads()
        {
            var rightnow = DateTime.UtcNow;
            var leads = this.context.Leads
                .Where(l => l.customer_id == null)
                .Where(l => l.created_at.Year == rightnow.Year)
                .Where(l => l.created_at.DayOfYear <= rightnow.DayOfYear && l.created_at.DayOfYear >= rightnow.DayOfYear-30)
                .ToList();
            // var leads = this.context.Leads.Where(l => l.customer_id == null && LastThirtyDays(l)).ToList();
            return leads;
        }

        // private bool LastThirtyDays(Lead l)
        // {
        //     var rightnow = DateTime.UtcNow;
        //     var daynow = rightnow.DayOfYear;
        //     var yearnow = rightnow.Year;
        //     var daylead = l.created_at.DayOfYear;
        //     var yearlead = l.created_at.Year;
        //     if (daynow <= 30)
        //     {
        //         if (yearlead == yearnow)
        //         {
        //             if (daylead <= daynow && daylead > 0)
        //             {
        //                 return true;
        //             }
        //         }
        //         else if (yearlead == yearnow-1)
        //         {
        //             if (daylead-365 <= daynow && daylead-365 >= daynow-30 )
        //             {
        //                 return true;
        //             }
        //         }
        //     }
        //     else
        //     {
        //         if (yearlead == yearnow)
        //         {
        //             if (daylead <= daynow && daylead >= daynow-30)
        //             {
        //                 return true;
        //             }
        //         }
        //     }
        //     return false;
        // }
    }   
}