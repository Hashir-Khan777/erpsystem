using Microsoft.EntityFrameworkCore;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Services
{
    public class CompanyService
    {
        private readonly ApplicationDbContext db;

        public CompanyService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Company> GetCompanies()
        {
            return db.Companies.Include(c => c.Manager).Include(c => c.Employees).ToList();
        }

        public List<Company> GetCompaniesByManagerId(string ManagerId)
        {
            return db.Companies.Where(c => c.ManagerId == ManagerId).Include(c => c.Manager).Include(c => c.Employees).ToList();
        }

        public void AddCompany(Company company)
        {
            db.Companies.Add(company);
            db.SaveChanges();
        }

        public void DeleteCompany(int Id)
        {
            var company = db.Companies.Find(Id);

            if (company is not null)
            {
                db.Companies.Remove(company);
                db.SaveChanges();
            }
        }
    }
}
