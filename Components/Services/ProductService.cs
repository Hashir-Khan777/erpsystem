﻿using Microsoft.EntityFrameworkCore;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext db;

        public ProductService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Product> GetProducts()
        {
            return db.Products.Include(c => c.Company).Include(c => c.CreatedBy).ToList();
        }

        public Product GetProductById(string ProductId)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == ProductId);
            if (product is not null)
            {
                return product;
            }
            else
            {
                return new Product();
            }
        }

        public List<Product> GetProductsByManagerId(string ManagerId)
        {
            return db.Products.Where(p => p.Company.ManagerId == ManagerId).Include(c => c.Company).Include(c => c.CreatedBy).ToList();
        }

        public List<Product> GetProductsByCompanyId(string? CompanyId)
        {
            return db.Products.Where(p => p.Company.Id == CompanyId).Include(c => c.Company).Include(c => c.CreatedBy).ToList();
        }

        public void AddProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void DeleteProduct(int Id)
        {
            var product = db.Products.Find(Id);

            if (product is not null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
