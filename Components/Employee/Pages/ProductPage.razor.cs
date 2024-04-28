using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Employee.Pages
{
    public partial class ProductPage
    {
        public List<Product> products = new List<Product>();

        [Inject]
        public ProductService ProductService {  get; set; }

        [Inject]
        public UserService UserService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override void OnInitialized()
        {
            GetProducts();
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
                    string? companyId = UserService.GetUserById(userId)?.CompanyId;
                    products = ProductService.GetProductsByCompanyId(companyId).ToList();
                }
            }
        }
    }
}
