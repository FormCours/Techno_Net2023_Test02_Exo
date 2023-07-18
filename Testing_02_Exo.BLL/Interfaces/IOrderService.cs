using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing_02_Exo.BLL.Models;

namespace Testing_02_Exo.BLL.Interfaces
{
    public interface IOrderService
    {
        public IEnumerable<Order> GetById();
        public IEnumerable<Order> GetOrderOfDay();
        public IEnumerable<Order> GetOrderOfDay(DateTime date);

        public Order CreateNewOrder();
        public bool AddProductIntoOrder(int orderId, int productId, int quantity);
        public bool CanBeDelivered(int orderId);
    }
}
