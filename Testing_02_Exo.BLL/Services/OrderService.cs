using Testing_02_Exo.BLL.Exceptions;
using Testing_02_Exo.BLL.Interfaces;
using Testing_02_Exo.BLL.Models;

namespace Testing_02_Exo.BLL.Services
{
    public class OrderService : IOrderService
    {
        #region Data
        private IList<Order> _orders;
        private int _NextId;
        #endregion

        #region Services
        private readonly IProductService _productService;
        #endregion

        public OrderService(IProductService productService)
        {
            _productService = productService;
            _orders = new List<Order>();
            _NextId = 1;
        }

        public Order? GetById(int id)
        {
            return _orders.SingleOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> GetOrderOfDay()
        {
            return GetOrderOfDay(DateTime.Today);
        }

        public IEnumerable<Order> GetOrderOfDay(DateTime date)
        {
            return _orders.Where(o => o.OrderDate.Date == date.Date);
        }

        public Order CreateNewOrder()
        {
            Order order = new Order(_NextId, DateTime.Now);
            _orders.Add(order);

            return order;
        }

        public bool AddProductIntoOrder(int orderId, int productId, int quantity)
        {
            Order? order = GetById(orderId);
            if (order is null)
            {
                throw new NotFoundOrderExeption();
            }

            Product? product = _productService.GetById(productId);
            if (product is null || quantity <= 0)
            {
                return false;
            }

            Order.ProductOrder? productOrdreAlreadyExists = order.ProductOrders.SingleOrDefault(po => po.Product.Id == productId);

            if (productOrdreAlreadyExists is null)
            {
                order.ProductOrders.Add(new Order.ProductOrder
                {
                    Quantity = quantity,
                    Product = product
                });
            }
            else
            {
                productOrdreAlreadyExists.Quantity += quantity;
            }

            return true;
        }

        public bool CanBeDelivered(int orderId)
        {
            Order? order = GetById(orderId);
            if (order is null)
            {
                throw new NotFoundOrderExeption();
            }

            return !order.ProductOrders.Any(po => po.Product.IsFrozen);
        }
    }
}
