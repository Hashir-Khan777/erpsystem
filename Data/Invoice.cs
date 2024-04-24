using System.ComponentModel.DataAnnotations;

namespace ZiniTechERPSystem.Data
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Price { get; set; }

        public int? CompanyId { get; set; }

        public Company? Company { get; set; }

        public string? CreatedById { get; set; } = string.Empty;

        public ApplicationUser? CreatedBy { get; set; }

        public Customer? Customer { get; set; }

        public int? CustomerId { get; set; }

        public List<ProductInvoice>? ProductInvoices { get; set; }
    }
}
