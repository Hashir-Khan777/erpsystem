using Microsoft.AspNetCore.Components;
using ZiniTechERPSystem.Data;
using ZiniTechERPSystem.Components.Services;

namespace ZiniTechERPSystem.Components.Admin.Pages
{
    public partial class CustomerPage
    {
        public List<Customer> customers = new List<Customer>();

        [Inject]
        private CustomerService CustomerService { get; set; }

        protected override void OnInitialized()
        {
            GetCustomners();
        }

        public void GetCustomners()
        {
            customers = CustomerService.GetCustomers().ToList();
        }

        public void DeleteCustomer(int Id)
        {
            CustomerService.DeleteCustomer(Id);
            GetCustomners();
        }
    }
}
