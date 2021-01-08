using Arhive_MDM.Models;
using Microsoft.EntityFrameworkCore;

namespace Arhive_MDM.Data
{
    class DataContext: DbContext
    {

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<OrderContent> OrderContents { get; set; }

        public DbSet<Documents> Documents { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.sqlite3");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Client>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Client);

            modelBuilder.Entity<Client>()
                .HasIndex(x => x.Id)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .HasIndex(x => x.ContactNumber)
                .IsUnique();

            modelBuilder.Entity<Worker>()
                .HasIndex(x => x.Id)
                .IsUnique();

            modelBuilder.Entity<Worker>()
                .HasIndex(x => x.Login)
                .IsUnique();

            modelBuilder.Entity<Orders>()
                .HasIndex(x => x.Id)
                .IsUnique();

            modelBuilder.Entity<OrderContent>()
                .HasIndex(x => x.Id)
                .IsUnique();

            modelBuilder.Entity<Orders>()
                .HasMany(x => x.OrderContents)
                .WithOne(x => x.Orders);

            modelBuilder.Entity<Orders>()
                .HasOne(x => x.Worker)
                .WithMany(x => x.Orders);

            modelBuilder.Entity<Orders>()
                .HasMany(x => x.Documents)
                .WithOne(x => x.Orders);

            modelBuilder.Entity<Documents>()
               .HasIndex(x => x.Id)
               .IsUnique();

        }
    }
}
