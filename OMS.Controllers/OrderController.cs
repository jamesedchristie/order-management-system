using OMS.DAL;
using OMS.Domain;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OMS.Controllers
{
    public class OrderController
    {
        private readonly OrderRepo orderRepo = new OrderRepo();
        private readonly StockRepo stockRepo = new StockRepo();
        private static OrderController _instance;

        private OrderController() { }

        public static OrderController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderController();
                }
                return _instance;
            }
        }

        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            return orderRepo.GetOrderHeaders();
        }
        public OrderHeader CreateNewOrderHeader()
        {
            return orderRepo.InsertOrderHeader();
        }
        public OrderHeader UpsertOrderItem(int orderHeaderId, int stockItemId, int quantity)
        {
            StockItem item = stockRepo.GetStockItem(stockItemId);
            OrderHeader order = orderRepo.GetOrderHeader(orderHeaderId);
            var orderItem = order.AddOrderItem(stockItemId, item.Price, item.Name, quantity);
            orderRepo.UpsertOrderItem(orderItem);
            return order;
        }
        public OrderHeader SubmitOrder(int orderHeaderId)
        {
            var order = orderRepo.GetOrderHeader(orderHeaderId);
            order.Submit();
            orderRepo.UpdateOrderState(order);
            return order;
        }
        public OrderHeader ProcessOrder(int orderHeaderId)
        {
            var order = orderRepo.GetOrderHeader(orderHeaderId);
            try
            {
                stockRepo.UpdateStockItemAmount(order);
                order.Complete();
            }
            catch (SqlException ex)
            {
                order.Reject();
            }
            orderRepo.UpdateOrderState(order);
            return order;            
        }
        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            orderRepo.DeleteOrderHeaderAndOrderItems(orderHeaderId);
        }
        public OrderHeader DeleteOrderItem(int orderHeaderId, int stockItemId)
        {
            orderRepo.DeleteOrderItem(orderHeaderId, stockItemId);
            var order = orderRepo.GetOrderHeader(orderHeaderId);
            return order;
        }

        public void ResetDatabase()
        {
            orderRepo.ResetDatabase();
        }
    }
}
