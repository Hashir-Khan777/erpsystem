using Microsoft.AspNetCore.Components;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Admin.Pages
{
    public partial class TaskPage
    {
        public List<EmployeeTask> tasks = new List<EmployeeTask>();

        [Inject]
        private TaskService TaskService { get; set; }

        protected override void OnInitialized()
        {
            GetTasks();
        }

        public void GetTasks()
        {
            tasks = TaskService.GetTasks().ToList();
        }
    }
}
