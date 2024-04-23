using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Admin.Pages
{
    public partial class AddCustomer
    {
        public Customer customer = new Customer();
        public List<Company> companies = new List<Company>();

        [Inject]
        private CustomerService CustomerService {  get; set; }

        [Inject]
        private CompanyService CompanyService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            GetCompanies();
            SetUserId();
        }

        public void CreateCustomer()
        {
            CustomerService.AddCustomer(customer);

            NavigationManager.NavigateTo("/admin/customers");
        }

        public void GetCompanies()
        {
            companies = CompanyService.GetCompanies().ToList();
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
                    customer.CreatedById = userId;
                }
            }
        }
    }
}
