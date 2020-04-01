using Microsoft.EntityFrameworkCore;

namespace restapi.Contexts 
{
    public class RestApiContext : DbContext
    {
        public RestApiContext(DbContextOptions<RestApiContext> options) : base(options)
        {
        }

        // Tables
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Column> Columns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Battery>()
                .ToTable("batteries")
                .HasKey(b => b.id);
            modelBuilder.Entity<Battery>().Property(x => x.certificate_operations).HasColumnType("blob");
            
            modelBuilder.Entity<Column>()
                .ToTable("columns")
                .HasKey(c => c.id);
            modelBuilder.Entity<Column>().Property<long>("battery_id");
            modelBuilder.Entity<Column>().HasOne(c => c.battery).WithMany().HasForeignKey("battery_id");
        }
    }
}