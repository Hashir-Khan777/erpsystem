using System.ComponentModel.DataAnnotations;

namespace ZiniTechERPSystem.Data
{
    public class EmployeeTask
    {
        public int Id { get; set; }

        [Required]
        public string Work { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? AsigneeId { get; set; } = string.Empty;

        public ApplicationUser? Asignee { get; set; }
    }
}
