using Microsoft.AspNetCore.Components;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Admin.Pages
{
    public partial class EmployeePage
    {
        private List<ApplicationUser> employees = new List<ApplicationUser>();
        [Inject]
        private UserService EmployeeService { get; set; }

        protected override void OnInitialized()
        {
            GetEmployees();
        }

        public void GetEmployees()
        {
            employees = EmployeeService.GetUsersByClaim("Role", "Employee").ToList();
        }
    }
}
