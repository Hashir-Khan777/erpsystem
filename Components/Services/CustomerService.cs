using Microsoft.EntityFrameworkCore;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Services
{
    public class CustomerService
    {
        private readonly ApplicationDbContext db;

        public CustomerService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Customer> GetCustomers()
        {
            return db.Customers.Include(c => c.CreatedBy).Include(c => c.Company).ToList();
        }

        public List<Customer> GetCustomersByManagerId(string ManagerId)
        {
            return db.Customers.Where(c => c.Company.ManagerId == ManagerId).Include(c => c.CreatedBy).Include(c => c.Company).ToList();
        }

        public List<Customer> GetCustomersByCompanyId(int? CompanyId)
        {
            return db.Customers.Where(c => c.Company.Id == CompanyId).Include(c => c.CreatedBy).Include(c => c.Company).ToList();
        }

        public void AddCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public void DeleteCustomer(int Id)
        {
            var customer = db.Customers.Find(Id);

            if (customer is not null)
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
        }
    }
}
