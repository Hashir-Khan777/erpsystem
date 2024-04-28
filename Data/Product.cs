using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZiniTechERPSystem.Data
{
    public enum Status
    {
        pending,
        approve,
        cancel
    }

    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        public Status Status { get; set; } = Status.pending;

        public string? CompanyId { get; set; }

        public Company? Company { get; set; }

        public string? CreatedById { get; set; } = string.Empty;

        public ApplicationUser? CreatedBy { get; set; }

        public List<ProductInvoice>? ProductInvoices { get; set; }
    }
}
