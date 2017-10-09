using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDevProject.Models
{
    public class ModelContext :DbContext
 
    {
        private IConfigurationRoot _config;

        public ModelContext(IConfigurationRoot config, DbContextOptions options) :base(options)
        {
            _config = config;
        }

        public DbSet<TestModel> testModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString: _config["ConnectionStrings:ModelContextConnection"]);
        }
    }
}
