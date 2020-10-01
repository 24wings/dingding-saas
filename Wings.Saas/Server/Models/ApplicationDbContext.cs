using Microsoft.EntityFrameworkCore;
using Wings.Saas.Shared;

namespace Wings.Saas.Server.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<KeySecret> KeySecrets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ScanTask> ScanTasks { get; set; }
        public DbSet<OcrTask> OcrTasks { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

    }

}