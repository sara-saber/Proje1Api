using Microsoft.EntityFrameworkCore;

namespace Proje1Api.Data
{
    public class hospitalDbContext:DbContext
    {
            public hospitalDbContext(DbContextOptions<hospitalDbContext> options)
                : base(options)
            {
            }
            public DbSet<User> Users { get; set; }
            public DbSet<Department> Departments { get; set; }
            public DbSet<Doctor> Doctors { get; set; }
    }
}
