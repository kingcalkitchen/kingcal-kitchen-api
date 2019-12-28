using KingCal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KingCal.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Food> Foods { get; set; }
    }
}
