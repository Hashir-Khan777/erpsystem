using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Employee.Pages
{
    public partial class InvoicePage
    {
        public List<Invoice> invoices = new List<Invoice>();

        [Inject]
        private InvoiceService InvoiceService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public UserService UserService { get; set; }

        protected override void OnInitialized()
        {
            GetInvoices();
        }

        public async void GetInvoices()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    int? companyId = UserService.GetUserById(userId)?.CompanyId;
                    invoices = InvoiceService.GetInvoicesByCompanyId(companyId).ToList();
                }
            }
        }
    }
}
