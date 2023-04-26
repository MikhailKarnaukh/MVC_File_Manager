using Microsoft.EntityFrameworkCore;
using MVC_File_Manager.Models;

namespace MVC_File_Manager
{
    public class FileManagerDbContext : DbContext
    {
        public FileManagerDbContext(DbContextOptions<FileManagerDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<Catalog> Catalogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>()
                .HasOne(c => c.ParentCatalog)
                .WithMany(c => c.ChildCatalogs)
                .HasForeignKey(c => c.ParentId);
        }
    }
}
