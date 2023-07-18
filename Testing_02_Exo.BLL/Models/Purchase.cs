using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_02_Exo.BLL.Models
{
    public class Purchase
    {
        public Purchase(Order order)
        {
            Order = order;
            IsComplet = false;
        }

        public Order Order { get; set; }
        public Customer? Customer { get; set; }
        public bool IsComplet { get; set; }

        public bool IsTakeAway { get; set; }
        public bool IsPaid { get; set; }
        public double Delivery { get; set; }
        public double Price { get; set; }
    }
}
