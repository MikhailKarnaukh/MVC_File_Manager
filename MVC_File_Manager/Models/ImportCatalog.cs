namespace MVC_File_Manager.Models
{
    public class ImportCatalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public ImportCatalog? ParentCatalog { get; set; }
        public List<ImportCatalog>? ChildCatalogs { get; set; }
    }
}
