namespace MVC_File_Manager.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public Catalog? ParentCatalog { get; set; }
        public List<Catalog>? ChildCatalogs { get; set; }
    }
}
