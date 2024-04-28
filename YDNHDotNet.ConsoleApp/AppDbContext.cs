using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDNHDotNet.ConsoleApp
{
    internal class AppDbContext : DbContext
    {
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }
        public DbSet<BlogDto> Blogs { get; set; }
    }
}
