using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje1Api.Data;
using System.Numerics;

namespace Proje1Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public readonly hospitalDbContext db;
        public DepartmentController(hospitalDbContext _db)
        {
            db = _db;
        }
        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Get()
        {
            if (db.Departments == null)
            {
                return NotFound();
            }
            return await db.Departments.ToListAsync();
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> Get(Guid id)
        {
            var department = await db.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            else
            {
                return department;
            }
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<IActionResult> Post(Guid id, Department department)
        {

            var d = db.Departments.FirstOrDefault(x => x.Name == department.Name);

            if (d == null)
            {
                return BadRequest();
            }
            else
            {
                db.Departments.Update(department);
                await db.SaveChangesAsync();
                return NoContent();
            }
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            else
            {
                db.Departments.Update(department);
                await db.SaveChangesAsync();
                return Ok("Updated Successfully");
            }
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dep = await db.Departments.FindAsync(id);
            if (dep == null)
            {
                return NotFound();
            }
            else
            {
                db.Departments.Remove(dep);
                await db.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
