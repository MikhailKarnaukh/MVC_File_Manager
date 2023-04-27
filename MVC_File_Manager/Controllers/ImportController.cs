using Microsoft.AspNetCore.Mvc;
using MVC_File_Manager.Models;
using System.Text;

namespace MVC_File_Manager.Controllers
{
    public class ImportController : Controller
    {
        private FileManagerDbContext _FileManagerDbContext;
        public ImportController(FileManagerDbContext dbContext)
        {
            _FileManagerDbContext = dbContext;
        }
        public IActionResult ImportCatalogs()
        {
            var catalogs = _FileManagerDbContext.ImportCatalogs.ToList();
            return View(catalogs);
        }
        public IActionResult CertainImportCatalog(ImportCatalog catalog)
        {
            catalog.ChildCatalogs = _FileManagerDbContext.ImportCatalogs.Where(x => x.ParentId == catalog.Id).ToList();
            return View(catalog);
        }
        public IActionResult Import()
        {
            var path = @"D:\2Check\Import.txt";
            var rows = System.IO.File.ReadAllLines(path);
            var importCatalogs = new List<ImportCatalog>();
            var catalogs = _FileManagerDbContext.ImportCatalogs.ToList();
            if (catalogs.Count == 0)
            {
                foreach (var row in rows)
                {
                    var properties = row.Split(',');
                    if (properties.Length == 3)
                    {
                        string name = properties[1];
                        int parentId = int.Parse(properties[2]);
                        var catalog = new ImportCatalog { Name = name, ParentId = parentId };
                        importCatalogs.Add(catalog);
                    }
                    else if (properties.Length == 2)
                    {
                        string name = properties[1];
                        var catalog = new ImportCatalog { Name = name };
                        importCatalogs.Add(catalog);
                    }
                }
                foreach (var catalog in importCatalogs)
                {
                    _FileManagerDbContext.ImportCatalogs.Add(catalog);
                }
                _FileManagerDbContext.SaveChanges();
            }
            return RedirectToAction("ImportCatalogs");
        }
        public IActionResult Export()
        {
            var path = @"D:\2Check\Export.txt";
            var catalogs = _FileManagerDbContext.ImportCatalogs.ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var catalog in catalogs)
            {
                sb.AppendLine($"{catalog.Id},{catalog.Name},{catalog.ParentId}");
            }
            System.IO.File.AppendAllText(path, sb.ToString());
            return View();
        }
    }
}
