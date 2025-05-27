
using Microsoft.EntityFrameworkCore;
using APSNET_Core_002.Models;

namespace APSNET_Core_002.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; } = null!;

        public DbSet<Customers> Customers { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "1984", Author = "George Orwell" },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" }
            );

            modelBuilder.Entity<Customers>().HasData(
                new Customers { Id = 1, Name = "John Doe", Email = "sdad@gmail.com" },
                new Customers { Id = 2, Name = "Jane Smith", Email = "rir@gmail.com" }

            );
        }
    }
}
