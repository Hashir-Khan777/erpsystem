using Microsoft.AspNetCore.Components;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Admin.Pages
{
    public partial class CompanyPage
    {
        private List<Company> companies = new List<Company>();
        [Inject]
        private CompanyService CompanyService { get; set; }

        protected override void OnInitialized()
        {
            GetCompanies();
        }

        public void GetCompanies()
        {
            companies = CompanyService.GetCompanies().ToList();
        }

        public void DeleteCompany(int Id)
        {
            CompanyService.DeleteCompany(Id);
            GetCompanies();
        }
    }
}
