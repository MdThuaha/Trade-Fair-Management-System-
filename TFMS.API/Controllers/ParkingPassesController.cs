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
    public class ParkingPassesController : ControllerBase
    {
        private readonly FairDbContext db;

        public ParkingPassesController(FairDbContext context)
        {
            db = context;
        }

        // GET: api/ParkingPasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingPass>>> GetParkingPasses()
        {
            return await db.ParkingPasses.ToListAsync();
        }

        // GET: api/ParkingPasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingPass>> GetParkingPass(int id)
        {
            var parkingPass = await db.ParkingPasses.FindAsync(id);

            if (parkingPass == null)
            {
                return NotFound();
            }

            return parkingPass;
        }

        // PUT: api/ParkingPasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingPass(int id, ParkingPass parkingPass)
        {
            if (id != parkingPass.ParkingPassId)
            {
                return BadRequest();
            }

            db.Entry(parkingPass).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingPassExists(id))
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

        // POST: api/ParkingPasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParkingPass>> PostParkingPass(ParkingPass parkingPass)
        {
            db.ParkingPasses.Add(parkingPass);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetParkingPass", new { id = parkingPass.ParkingPassId }, parkingPass);
        }

        // DELETE: api/ParkingPasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingPass(int id)
        {
            var parkingPass = await db.ParkingPasses.FindAsync(id);
            if (parkingPass == null)
            {
                return NotFound();
            }

            db.ParkingPasses.Remove(parkingPass);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ParkingPassExists(int id)
        {
            return db.ParkingPasses.Any(e => e.ParkingPassId == id);
        }
    }
}
