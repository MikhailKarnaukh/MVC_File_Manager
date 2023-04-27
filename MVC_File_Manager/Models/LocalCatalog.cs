namespace MVC_File_Manager.Models
{
    public class LocalCatalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public LocalCatalog? ParentCatalog { get; set; }
        public List<LocalCatalog>? ChildCatalogs { get; set; }
    }
}
