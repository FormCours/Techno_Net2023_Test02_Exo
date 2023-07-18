using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing_02_Exo.BLL.Models;

namespace Testing_02_Exo.BLL.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<Product> GetAll();
        public Product? GetById(int id);
        public int Add(Product product);
    }
}
