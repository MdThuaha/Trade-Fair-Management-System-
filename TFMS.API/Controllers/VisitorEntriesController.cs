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
    public class VisitorEntriesController : ControllerBase
    {
        private readonly FairDbContext db;

        public VisitorEntriesController(FairDbContext context)
        {
            db = context;
        }

        // GET: api/VisitorEntries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitorEntry>>> GetVisitorEntry()
        {
            return await db.VisitorEntry.ToListAsync();
        }

        // GET: api/VisitorEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitorEntry>> GetVisitorEntry(int id)
        {
            var visitorEntry = await db.VisitorEntry.FindAsync(id);

            if (visitorEntry == null)
            {
                return NotFound();
            }

            return visitorEntry;
        }

        // PUT: api/VisitorEntries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitorEntry(int id, VisitorEntry visitorEntry)
        {
            if (id != visitorEntry.VisitorEntryId)
            {
                return BadRequest();
            }

            db.Entry(visitorEntry).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorEntryExists(id))
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

        // POST: api/VisitorEntries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VisitorEntry>> PostVisitorEntry(VisitorEntry visitorEntry)
        {
            db.VisitorEntry.Add(visitorEntry);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetVisitorEntry", new { id = visitorEntry.VisitorEntryId }, visitorEntry);
        }

        // DELETE: api/VisitorEntries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitorEntry(int id)
        {
            var visitorEntry = await db.VisitorEntry.FindAsync(id);
            if (visitorEntry == null)
            {
                return NotFound();
            }

            db.VisitorEntry.Remove(visitorEntry);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool VisitorEntryExists(int id)
        {
            return db.VisitorEntry.Any(e => e.VisitorEntryId == id);
        }
    }
}
