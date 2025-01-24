namespace hotelier_core_app.Model.DTOs.Response
{
    public class ModuleGroupDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public List<ModuleDTO> Modules { get; set; }
    }
}
