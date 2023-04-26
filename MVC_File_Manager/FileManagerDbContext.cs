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

            Catalog creatingDI = new Catalog { Id = 1, Name = "Creating Digital Images" };
            Catalog resources = new Catalog { Id = 2, Name = "Resources", ParentId = 1 };
            Catalog evidence = new Catalog { Id = 3, Name = "Evidence", ParentId = 1 };
            Catalog graphicProducts = new Catalog { Id = 4, Name = "Graphic Products", ParentId = 1 };
            Catalog primarySources = new Catalog { Id = 5, Name = "Primary Sources", ParentId = 2 };
            Catalog secondarySources = new Catalog { Id = 6, Name = "Secondary Sources", ParentId = 2 };
            Catalog process = new Catalog { Id = 7, Name = "Process", ParentId = 4 };
            Catalog finalProduct = new Catalog { Id = 8, Name = "Final Product", ParentId = 4 };
            modelBuilder.Entity<Catalog>().HasData(creatingDI);
            modelBuilder.Entity<Catalog>().HasData(resources);
            modelBuilder.Entity<Catalog>().HasData(evidence);
            modelBuilder.Entity<Catalog>().HasData(graphicProducts);
            modelBuilder.Entity<Catalog>().HasData(primarySources);
            modelBuilder.Entity<Catalog>().HasData(secondarySources);
            modelBuilder.Entity<Catalog>().HasData(process);
            modelBuilder.Entity<Catalog>().HasData(finalProduct);
        }
    }
}
