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
        public IActionResult TaskCatalogs()
        {
            var catalogs = _FileManagerDbContext.Catalogs.ToList();
            return View(catalogs);
        }
        public IActionResult CertainCatalog(Catalog catalog)
        {
            catalog.ChildCatalogs = _FileManagerDbContext.Catalogs.Where(x => x.ParentId == catalog.Id).ToList();
            return View(catalog);
        }
        public IActionResult AddCatalog(Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                _FileManagerDbContext.Catalogs.Add(catalog);
                _FileManagerDbContext.SaveChanges();
                return RedirectToAction("Home");
            }
            return View(catalog);
        }
    }
}
