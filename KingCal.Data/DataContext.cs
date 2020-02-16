using KingCal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KingCal.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<SubItemProperty> SubItemProperty { get; set; }
        public DbSet<SubItem> SubItem { get; set; }
        public DbSet<SubCategoryItem> SubCategoryItem { get; set; }
        public DbSet<ItemSubItem> ItemSubItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubCategoryItem>()
                .HasKey(s => new { s.ItemId, s.SubCategoryId });

            modelBuilder.Entity<ItemSubItem>()
                .HasKey(i => new { i.ItemId, i.SubItemId });
        }
    }
}
