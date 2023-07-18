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
        public Purchase? GetById(int id);
        public IEnumerable<Purchase> GetPurchaseOfDay();
        public IEnumerable<Purchase> GetPurchaseOfDay(DateTime date);

        public Purchase CreatePurchase(int orderId, Customer customer);
        public bool CompletePurchase(int orderId, bool onDelivery);
    }
}
