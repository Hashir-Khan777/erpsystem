using System.ComponentModel.DataAnnotations;

namespace ZiniTechERPSystem.Data
{
    public class Company
    {
        public int Id { get; set; }

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
