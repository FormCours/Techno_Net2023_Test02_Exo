using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing_02_Exo.BLL.Models;

namespace Testing_02_Exo.BLL.Interfaces
{
    public interface IPurchaseService
    {
        public IEnumerable<Purchase> GetById();
        public IEnumerable<Purchase> GetOrderOfDay();
        public IEnumerable<Purchase> GetOrderOfDay(DateTime date);

        public Purchase CreatePurchase(int orderId, Customer customer);
        public bool CompletePurchase(int purchaseId, bool onDelivery);
    }
}
