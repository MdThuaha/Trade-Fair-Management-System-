using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFMS.Lib.Models;

namespace TFMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizersController : ControllerBase
    {
        private readonly FairDbContext db;
        public OrganizersController(FairDbContext context)
        {
            db = context;
        }

        // GET: api/Organizers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organizer>>> GetOrganizers()
        {
            return await db.Organizers.ToListAsync();
        }

        [HttpGet("Fairs/Include")]
        public async Task<ActionResult<IEnumerable<Organizer>>> GetOrganizersWithFair()
        {
            return await db.Organizers
                .Include(x => x.Fairs)
                .ToListAsync();
        }

        // GET: api/Organizers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organizer>> GetOrganizer(int id)
        {
            var organizer = await db.Organizers.FindAsync(id);

            if (organizer == null)
            {
                return NotFound();
            }

            return organizer;
        }

        [HttpGet("{id}/Include")]
        public async Task<ActionResult<Organizer>> GetOrganizerWithFairs(int id)
        {
            var organizer = await db.Organizers
                .Include(x => x.Fairs)
                .FirstOrDefaultAsync(x=>x.OrganizerId==id);

            if (organizer == null)
            {
                return NotFound();
            }

            return organizer;
        }

        // Fairs data of
        //===========================================
        [HttpGet("Fairs/Of/{id}")]
        public async Task<ActionResult<IEnumerable<Fair>>> GetFairOfOrganizer(int id)
        {
            var data = await db.Fairs.Where(x => x.OrganizerId == id).ToListAsync();
            return data;
        }



        // PUT: api/Organizers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizer(int id, Organizer organizer)
        {
            if (id != organizer.OrganizerId)
            {
                return BadRequest();
            }

            var c = db.Organizers.Include(x => x.Fairs).FirstOrDefault(x => x.OrganizerId == id);
            // Update only the properties you want to allow updating
            c.OrganizerName = organizer.OrganizerName;
            c.OrganizerEmail = organizer.OrganizerEmail;
            c.OrganizerPhone = organizer.OrganizerPhone;
            c.WebSiteUrl = organizer.WebSiteUrl;
            c.Fairs.Clear();
            foreach (var p in organizer.Fairs)
            {
                c.Fairs.Add(p);
            }


            try
            {
                db.Entry(organizer).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Organizers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Organizer>> PostOrganizer(Organizer organizer)
        {
            db.Organizers.Add(organizer);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetOrganizer", new { id = organizer.OrganizerId }, organizer);
        }

        // DELETE: api/Organizers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizer(int id)
        {
            var organizer = await db.Organizers.FindAsync(id);
            if (organizer == null)
            {
                return NotFound();
            }

            db.Organizers.Remove(organizer);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizerExists(int id)
        {
            return db.Organizers.Any(e => e.OrganizerId == id);
        }
    }
}
