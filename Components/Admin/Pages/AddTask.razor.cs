using Microsoft.AspNetCore.Components;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Admin.Pages
{
    public partial class AddTask
    {
        public EmployeeTask task = new EmployeeTask();
        public List <ApplicationUser> employees = new List<ApplicationUser>();
        [Inject]
        private UserService EmployeeService { get; set; }

        [Inject]
        private TaskService TaskService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            GetEmployees();
        }

        public void GetEmployees()
        {
            employees = EmployeeService.GetUsersByClaim("Role", "Employee").ToList();
        }

        public void CreateTask()
        {
            TaskService.AddTask(task);
            NavigationManager.NavigateTo("/admin/tasks");
        }
    }
}
