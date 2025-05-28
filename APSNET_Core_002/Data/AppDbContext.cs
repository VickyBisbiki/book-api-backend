
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

        public DbSet<Customer> Customers { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "1984", Author = "George Orwell" },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "John Doe", Email = "sdad@gmail.com" },
                new Customer { Id = 2, Name = "Jane Smith", Email = "rir@gmail.com" }

            );
        }
    }
}
