using Microsoft.EntityFrameworkCore;

namespace restapi.Contexts 
{
    public class RestApiContext : DbContext
    {
        public RestApiContext(DbContextOptions<RestApiContext> options) : base(options)
        {
        }

        // Tables
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Elevator> Elevators { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>()
                .ToTable("buildings")
                .HasKey(c => c.id);
            modelBuilder.Entity<Building>().HasMany(p => p.Batteries).WithOne(c => c.Building);

            modelBuilder.Entity<Battery>()
                .ToTable("batteries")
                .HasKey(b => b.id);
            modelBuilder.Entity<Battery>()
                .Property(x => x.certificate_operations)
                .HasColumnType("blob");
            modelBuilder.Entity<Battery>().Property<long?>("building_id");
            modelBuilder.Entity<Battery>().HasOne(c => c.Building).WithMany(p => p.Batteries).HasForeignKey(d => d.building_id);
            modelBuilder.Entity<Battery>().HasMany(b => b.Columns).WithOne(c => c.Battery);
            
            modelBuilder.Entity<Column>()
                .ToTable("columns")
                .HasKey(c => c.id);
            modelBuilder.Entity<Column>().Property<long?>("battery_id");
            modelBuilder.Entity<Column>().HasOne(c => c.Battery).WithMany(p => p.Columns).HasForeignKey(d => d.battery_id);
            modelBuilder.Entity<Column>().HasMany(p => p.Elevators).WithOne(c => c.Column);
            
            modelBuilder.Entity<Elevator>()
                .ToTable("elevators")
                .HasKey(x => x.id);
            modelBuilder.Entity<Elevator>().Property<long?>("column_id");
            modelBuilder.Entity<Elevator>().HasOne(c => c.Column).WithMany(p => p.Elevators).HasForeignKey(d => d.column_id);

            modelBuilder.Entity<Intervention>()
                .ToTable("interventions")
                .HasKey(x => x.id);

            modelBuilder.Entity<Lead>()
                .ToTable("leads")
                .HasKey(l => l.id);
            modelBuilder.Entity<Employee>()
                .ToTable("employees")
                .HasKey(x => x.id);
        }
    }
}