using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OMS.Controllers;
using OMS.Domain;

namespace OMS.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Initialise()
        {
            OrderController.Instance.ResetDatabase();
        }

        [TestCleanup]
        public void CleanUp()
        {
            OrderController.Instance.ResetDatabase();
        }

        [TestMethod]
        public void CreateNewOrderHeader_ShouldHaveIdGenerated()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            Assert.IsTrue(order.Id > 0);
        }

        [TestMethod]
        public void CreateNewOrderHeader_ShouldHaveDateTime()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            Assert.IsTrue(order.DateTime != null);
        }

        [TestMethod]
        public void CreateNewOrderHeader_OrderStateShouldBeNew()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            Assert.IsTrue(order.State == OrderStates.New);
        }

        [TestMethod]
        public void GetOrderHeaders_ShouldGetOrders()
        {
            var stockItems = StockItemController.Instance.GetStockItems();
            var orders = new List<OrderHeader>();
            foreach(var si in stockItems)
            {
                var order = OrderController.Instance.CreateNewOrderHeader();
                order = OrderController.Instance.UpsertOrderItem(order.Id, si.Id, 1);
                orders.Add(order);
            }
            var testOrders = OrderController.Instance.GetOrderHeaders();
            Assert.IsTrue(orders.Count == testOrders.Count());
        }

        [TestMethod]
        public void SubmitEmptyOrder_ShouldThrowException()
        {
            var order = OrderController.Instance.CreateNewOrderHeader();
            Action submit = order.Submit;
            Assert.ThrowsException<InvalidOrderStateException>(submit);            
        }

        [TestMethod]
        public void ProcessInvalidOrder_ShouldBeRejected()
        {
            var stockItems = StockItemController.Instance.GetStockItems();
            var item = stockItems.First();
            var inStock = item.InStock;
            var order = OrderController.Instance.CreateNewOrderHeader();
            order = OrderController.Instance.UpsertOrderItem(order.Id, item.Id, inStock + 1);
            order = OrderController.Instance.SubmitOrder(order.Id);
            order = OrderController.Instance.ProcessOrder(order.Id);
            Assert.AreEqual(OrderStates.Rejected, order.State);
        }
    }
}
