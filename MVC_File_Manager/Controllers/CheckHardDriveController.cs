using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_File_Manager.Models;

namespace MVC_File_Manager.Controllers
{
    public class CheckHardDriveController : Controller
    {
        private FileManagerDbContext _FileManagerDbContext;
        public CheckHardDriveController(FileManagerDbContext dbContext)
        {
            _FileManagerDbContext = dbContext;
        }
        public IActionResult LocalCatalogs()
        {
            var catalogs = _FileManagerDbContext.LocalCatalogs.ToList();
            return View(catalogs);
        }
        public IActionResult CertainLocalCatalog(LocalCatalog catalog)
        {
            catalog.ChildCatalogs = _FileManagerDbContext.LocalCatalogs.Where(x => x.ParentId == catalog.Id).ToList();
            return View(catalog);
        }
        public IActionResult CheckHDD()
        {
            var catalogs = _FileManagerDbContext.LocalCatalogs.ToList();
            var path = @"D:\1Check\";
            if(catalogs.Count == 0)
            {
                ScanDrive(path, null);
            }
            return RedirectToAction("LocalCatalogs");
        }
        private void ScanDrive(string path, LocalCatalog? parentCatalog)
        {
            LocalCatalog catalog = new LocalCatalog
            {
                Name = Path.GetFileName(path),
                ParentCatalog = parentCatalog
            };
            _FileManagerDbContext.LocalCatalogs.Add(catalog);
            _FileManagerDbContext.SaveChanges();

            foreach (string subDirectory in Directory.GetDirectories(path))
            {
                ScanDrive(subDirectory, catalog);
            }
        }
    }
}
