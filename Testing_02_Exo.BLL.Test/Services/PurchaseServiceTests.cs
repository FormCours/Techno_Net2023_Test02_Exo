using Moq;
using Testing_02_Exo.BLL.Exceptions;
using Testing_02_Exo.BLL.Interfaces;
using Testing_02_Exo.BLL.Models;
using Testing_02_Exo.BLL.Services;

namespace Testing_02_Exo.BLL.Test.Services
{
    public class PurchaseServiceTests
    {
        private IOrderService GetOrderServiceMock()
        {
            // Fake → Mock pour répondre au besoin de mes tests
            IOrderService orderService = Mock.Of<IOrderService>();

            Order order1 = new Order(1, DateTime.Now);
            order1.ProductOrders.Add(new Order.ProductOrder()
            {
                Product = new Product { Id = 1, Name = "Test 1", Price = 10, IsFrozen = false },
                Quantity = 10
            });
            order1.ProductOrders.Add(new Order.ProductOrder()
            {
                Product = new Product { Id = 3, Name = "Test 3", Price = 5, IsFrozen = true },
                Quantity = 1
            });

            Mock.Get(orderService).Setup(m => m.GetById(1))
                                  .Returns(order1);

            return orderService;
        }


        [Fact]
        public void PurchaseService_CreatePurchase()
        {
            // Arrange 
            PurchaseService purchaseService = new PurchaseService(GetOrderServiceMock());
            Customer customer = new Customer(1, "Della", "Duck", "Rue du test 42, Belgique");
            int orderId = 1;

            // Action
            Purchase purchase = purchaseService.CreatePurchase(orderId, customer);

            // Assert
            Assert.NotNull(purchase);
        }

        [Fact]
        public void PurchaseService_CreatePurchase_UnknowOrder()
        {
            // Arrange 
            PurchaseService purchaseService = new PurchaseService(GetOrderServiceMock());
            Customer customer = new Customer(1, "Della", "Duck", "Rue du test 42, Belgique");
            int orderId = 42;

            // Action + Assert
            Assert.Throws<NotFoundOrderExeption>(() =>
            {
                Purchase purchase = purchaseService.CreatePurchase(orderId, customer);
            });
        }

        [Fact]
        public void PurchaseService_CreatePurchase_AlreadyExists()
        {
            // Arrange 
            PurchaseService purchaseService = new PurchaseService(GetOrderServiceMock());
            Customer customer = new Customer(1, "Della", "Duck", "Rue du test 42, Belgique");
            int orderId = 1;

            // Action + Assert
            Assert.Throws<AlreadyExistsPurchaseException>(() =>
            {
                purchaseService.CreatePurchase(orderId, customer);
                purchaseService.CreatePurchase(orderId, customer);
            });
        }
    }
}
