using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing_02_Exo.BLL.Interfaces;
using Testing_02_Exo.BLL.Models;

namespace Testing_02_Exo.BLL.Test.Fakes
{
    internal class ProductServiceFake : IProductService
    {
        #region Donnée pour les tests
        private readonly IEnumerable<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Test 1", Price = 10, IsFrozen = false },
            new Product { Id = 2, Name = "Test 2", Price = 1.5, IsFrozen = false },
            new Product { Id = 3, Name = "Test 3", Price = 5, IsFrozen = true },
        };
        #endregion


        public int Add(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(int id)
        {
            return _products.SingleOrDefault(p =>  p.Id == id);
        }
    }
}
