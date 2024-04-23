using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Manager.Pages
{
    public partial class AddInvoice
    {
        public Invoice invoice = new Invoice();
        public List<Company> companies = new List<Company>();
        public List<Customer> customers = new List<Customer>();
        public List<Product> products = new List<Product>();
        public List<Product> selectedProducts = new List<Product>();
        public List<string> selectedProductNames = new List<string>();

        public bool isDropDownOpen = false;

        [Inject]
        private InvoiceService InvoiceService { get; set; }

        [Inject]
        private CompanyService CompanyService { get; set; }

        [Inject]
        private ProductService ProductService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private CustomerService CustomerService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            GetCompanies();
            GetCustomers();
            GetProducts();
            SetUserId();
        }

        public void CreateInvoice()
        {
            InvoiceService.AddInvoice(invoice, selectedProducts);
            NavigationManager.NavigateTo("/manager/invoices");
        }

        public async void GetCompanies()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    companies = CompanyService.GetCompaniesByManagerId(userId).ToList();
                }
            }
        }

        public async void GetCustomers()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    customers = CustomerService.GetCustomersByManagerId(userId).ToList();
                }
            }
        }

        public async void GetProducts()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    products = ProductService.GetProductsByManagerId(userId).ToList();
                }
            }
        }

        public async void SetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    invoice.CreatedById = userId;
                }
            }
        }
    }
}
