using System.ComponentModel.DataAnnotations;

namespace ZiniTechERPSystem.Data
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public Company? Company { get; set; }

        [Required]
        public string CreatedById { get; set; } = string.Empty;

        public ApplicationUser? CreatedBy { get; set; }

        public List<Invoice>? Invoices { get; set; }
    }
}
