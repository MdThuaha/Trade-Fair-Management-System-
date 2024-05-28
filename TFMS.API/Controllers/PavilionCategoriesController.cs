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
    public class PavilionCategoriesController : ControllerBase
    {
        private readonly FairDbContext db;
        public PavilionCategoriesController(FairDbContext context)
        {
            db = context;
        }

        // GET: api/PavilionCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PavilionCategory>>> GetPavilionCategorys()
        {
            return await db.PavilionCategorys.ToListAsync();
        }

        [HttpGet("Pavilions/Include")]
        public async Task<ActionResult<IEnumerable<PavilionCategory>>> GetPavilionCategorysWithPavilion()
        {
            return await db.PavilionCategorys
                .Include(x=>x.Pavilions)
                .ToListAsync();
        }
        // GET: api/PavilionCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PavilionCategory>> GetPavilionCategory(int id)
        {
            var pavilionCategory = await db.PavilionCategorys.FindAsync(id);

            if (pavilionCategory == null)
            {
                return NotFound();
            }

            return pavilionCategory;
        }


        [HttpGet("{id}/Include")]
        public async Task<ActionResult<PavilionCategory>> GetPavilionCategoryWithPavilions(int id)
        {
            var pavilionCategory = await db.PavilionCategorys
                 .Include(x => x.Pavilions)
                .FirstOrDefaultAsync(x=>x.PavilionCategoryId==id);

            if (pavilionCategory == null)
            {
                return NotFound();
            }
            return pavilionCategory;
        }


        // Pavilions data of
        //===========================================
        [HttpGet("Pavilions/Of/{id}")]
        public async Task<ActionResult<IEnumerable<Pavilion>>> GetPavilionOfPavilionCategiry(int id)
        {
            var data = await db.Pavilions.Where(x => x.PavilionCategoryId == id).ToListAsync();
            return data;
        }

//=====================================


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPavilionCategory(int id, PavilionCategory pavilionCategory)
        {
            if (id != pavilionCategory.PavilionCategoryId)
            {
                return BadRequest("ID in the request body does not match the URL.");
            }

            var c = db.PavilionCategorys.Include(x => x.Pavilions).FirstOrDefault(x => x.PavilionCategoryId == id);
            // Update only the properties you want to allow updating
            c.CategoryName = pavilionCategory.CategoryName;
            c.PavilionType = pavilionCategory.PavilionType;
            c.Description = pavilionCategory.Description;
            c.FarePerSquareFeet=pavilionCategory.FarePerSquareFeet;
            c.Pavilions.Clear();
            foreach (var p in pavilionCategory.Pavilions)
            { 
            c.Pavilions.Add(p);
            }

            // Add more properties to update as needed

            try
            {
                db.Entry(c).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PavilionCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw; // Rethrow the exception, indicating a server error
                }
            }

            return NoContent();
        }


        // POST: api/PavilionCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PavilionCategory>> PostPavilionCategory(PavilionCategory pavilionCategory)
        {
            db.PavilionCategorys.Add(pavilionCategory);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetPavilionCategory", new { id = pavilionCategory.PavilionCategoryId }, pavilionCategory);
        }

        // DELETE: api/PavilionCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePavilionCategory(int id)
        {
            var pavilionCategory = await db.PavilionCategorys.FindAsync(id);
            if (pavilionCategory == null)
            {
                return NotFound();
            }

            db.PavilionCategorys.Remove(pavilionCategory);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool PavilionCategoryExists(int id)
        {
            return db.PavilionCategorys.Any(e => e.PavilionCategoryId == id);
        }
    }
}
