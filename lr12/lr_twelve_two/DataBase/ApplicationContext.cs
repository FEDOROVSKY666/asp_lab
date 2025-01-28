using lr_twelve_two.Models;
using Microsoft.EntityFrameworkCore;

namespace lr_twelve_two.DataBase
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Company> Companies { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { 
            Database.EnsureCreated();
        }
    }
}
