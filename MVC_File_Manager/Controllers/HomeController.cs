using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_File_Manager.Models;

namespace MVC_File_Manager.Controllers
{
    public class HomeController : Controller
    {
        private FileManagerDbContext _FileManagerDbContext;
        public HomeController(FileManagerDbContext dbContext)
        {
            _FileManagerDbContext = dbContext;
        }
        public IActionResult Home()
        {
            return View();
        }
        public async Task<IActionResult> TaskCatalogsAsync()
        {
            var catalogs = await _FileManagerDbContext.Catalogs.ToListAsync();
            return View(catalogs);
        }
        public async Task<IActionResult> CertainCatalogAsync(Catalog catalog)
        {
            catalog.ChildCatalogs = await _FileManagerDbContext.Catalogs.Where(x => x.ParentId == catalog.Id).ToListAsync();
            return View(catalog);
        }
        public async Task<IActionResult> AddCatalogAsync(Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                await _FileManagerDbContext.Catalogs.AddAsync(catalog);
                await _FileManagerDbContext.SaveChangesAsync();
                return RedirectToAction("Home");
            }
            return View(catalog);
        }
    }
}
