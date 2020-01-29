using KingCal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KingCal.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    }
}
