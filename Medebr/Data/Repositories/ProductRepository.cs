using Medebr.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medebr.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MedebrContext _ctx;

        public ProductRepository(MedebrContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Product> GetAllProducts() {
            return _ctx.Products
                       .OrderBy(p => p.Name)
                       .ToList();
        }

        public IEnumerable<Product> GetProductsByCatagory(string catagory) {
            return _ctx.Products
                       .Where(p => p.Catagory == catagory)
                       .ToList();
        }
        public Product GetProductsById(int id)
        {
            return _ctx.Products
                       .SingleOrDefault(p => p.ProductId == id);
        }
        public void DeleteAllProduts()
        {
            foreach (var p in GetAllProducts()) {
                _ctx.Products.Remove(p);
            }
            _ctx.SaveChanges();
        }

        public void Save(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
        }
    }
}
