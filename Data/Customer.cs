using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZiniTechERPSystem.Data
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int PhoneNumber { get; set; }

        public string? CompanyId { get; set; }

        public Company? Company { get; set; }

        public string? CreatedById { get; set; } = string.Empty;

        public ApplicationUser? CreatedBy { get; set; }

        public List<Invoice>? Invoices { get; set; }
    }
}
