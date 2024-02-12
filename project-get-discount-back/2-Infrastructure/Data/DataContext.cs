using Microsoft.EntityFrameworkCore;
using project_get_discount_back.Entities;

namespace project_get_discount_back.Context
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("WebApiDatabase");
            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<User> Users { get; set; }
    }
}
