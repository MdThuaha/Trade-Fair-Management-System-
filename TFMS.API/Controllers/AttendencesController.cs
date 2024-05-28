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
    public class AttendencesController : ControllerBase
    {
        private readonly FairDbContext db;

        public AttendencesController(FairDbContext context)
        {
            db = context;
        }

        // GET: api/Attendences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendence>>> GetAttendences()
        {
            return await db.Attendences.ToListAsync();
        }

        // GET: api/Attendences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendence>> GetAttendence(int id)
        {
            var attendence = await db.Attendences.FindAsync(id);

            if (attendence == null)
            {
                return NotFound();
            }

            return attendence;
        }

        // PUT: api/Attendences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendence(int id, Attendence attendence)
        {
            if (id != attendence.AttendenceId)
            {
                return BadRequest();
            }

            db.Entry(attendence).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendenceExists(id))
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

        // POST: api/Attendences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attendence>> PostAttendence(Attendence attendence)
        {
            db.Attendences.Add(attendence);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetAttendence", new { id = attendence.AttendenceId }, attendence);
        }

        // DELETE: api/Attendences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendence(int id)
        {
            var attendence = await db.Attendences.FindAsync(id);
            if (attendence == null)
            {
                return NotFound();
            }

            db.Attendences.Remove(attendence);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendenceExists(int id)
        {
            return db.Attendences.Any(e => e.AttendenceId == id);
        }
    }
}
