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
    public class FairsController : ControllerBase
    {
        private readonly FairDbContext db;

        public FairsController(FairDbContext context)
        {
            db = context;
        }

        // GET: api/Fairs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fair>>> GetFairs()
        {
            return await db.Fairs.ToListAsync();
        }

        // GET: api/Fairs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fair>> GetFair(int id)
        {
            var fair = await db.Fairs.FindAsync(id);

            if (fair == null)
            {
                return NotFound();
            }

            return fair;
        }

        // PUT: api/Fairs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFair(int id, Fair fair)
        {
            if (id != fair.FairId)
            {
                return BadRequest();
            }

            db.Entry(fair).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FairExists(id))
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

        // POST: api/Fairs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fair>> PostFair(Fair fair)
        {
            db.Fairs.Add(fair);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetFair", new { id = fair.FairId }, fair);
        }

        // DELETE: api/Fairs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFair(int id)
        {
            var fair = await db.Fairs.FindAsync(id);
            if (fair == null)
            {
                return NotFound();
            }

            db.Fairs.Remove(fair);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool FairExists(int id)
        {
            return db.Fairs.Any(e => e.FairId == id);
        }
    }
}
