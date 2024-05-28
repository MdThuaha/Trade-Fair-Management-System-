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
    public class ExhibitorPassesController : ControllerBase
    {
        private readonly FairDbContext db;

        public ExhibitorPassesController(FairDbContext context)
        {
            db = context;
        }

        // GET: api/ExhibitorPasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExhibitorPass>>> GetexhibitorPasses()
        {
            return await db.exhibitorPasses.ToListAsync();
        }

        // GET: api/ExhibitorPasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExhibitorPass>> GetExhibitorPass(int id)
        {
            var exhibitorPass = await db.exhibitorPasses.FindAsync(id);

            if (exhibitorPass == null)
            {
                return NotFound();
            }

            return exhibitorPass;
        }

        // PUT: api/ExhibitorPasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExhibitorPass(int id, ExhibitorPass exhibitorPass)
        {
            if (id != exhibitorPass.ExhibitorPassId)
            {
                return BadRequest();
            }

            db.Entry(exhibitorPass).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExhibitorPassExists(id))
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

        // POST: api/ExhibitorPasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExhibitorPass>> PostExhibitorPass(ExhibitorPass exhibitorPass)
        {
            db.exhibitorPasses.Add(exhibitorPass);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetExhibitorPass", new { id = exhibitorPass.ExhibitorPassId }, exhibitorPass);
        }

        // DELETE: api/ExhibitorPasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExhibitorPass(int id)
        {
            var exhibitorPass = await db.exhibitorPasses.FindAsync(id);
            if (exhibitorPass == null)
            {
                return NotFound();
            }

            db.exhibitorPasses.Remove(exhibitorPass);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ExhibitorPassExists(int id)
        {
            return db.exhibitorPasses.Any(e => e.ExhibitorPassId == id);
        }
    }
}
