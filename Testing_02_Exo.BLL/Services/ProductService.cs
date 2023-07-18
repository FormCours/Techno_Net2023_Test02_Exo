using System.Collections.ObjectModel;
using Testing_02_Exo.BLL.Interfaces;
using Testing_02_Exo.BLL.Models;

namespace Testing_02_Exo.BLL.Services
{
    public class ProductService : IProductService
    {
        #region Liste static de données → Cas réel : un acces DB
        private IList<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Margarita", Price = 10, IsFrozen = false },
            new Product { Id = 2, Name = "Diavolo", Price = 12, IsFrozen = false },
            new Product { Id = 3, Name = "Quatre fromages", Price = 14, IsFrozen = false },
            new Product { Id = 4, Name = "Végétarienne", Price = 14, IsFrozen = false },
            new Product { Id = 5, Name = "Hawaï", Price = 13, IsFrozen = false },
            new Product { Id = 6, Name = "Sorbet", Price = 7, IsFrozen = true },
            new Product { Id = 7, Name = "Dame blanche", Price = 8.5, IsFrozen = true },
            new Product { Id = 8, Name = "Coupe Disney", Price = 3.95, IsFrozen = true },
        };

        private int _NextId = 9;
        #endregion

        public IEnumerable<Product> GetAll()
        {
            return new ReadOnlyCollection<Product>(_products);
        }

        public Product? GetById(int id)
        {
            return _products.SingleOrDefault(p => p.Id == id);
        }

        public int Add(Product product)
        {
            _products.Add(new Product
            {
                Id = _NextId,
                Name = product.Name,
                Price = product.Price,
                IsFrozen = product.IsFrozen,
                Desc = product.Desc
            });

            _NextId++;
            return product.Id;
        }
    }
}
