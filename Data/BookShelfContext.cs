using BookShelfAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShelfAPI.Data
{
    public class BookShelfContext:DbContext
    {
        public BookShelfContext(DbContextOptions opt) : base(opt)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
            .HasIndex(b => b.Title)
            .IsUnique();
        }
        public DbSet<Book> Books{get; set;}
    }
}