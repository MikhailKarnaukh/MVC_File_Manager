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
        public async Task<IActionResult> LocalCatalogsAsync()
        {
            var catalogs = await _FileManagerDbContext.LocalCatalogs.ToListAsync();
            return View(catalogs);
        }
        public async Task<IActionResult> CertainLocalCatalogAsync(LocalCatalog catalog)
        {
            catalog.ChildCatalogs = await _FileManagerDbContext.LocalCatalogs.Where(x => x.ParentId == catalog.Id).ToListAsync();
            return View(catalog);
        }
        public IActionResult SelectingRoot()
        {
            return View();
        }
        public async Task<IActionResult> CheckHDDAsync(string inputPath)
        {
            var catalogs = await _FileManagerDbContext.LocalCatalogs.ToListAsync();
            var path = inputPath.Replace(@"\", @"\"); ;
            if (catalogs.Count == 0)
            {
                await ScanDriveAsync(path, null);
            }
            return RedirectToAction("LocalCatalogs");
        }
        private async Task ScanDriveAsync(string path, LocalCatalog? parentCatalog)
        {
            LocalCatalog catalog = new LocalCatalog
            {
                Name = Path.GetFileName(path),
                ParentCatalog = parentCatalog
            };
            await _FileManagerDbContext.LocalCatalogs.AddAsync(catalog);
            await _FileManagerDbContext.SaveChangesAsync();
            foreach (string subDirectory in Directory.GetDirectories(path))
            {
                await ScanDriveAsync(subDirectory, catalog);
            }
        }
    }
}
