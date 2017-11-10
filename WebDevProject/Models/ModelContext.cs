using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebDevProject.Models
{
    public class ModelContext : IdentityDbContext<ApplicationUser>
 
    {
        private IConfigurationRoot _config;

        public ModelContext(IConfigurationRoot config, DbContextOptions options) :base(options)
        {
            _config = config;
        }

        public DbSet<Index> Index { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString: _config["ConnectionStrings:ModelContextConnection"]);
        }
    }
}
