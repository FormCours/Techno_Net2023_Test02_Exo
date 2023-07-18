using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing_02_Exo.BLL.Exceptions;
using Testing_02_Exo.BLL.Interfaces;
using Testing_02_Exo.BLL.Models;

namespace Testing_02_Exo.BLL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private const int VAT_RATES = 6;
        private const double DELIVERY_COST = 15;

        #region Data
        private IList<Purchase> _purchases;
        #endregion

        #region Services
        private readonly IOrderService _orderService;
        #endregion

        public PurchaseService(IOrderService orderService)
        {
            _orderService = orderService;
            _purchases = new List<Purchase>();
        }

        public Purchase? GetById(int id)
        {
            return _purchases.SingleOrDefault(p => p.Order.Id == id);
        }

        public IEnumerable<Purchase> GetPurchaseOfDay()
        {
            return GetPurchaseOfDay(DateTime.Today);
        }

        public IEnumerable<Purchase> GetPurchaseOfDay(DateTime date)
        {
            return _purchases.Where(p => p.Order.OrderDate.Date == date.Date);
        }

        public Purchase CreatePurchase(int orderId, Customer customer)
        {
            Order? order = _orderService.GetById(orderId);
            if (order is null)
            {
                throw new NotFoundOrderExeption();
            }

            if(GetById(orderId) != null)
            {
                throw new AlreadyExistsPurchaseException();
            }

            Purchase purchase = new Purchase(order);
            purchase.Customer = customer;

            _purchases.Add(purchase);

            return purchase;
        }

        public bool CompletePurchase(int orderId, bool onDelivery)
        {
            Purchase? purchase = GetById(orderId);
            if (purchase is null)
            {
                throw new NotFoundPurchaseException();
            }

            if(onDelivery && !_orderService.CanBeDelivered(orderId))
            {
                throw new DeliveryNotAllowPurchaseException();
            }

            double price = purchase.Order.ProductOrders.Sum(po => po.Product.Price * po.Quantity);

            if(!onDelivery)
            {
                price = ApplyTakeawayDiscount(price);
            }
                                         
            purchase.Price = ApplyPriceTVA(price);
            purchase.Delivery = onDelivery ? DELIVERY_COST : 0;
            purchase.IsTakeAway = !onDelivery;

            purchase.IsComplet = true;
            return true;
        }

        private double ApplyPriceTVA(double price)
        {
            return price + ((price * VAT_RATES) / 100);
        }

        private double ApplyTakeawayDiscount(double price)
        {
            if(DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
            {
                price = price - ((price * 21) / 100);
            }

            return price;
        }
    }
}
