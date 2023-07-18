using Testing_02_Exo.BLL.Exceptions;
using Testing_02_Exo.BLL.Interfaces;
using Testing_02_Exo.BLL.Models;
using Testing_02_Exo.BLL.Services;
using Testing_02_Exo.BLL.Test.Fakes;

namespace Testing_02_Exo.BLL.Test.Services
{
    public class OrderServiceTests
    {
        // Fake
        // - Mock : Fake créer pour un sénario de test.
        //          Le Mock est configuré en fonction du resultat attendu du test.
        // - Stub : Fake qui permet de remplacer une dépendence (Simulation du fonctionnement)


        private IProductService GetProductServiceStub()
        {
            return new ProductServiceFake();
        }

        [Fact]
        public void OrderService_CreateOrder()
        {
            // Arrange
            OrderService orderService = new OrderService(GetProductServiceStub());

            // Action
            Order order = orderService.CreateNewOrder();

            // Assert
            Assert.NotNull(order);
        }


        [Fact]
        public void OrderService_AddProduct_TwoUniqueProduct()
        {
            // Arrange
            OrderService orderService = new OrderService(GetProductServiceStub());

            int orderId = orderService.CreateNewOrder().Id;
            int productId1 = 1;
            int quantity1 = 10;
            int productId2 = 2;
            int quantity2 = 5;

            // Action
            bool added1 = orderService.AddProductIntoOrder(orderId, productId1, quantity1);
            bool added2 = orderService.AddProductIntoOrder(orderId, productId2, quantity2);
            Order actual = orderService.GetById(orderId)!;

            // Assert
            Assert.True(added1);
            Assert.True(added2);
            Assert.NotEmpty(actual.ProductOrders);

            Assert.Collection(actual.ProductOrders,
              po1 =>
              {
                  Assert.Equal(productId1, po1.Product.Id);
                  Assert.Equal(quantity1, po1.Quantity);
              },
              po2 =>
              {
                  Assert.Equal(productId2, po2.Product.Id);
                  Assert.Equal(quantity2, po2.Quantity);
              });
        }

        [Fact]
        public void OrderService_AddProduct_TwoSameProduct()
        {
            // Arrange
            OrderService orderService = new OrderService(GetProductServiceStub());

            int orderId = orderService.CreateNewOrder().Id;
            int productId1 = 1;
            int quantity1 = 10;
            int quantity2 = 5;
            int quantityExpect = 15;

            // Action
            bool added1 = orderService.AddProductIntoOrder(orderId, productId1, quantity1);
            bool added2 = orderService.AddProductIntoOrder(orderId, productId1, quantity2);
            Order actual = orderService.GetById(orderId)!;

            // Assert
            Assert.True(added1);
            Assert.True(added2);
            Assert.NotEmpty(actual.ProductOrders);

            Assert.Collection(actual.ProductOrders,
              po =>
              {
                  Assert.Equal(productId1, po.Product.Id);
                  Assert.Equal(quantityExpect, po.Quantity);
              });
        }

        [Fact]
        public void OrderService_AddProduct_UnknowProduct()
        {
            // Arrange
            OrderService orderService = new OrderService(GetProductServiceStub());

            int orderId = orderService.CreateNewOrder().Id;
            int productId = 1337;
            int quantity = 10;

            // Action
            bool added = orderService.AddProductIntoOrder(orderId, productId, quantity);

            // Assert 
            Assert.False(added);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void OrderService_AddProduct_QuantityOutOfRange(int quantity)
        {
            // Arrange
            OrderService orderService = new OrderService(GetProductServiceStub());

            int orderId = orderService.CreateNewOrder().Id;
            int productId = 1;

            // Action
            bool added = orderService.AddProductIntoOrder(orderId, productId, quantity);

            // Assert 
            Assert.False(added);
        }

        [Fact]
        public void OrderService_AddProduct_NoOrderCreated()
        {
            // Arrange
            OrderService orderService = new OrderService(GetProductServiceStub());
            int productId = 1;
            int quantity = 1;

            // Action + Assert
            Assert.Throws<NotFoundOrderExeption>(() =>
            {
                bool added = orderService.AddProductIntoOrder(42, productId, quantity);
            });
        }


        [Theory]
        [InlineData(1, 2, true)]
        [InlineData(1, 3, false)]
        public void OrderService_CanBeDelivered(int productId1, int productId2, bool expect)
        {

            // Arrange
            OrderService orderService = new OrderService(GetProductServiceStub());

            int orderId = orderService.CreateNewOrder().Id;
            orderService.AddProductIntoOrder(orderId, productId1, 1);
            orderService.AddProductIntoOrder(orderId, productId2, 1);


            // Action
            bool actual = orderService.CanBeDelivered(orderId);

            // Assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void OrderService_CanBeDelivered_NoOrderCreated()
        {
            // Arrange
            OrderService orderService = new OrderService(GetProductServiceStub());

            // Action + Assert
            Assert.Throws<NotFoundOrderExeption>(() =>
            {
                bool added = orderService.CanBeDelivered(42);
            });
        }
    }
}
