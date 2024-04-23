using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Manager.Pages
{
    public partial class AddProduct
    {
        public Product product = new Product();
        public List<Company> companies = new List<Company>();

        [Inject]
        private ProductService ProductService { get; set; }

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

        public void CreateProduct()
        {
            ProductService.AddProduct(product);
            NavigationManager.NavigateTo("/manager/products");
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
                    product.CreatedById = userId;
                }
            }
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
    }
}
