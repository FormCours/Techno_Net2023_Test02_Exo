namespace Testing_02_Exo.BLL.Models
{
    public class Order
    {
        public class ProductOrder
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }

        public Order(int id, DateTime orderDate)
        {
            Id = id;
            OrderDate = orderDate;
            ProductOrders = new List<ProductOrder>();
        }

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
    }
}
