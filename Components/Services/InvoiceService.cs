using Microsoft.EntityFrameworkCore;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Services
{
    public class InvoiceService
    {
        private readonly ApplicationDbContext db;

        public InvoiceService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Invoice> GetInvoices()
        {
            return db.Invoices.Include(i => i.ProductInvoices).ThenInclude(pi => pi.Product).Include(i => i.Company).Include(i => i.CreatedBy).Include(i => i.Customer).ToList();
        }

        public List<Invoice> GetInvoicesByManagerId(string ManagerId)
        {
            return db.Invoices.Where(i => i.Company.ManagerId == ManagerId).Include(i => i.ProductInvoices).ThenInclude(pi => pi.Product).Include(i => i.Company).Include(i => i.CreatedBy).Include(i => i.Customer).ToList();
        }

        public void AddProductInvoice(Product product, Invoice invoice)
        {
            db.ProductInvoice.Add(new ProductInvoice { InvoiceId = invoice.Id, ProductId = product.Id });
            db.SaveChanges();
        }

        public List<Invoice> GetInvoicesByCompanyId(int? CompanyId)
        {
            return db.Invoices.Where(i => i.Company.Id == CompanyId).Include(i => i.ProductInvoices).ThenInclude(pi => pi.Product).Include(i => i.Company).Include(i => i.CreatedBy).Include(i => i.Customer).ToList();
        }

        public void AddInvoice(Invoice invoice, List<Product> selectedProducts)
        {
            db.Invoices.Add(invoice);
            db.SaveChanges();

            foreach (var product in selectedProducts)
            {
                db.ProductInvoice.Add(new ProductInvoice { InvoiceId = invoice.Id, ProductId = product.Id });
                db.SaveChanges();
            }
        }

        public void DeleteInvoice(int Id)
        {
            var invoice = db.Invoices.Find(Id);

            if (invoice is not null)
            {
                db.Invoices.Remove(invoice);
                db.SaveChanges();
            }
        }
    }
}
