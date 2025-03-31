using CrudOpration.Entity;
using Microsoft.EntityFrameworkCore;

namespace CrudOpration.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<EmployeeEntity> Employees { get; set; } // Ensure this line exists
    }
}
