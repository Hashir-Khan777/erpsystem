using Microsoft.AspNetCore.Components;
using ZiniTechERPSystem.Components.Services;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Admin.Pages
{
    public partial class ProductPage
    {
        public List<Product> products = new List<Product>();

        [Inject]
        public ProductService ProductService {  get; set; }

        protected override void OnInitialized()
        {
            GetProducts();
        }

        public void GetProducts()
        {
            products = ProductService.GetProducts().ToList();
        }
    }
}
