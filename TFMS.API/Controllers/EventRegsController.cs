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
    public class EventRegsController : ControllerBase
    {
        private readonly FairDbContext db;

        public EventRegsController(FairDbContext context)
        {
            db = context;
        }

        // GET: api/EventRegs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventReg>>> GetEventsReg()
        {
            return await db.EventsReg.ToListAsync();
        }

        // GET: api/EventRegs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventReg>> GetEventReg(int id)
        {
            var eventReg = await db.EventsReg.FindAsync(id);

            if (eventReg == null)
            {
                return NotFound();
            }

            return eventReg;
        }

        // PUT: api/EventRegs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventReg(int id, EventReg eventReg)
        {
            if (id != eventReg.EventRegId)
            {
                return BadRequest();
            }

            db.Entry(eventReg).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventRegExists(id))
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

        // POST: api/EventRegs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventReg>> PostEventReg(EventReg eventReg)
        {
            db.EventsReg.Add(eventReg);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetEventReg", new { id = eventReg.EventRegId }, eventReg);
        }

        // DELETE: api/EventRegs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventReg(int id)
        {
            var eventReg = await db.EventsReg.FindAsync(id);
            if (eventReg == null)
            {
                return NotFound();
            }

            db.EventsReg.Remove(eventReg);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool EventRegExists(int id)
        {
            return db.EventsReg.Any(e => e.EventRegId == id);
        }
    }
}
