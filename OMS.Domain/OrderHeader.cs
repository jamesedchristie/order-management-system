using System;
using System.Collections.Generic;
using System.Linq;

namespace OMS.Domain
{
    public class OrderHeader
    {
        private List<OrderItem> _orderItems = new List<OrderItem>();
        private OrderState _state;
        public OrderHeader(int id, DateTime dateTime, int stateId)
        {
            Id = id;
            DateTime = dateTime;
            setState(stateId);
        }
        public DateTime DateTime { get; private set; }
        public int Id { get; private set; }
        public OrderStates State => _state.State;
        public IEnumerable<OrderItem> OrderItems => _orderItems;

        public decimal Total => OrderItems.Sum(i => i.Total);
        private void setState(int stateId)
        {
            switch(stateId)
            {
                case 1:
                    _state = new OrderNew(this);
                    break;
                case 2:
                    _state = new OrderPending(this);
                    break;
                case 3:
                    _state = new OrderRejected(this);
                    break;
                case 4:
                    _state = new OrderComplete(this);
                    break;
                default:
                    throw new InvalidOrderStateException("Invalid StateId detected");
            }
        }
        public OrderItem AddOrderItem(int stockItemId, decimal price, string description, int quantity)
        {
            OrderItem item = null;
            foreach (var i in _orderItems)
            {
                if (i.StockItemId == stockItemId)
                {
                    item = i;
                }
            }
            if (item == null)
            {
                item = new OrderItem(this, stockItemId, price, description, quantity);
                _orderItems.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
            return item;
        }
        public void Submit()
        {
            _state.Submit(ref _state);
        }
        public void Reject()
        {
            _state.Reject(ref _state);
        }
        public void Complete()
        {
            _state.Complete(ref _state);
        }
    }
}
