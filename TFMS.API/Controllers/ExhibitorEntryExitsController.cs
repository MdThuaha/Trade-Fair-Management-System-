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
    public class ExhibitorEntryExitsController : ControllerBase
    {
        private readonly FairDbContext db;

        public ExhibitorEntryExitsController(FairDbContext context)
        {
            db = context;
        }

        // GET: api/ExhibitorEntryExits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExhibitorEntryExit>>> GetExhibitorEntryExits()
        {
            return await db.ExhibitorEntryExits.ToListAsync();
        }

        // GET: api/ExhibitorEntryExits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExhibitorEntryExit>> GetExhibitorEntryExit(int id)
        {
            var exhibitorEntryExit = await db.ExhibitorEntryExits.FindAsync(id);

            if (exhibitorEntryExit == null)
            {
                return NotFound();
            }

            return exhibitorEntryExit;
        }

        // PUT: api/ExhibitorEntryExits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExhibitorEntryExit(int id, ExhibitorEntryExit exhibitorEntryExit)
        {
            if (id != exhibitorEntryExit.ExhibitorEntryExitId)
            {
                return BadRequest();
            }

            db.Entry(exhibitorEntryExit).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExhibitorEntryExitExists(id))
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

        // POST: api/ExhibitorEntryExits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExhibitorEntryExit>> PostExhibitorEntryExit(ExhibitorEntryExit exhibitorEntryExit)
        {
            db.ExhibitorEntryExits.Add(exhibitorEntryExit);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetExhibitorEntryExit", new { id = exhibitorEntryExit.ExhibitorEntryExitId }, exhibitorEntryExit);
        }

        // DELETE: api/ExhibitorEntryExits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExhibitorEntryExit(int id)
        {
            var exhibitorEntryExit = await db.ExhibitorEntryExits.FindAsync(id);
            if (exhibitorEntryExit == null)
            {
                return NotFound();
            }

            db.ExhibitorEntryExits.Remove(exhibitorEntryExit);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ExhibitorEntryExitExists(int id)
        {
            return db.ExhibitorEntryExits.Any(e => e.ExhibitorEntryExitId == id);
        }
    }
}
