using System.ComponentModel.DataAnnotations;

namespace hotelier_core_app.Model.DTOs.Request
{
    public class CreateModuleDTO
    {
        [Range(1, Int32.MaxValue)]
        public int ModuleGroupId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
