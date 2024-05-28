using System;
using System.Collections.Generic;
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
    public class ExhibitorsController : ControllerBase
    {
        private readonly FairDbContext db;
        private readonly IWebHostEnvironment env;
        public ExhibitorsController(FairDbContext context, IWebHostEnvironment env)
        {
            db = context;
            this.env = env;
        }

        // GET: api/Exhibitors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exhibitor>>> GetExhibitors()
        {
            return await db.Exhibitors.ToListAsync();
        }

        // GET: api/Exhibitors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exhibitor>> GetExhibitor(int id)
        {
            var exhibitor = await db.Exhibitors.FindAsync(id);

            if (exhibitor == null)
            {
                return NotFound();
            }

            return exhibitor;
        }

        // PUT: api/Exhibitors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExhibitor(int id, Exhibitor exhibitor)
        {
            if (id != exhibitor.ExhibitorId)
            {
                return BadRequest();
            }

            db.Entry(exhibitor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExhibitorExists(id))
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

        // POST: api/Exhibitors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exhibitor>> PostExhibitor(Exhibitor exhibitor)
        {
            db.Exhibitors.Add(exhibitor);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetExhibitor", new { id = exhibitor.ExhibitorId }, exhibitor);
        }

        // DELETE: api/Exhibitors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExhibitor(int id)
        {
            var exhibitor = await db.Exhibitors.FindAsync(id);
            if (exhibitor == null)
            {
                return NotFound();
            }

            db.Exhibitors.Remove(exhibitor);
            await db.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("Upload/{id}")]
        public async Task<ActionResult<UploadResponse>> Upload(int id, IFormFile file)
        {
            var exhibitor = await db.Exhibitors.FirstOrDefaultAsync(x => x.ExhibitorId == id);
            if (exhibitor == null) return NotFound();
            string ext = Path.GetExtension(file.FileName);
            string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
            string savePath = Path.Combine(this.env.WebRootPath, "Pictures", fileName);
            if (!Directory.Exists(Path.Combine(this.env.WebRootPath, "Pictures")))
            {
                Directory.CreateDirectory(Path.Combine(this.env.WebRootPath, "Pictures"));
            }
            FileStream fs = new FileStream(savePath, FileMode.Create);
            await file.CopyToAsync(fs);
            fs.Close();
            exhibitor.LogoUrl = fileName;
            await db.SaveChangesAsync();
            return new UploadResponse { FileName = fileName };

        }

        private bool ExhibitorExists(int id)
        {
            return db.Exhibitors.Any(e => e.ExhibitorId == id);
        }
    }
}
