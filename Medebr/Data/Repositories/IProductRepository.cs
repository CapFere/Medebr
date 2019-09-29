using System.Collections.Generic;
using Medebr.Data.Entities;

namespace Medebr.Data.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCatagory(string catagory);
        Product GetProductsById(int id);
        void Save(Product product);
        void DeleteAllProduts();
    }
}