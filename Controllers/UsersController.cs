
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje1Api.Data;
using System.Numerics;

namespace Proje1Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly hospitalDbContext db;
        public UsersController(hospitalDbContext _db)
        {
            db = _db;
        }
        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            if (db.Users == null)
            {
                return NotFound();
            }
            return await db.Users.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Getuser(Guid id)
        {

            var doctor = await db.Users.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // PUT: api/users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            user.Id = new Guid();
            var d = await db.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (d == null)
            {
                user.IsActive = true;
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return Ok("add Successfully");
            }
            else
            {
                return BadRequest();

            }
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return (db.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // POST: api/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> UserLogin(User user)
        {
            var d = await db.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
            if (d == null)
            {
                return StatusCode(500);
            }
            else
            {
                user.Id = d.Id;
                user.Email = d.Email;
                user.Phone = d.Phone;
                user.Password = d.Password;
                user.IsActive = d.IsActive;
                user.Name = d.Name;

                if (user.IsActive==true)
                {
                    return user;
                }
                else
                {
                    return StatusCode(500);
                }

            }
        }
    }
}
