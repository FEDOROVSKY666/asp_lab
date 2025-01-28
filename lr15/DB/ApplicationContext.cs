using lr_fifteen.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace lr_fifteen.DB
{
    public class ApplicationContext: DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
            Database.EnsureCreated();
        }
    }
}
