using System.ComponentModel.DataAnnotations.Schema;

namespace ZiniTechERPSystem.Data
{
    public class ProductInvoice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? ProductId { get; set; }
        public Product? Product { get; set; }
        public string? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
