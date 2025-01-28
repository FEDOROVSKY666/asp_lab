using lr_twelve_one.Models;
using Microsoft.EntityFrameworkCore;

namespace lr_twelve_one.DataBase
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) {
            Database.EnsureCreated();
        }
    }
}
