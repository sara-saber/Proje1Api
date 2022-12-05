
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje1Api.Data;
using System.Runtime.Intrinsics.Arm;

namespace Proje1Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        public readonly hospitalDbContext db;
        public DoctorsController(hospitalDbContext _db)
        {
            db = _db;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            if (db.Doctors == null)
            {
                return NotFound();
            }
            return await db.Doctors.ToListAsync();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(Guid id)
        {
            var doctor = await db.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }
            else
            {
                return doctor;
            }
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(Doctor doctor)
        {
            var d = await db.Doctors.FindAsync(doctor.Id);
            if (d==null)
            {
                return BadRequest();
            }
            else
            {
                d.Id = doctor.Id;
                d.Name = doctor.Name;
                d.Email = doctor.Email;
                d.Gender = doctor.Gender;
                d.Address = doctor.Address;
                d.Experinece = doctor.Experinece;
                d.Tel = doctor.Tel;

                db.Doctors.Update(d);
                await db.SaveChangesAsync();
                return Ok(200);
            }
        }

        // POST: api/Doctors
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            var d = await db.Doctors.FirstOrDefaultAsync(x => x.Email == doctor.Email);
            if (d == null)
            {
                doctor.Id = new Guid();
                db.Doctors.Add(doctor);
                await db.SaveChangesAsync();
                return Ok(200);
            }
            else
            {
                return BadRequest();

            }
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var d = await db.Doctors.FindAsync(id);
            if (d == null)
            {
                return NotFound();
            }
            else
            {
                db.Doctors.Remove(d);
                await db.SaveChangesAsync();
            }
            return NoContent();
        }


    }
}
