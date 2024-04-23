using Microsoft.AspNetCore.Components;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Admin.Pages
{
    public partial class AddCompany
    {
        public Company company = new Company();
        public List<ApplicationUser> managers = new List<ApplicationUser>();
        public List<ApplicationUser> employees = new List<ApplicationUser>();
        [Inject]
        private UserService ManagerService { get; set; }
        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            GetManagers();
        }

        public void CreateCompany()
        {
            CompanyService.AddCompany(company);
            NavigationManager.NavigateTo("/admin/companies");
        }

        public void GetManagers()
        {
            managers = ManagerService.GetUsersByClaim("Role", "Manager").ToList();
        }
    }
}
