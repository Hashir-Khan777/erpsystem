using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Manager.Pages
{
    public partial class AddCompany
    {
        public Company company = new Company();
        public List<ApplicationUser> managers = new List<ApplicationUser>();
        public List<ApplicationUser> employees = new List<ApplicationUser>();
        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override void OnInitialized()
        {
            SetUserId();
        }

        public void CreateCompany()
        {
            CompanyService.AddCompany(company);
            NavigationManager.NavigateTo("/manager/companies");
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
                    company.ManagerId = userId;
                }
            }
        }
    }
}
