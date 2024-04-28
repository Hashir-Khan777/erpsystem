using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZiniTechERPSystem.Data
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? ManagerId { get; set; } = string.Empty;

        public ApplicationUser? Manager { get; set; }

        public List<ApplicationUser>? Employees { get; set; }

        public List<Product>? Products { get; set; }

        public List<Customer>? Customers { get; set; }

        public List<Invoice>? Invoices { get; set; }
    }
}
