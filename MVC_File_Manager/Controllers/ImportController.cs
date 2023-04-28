using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> ImportCatalogsAsync()
        {
            var catalogs = await _FileManagerDbContext.ImportCatalogs.ToListAsync();
            return View(catalogs);
        }
        public async Task<IActionResult> CertainImportCatalogAsync(ImportCatalog catalog)
        {
            catalog.ChildCatalogs = await _FileManagerDbContext.ImportCatalogs.Where(x => x.ParentId == catalog.Id).ToListAsync();
            return View(catalog);
        }
        public async Task<IActionResult> ImportAsync()
        {
            var path = @"D:\2Check\Import.txt";
            var rows = await System.IO.File.ReadAllLinesAsync(path);
            var importCatalogs = new List<ImportCatalog>();
            var catalogs = await _FileManagerDbContext.ImportCatalogs.ToListAsync();
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
                    await _FileManagerDbContext.ImportCatalogs.AddAsync(catalog);
                }
                await _FileManagerDbContext.SaveChangesAsync();
            }
            return RedirectToAction("ImportCatalogs");
        }
        public async Task<IActionResult> ExportAsync()
        {
            var catalogs = await _FileManagerDbContext.ImportCatalogs.ToListAsync();
            StringBuilder sb = new StringBuilder();
            foreach (var catalog in catalogs)
            {
                sb.AppendLine($"{catalog.Id},{catalog.Name},{catalog.ParentId}");
            }
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            var stream = new MemoryStream(bytes);
            return File(stream, "text/plain", "Export.txt");
        }
    }
}
