using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZiniTechERPSystem.Data
{
    public class Invoice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Price { get; set; }

        public string? CompanyId { get; set; }

        public Company? Company { get; set; }

        public string? CreatedById { get; set; } = string.Empty;

        public ApplicationUser? CreatedBy { get; set; }

        public Customer? Customer { get; set; }

        public string? CustomerId { get; set; }

        public List<ProductInvoice>? ProductInvoices { get; set; }
    }
}
