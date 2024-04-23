using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Manager.Pages
{
    public partial class EmployeePage
    {
        private List<ApplicationUser> employees = new List<ApplicationUser>();

        [Inject]
        private UserService EmployeeService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override void OnInitialized()
        {
            GetEmployees();
        }

        public async void GetEmployees()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    employees = EmployeeService.GetEmployeeesByManagerId(userId).ToList();
                }
            }
        }
    }
}
