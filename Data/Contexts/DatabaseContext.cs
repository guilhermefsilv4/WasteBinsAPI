using Microsoft.EntityFrameworkCore;
using WasteBinsAPI.Models;

namespace WasteBinsAPI.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<WasteBinModel> WasteBins { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }
    }
}