using Microsoft.AspNetCore.Components;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Admin.Pages
{
    public partial class LogsPage
    {
        private List<AuditLog> auditLogs = new List<AuditLog>();
        public double Page = 1;
        public double totalLogsCount = 0;
        public List<double> totalPages = new List<double>();

        [Inject]
        private LogService LogService { get; set; }

        protected override void OnInitialized()
        {
            GetLogs();
            GetTotalLogsCount();

            for(int i = 1; i <= Math.Round(totalLogsCount / 5); i++)
            {
                totalPages.Add(i);
            }
        }

        private void GetLogs()
        {
            auditLogs = LogService.GetLogs(Page).ToList();
        }

        public void Paginate(bool? next, double page)
        {
            if (next is not null)
            {
                if (next == true)
                {
                    if (Page >= totalPages.Count())
                    {
                        Page = totalPages.Count();
                    }
                    else
                    {
                        Page++;
                    }
                }
                else
                {
                    if (Page <= 1)
                    {
                        Page = 1;
                    }
                    else
                    {
                        Page--;
                    }
                }
            }
            else
            {
                if (page > 0)
                {
                    Page = page;
                }
            }
            GetLogs();
        }

        private void GetTotalLogsCount()
        {
            totalLogsCount = LogService.GetTotalLogsCount();
        }
    }
}
