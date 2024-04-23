using Microsoft.AspNetCore.Identity;

namespace ZiniTechERPSystem.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int? CompanyId { get; set; }

        public Company? Company { get; set; }

        public List<Company>? Companies { get; set; }

        public List<Product>? Products { get; set; }

        public List<Customer>? Customers { get; set; }

        public List<Invoice>? Invoices { get; set; }

        public List<EmployeeTask>? AssignedTasks { get; set; }

        public List<AuditLog>? Logs { get; set; }
    }
}
