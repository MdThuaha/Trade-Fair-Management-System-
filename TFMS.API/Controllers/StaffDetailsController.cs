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
    public class StaffDetailsController : ControllerBase
    {
        private readonly FairDbContext db;
        private readonly IWebHostEnvironment env;


        public StaffDetailsController(FairDbContext context, IWebHostEnvironment env)
        {
            db = context;
            this.env = env;
        }

        // GET: api/StaffDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDetail>>> GetStaffDetails()
        {
            return await db.StaffDetails.ToListAsync();
        }

        [HttpGet("Attendences/Include")]
        public async Task<ActionResult<IEnumerable<StaffDetail>>> GetStaffDetailsWithAttendence()
        {
            return await db.StaffDetails
                .Include(x=>x.Attendences)
                .ToListAsync();
        }

        // GET: api/StaffDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDetail>> GetStaffDetail(int id)
        {
            var staffDetail = await db.StaffDetails.FindAsync(id);

            if (staffDetail == null)
            {
                return NotFound();
            }

            return staffDetail;
        }

        [HttpGet("{id}/Include")]
        public async Task<ActionResult<StaffDetail>> GetStaffDetailWithAttendences(int id)
        {
            var staffDetail = await db.StaffDetails.Include(x => x.Attendences)
                .FirstOrDefaultAsync(x => x.StaffDetailId == id);

            if (staffDetail == null)
            {
                return NotFound();
            }

            return staffDetail;
        }

        // Attendences data of
        //===========================================
        [HttpGet("Attendences/Of/{id}")]
        public async Task<ActionResult<IEnumerable<Attendence>>> GetAttendenceOfStaffDetail(int id)
        {
            var data = await db.Attendences.Where(x => x.StaffDetailId == id).ToListAsync();
            return data;
        }

        // PUT: api/StaffDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffDetail(int id, StaffDetail staffDetail)
        {
            if (id != staffDetail.StaffDetailId)
            {
                return BadRequest("ID in the request body does not match the URL.");
            }

            var c = db.StaffDetails.Include(x => x.Attendences).FirstOrDefault(x => x.StaffDetailId == id);
            // Update only the properties you want to allow updating
            c.StaffName = staffDetail.StaffName;
            c.Email = staffDetail.Email;
            c.Phone = staffDetail.Phone;
            c.Gender = staffDetail.Gender;
            c.Picture = staffDetail.Picture;
            c.Attendences.Clear();
            foreach (var p in staffDetail.Attendences)
            {
                c.Attendences.Add(p);
            }


            try
            {
                db.Entry(c).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffDetailExists(id))
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

        // POST: api/StaffDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StaffDetail>> PostStaffDetail(StaffDetail staffDetail)
        {
            db.StaffDetails.Add(staffDetail);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetStaffDetail", new { id = staffDetail.StaffDetailId }, staffDetail);
        }

        // DELETE: api/StaffDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffDetail(int id)
        {
            var staffDetail = await db.StaffDetails.FindAsync(id);
            if (staffDetail == null)
            {
                return NotFound();
            }

            db.StaffDetails.Remove(staffDetail);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("Upload/{id}")]
        public async Task<ActionResult<UploadResponse>> Upload(int id, IFormFile file)
        {
            var staff = await db.StaffDetails.FirstOrDefaultAsync(x => x.StaffDetailId == id);
            if (staff == null) return NotFound();
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
            staff.Picture = fileName;
            await db.SaveChangesAsync();
            return new UploadResponse { FileName = fileName };

        }

        private bool StaffDetailExists(int id)
        {
            return db.StaffDetails.Any(e => e.StaffDetailId == id);
        }
    }
}
