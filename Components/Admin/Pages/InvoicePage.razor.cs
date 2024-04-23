using Microsoft.AspNetCore.Components;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Admin.Pages
{
    public partial class InvoicePage
    {
        public List<Invoice> invoices = new List<Invoice>();

        [Inject]
        private InvoiceService InvoiceService { get; set; }

        protected override void OnInitialized()
        {
            GetInvoices();
        }

        public void GetInvoices()
        {
            invoices = InvoiceService.GetInvoices().ToList();
        }
    }
}
