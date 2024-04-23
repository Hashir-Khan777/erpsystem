using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Employee.Pages
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

        [Inject]
        public UserService UserService { get; set; }

        protected override void OnInitialized()
        {
            GetCustomers();
            GetProducts();
            SetUserId();
        }

        public void CreateInvoice()
        {
            InvoiceService.AddInvoice(invoice, selectedProducts);
            NavigationManager.NavigateTo("/employee/invoices");
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
                    int? companyId = UserService.GetUserById(userId)?.CompanyId;
                    customers = CustomerService.GetCustomersByCompanyId(companyId).ToList();
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
                    int? companyId = UserService.GetUserById(userId)?.CompanyId;
                    products = ProductService.GetProductsByCompanyId(companyId).ToList();
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
                    var companyId = UserService.GetUserById(userId).CompanyId;
                    invoice.CreatedById = userId;
                    if (companyId is not null)
                    {
                        invoice.CompanyId = (int)companyId;
                    }
                }
            }
        }
    }
}
