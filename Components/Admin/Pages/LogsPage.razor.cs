using Microsoft.AspNetCore.Components;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Admin.Pages
{
    public partial class LogsPage
    {
        private List<AuditLog> auditLogs = new List<AuditLog>();

        [Inject]
        private LogService LogService { get; set; }

        protected override void OnInitialized()
        {
            GetLogs();
        }

        private void GetLogs()
        {
            auditLogs = LogService.GetLogs().ToList();
        }
    }
}
