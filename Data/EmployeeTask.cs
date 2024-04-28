using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZiniTechERPSystem.Data
{
    public class EmployeeTask
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Work { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? AsigneeId { get; set; } = string.Empty;

        public ApplicationUser? Asignee { get; set; }
    }
}
