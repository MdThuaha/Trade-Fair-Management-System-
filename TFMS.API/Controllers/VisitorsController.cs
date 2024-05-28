using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFMS.API.ViewModels;
using TFMS.Lib.Models;

namespace TFMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        private readonly FairDbContext db;

        public VisitorsController(FairDbContext context)
        {
            db = context;
        }

        // GET: api/Visitors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visitor>>> GetVisitors()
        {
            return await db.Visitors.ToListAsync();
        }
        [HttpGet("Visitors/Include")]
        public async Task<ActionResult<IEnumerable<Visitor>>> GetVisitorsWithTickets()
        {
            return await db.Visitors
                .Include(x => x.Tickets)
                .ToListAsync();
        }

        // GET: api/Visitors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Visitor>> GetVisitor(int id)
        {
            var visitor = await db.Visitors.FindAsync(id);

            if (visitor == null)
            {
                return NotFound();
            }

            return visitor;
        }

        //**********//
        [HttpGet("{id}/Include")]
        public async Task<ActionResult<Visitor>> GetVisitorWithTickets(int id)
        {
            var visitor = await db.Visitors
                .Include(x => x.Tickets)
                .FirstOrDefaultAsync(x => x.VisitorId == id);

            if (visitor == null)
            {
                return NotFound();
            }

            return visitor;
        }


        //******//
        [HttpGet("Tickets/Of/{id}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetDetailsOfProduct(int id)
        {
            var data = await db.Tickets.Where(x => x.VisitorId == id).ToListAsync();
            return data;
        }


        // PUT: api/Visitors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]

        public async Task<IActionResult> PutVisitor(int id, Visitor visitor)
        {
            if (id != visitor.VisitorId)
            {
                return BadRequest();
            }
            var p = await db.Visitors.FirstOrDefaultAsync(x => x.VisitorId == id);
            if (p == null)
            {
                return NotFound();
            }
            p.FirstName = visitor.FirstName;
            p.LastName = visitor.LastName;
            p.Email = visitor.Email;
            p.Phone = visitor.Phone;
            p.Gender= visitor.Gender;
            p.Nationality= visitor.Nationality;
            
            int n = db.Database.ExecuteSqlInterpolated($"DELETE FROM Tickets WHERE VisitorId={p.VisitorId}");
            //_context.Entry(product).State = EntityState.Modified;
            foreach (var d in visitor.Tickets)
            {
                db.Tickets.Add(new Ticket { VisitorId = p.VisitorId, IssueDate = d.IssueDate, Quantity = d.Quantity, Price=d.Price });
            }
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorExists(id))
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


        // POST: api/Visitors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Visitor>> PostVisitor(Visitor visitor)
        {
            db.Visitors.Add(visitor);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetVisitor", new { id = visitor.VisitorId }, visitor);
        }

        // DELETE: api/Visitors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            var visitor = await db.Visitors.FindAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }

            db.Visitors.Remove(visitor);
            await db.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("Options/Gender")]
        public async Task<IEnumerable<GenderOption>> GetGenderOption()
        {
            string[] names = Enum.GetNames(typeof(Gender));
            List<GenderOption> result = new List<GenderOption>();
            await Task.Run(() =>
            {
                foreach (string name in names)
                {
                    Gender v = (Gender)Enum.Parse(typeof(Gender), name);
                    result.Add(new GenderOption { Text = name, Value = (int)v });
                }

            });
            return result.ToList();
        }


        private bool VisitorExists(int id)
        {
            return db.Visitors.Any(e => e.VisitorId == id);
        }
    }
}
