using System;
using System.Linq;

namespace OMS.Domain
{
    public class OrderNew : OrderState
    {
        public OrderNew(OrderHeader orderHeader) : base(orderHeader) { }
        public override OrderStates State => OrderStates.New;
        public override void Complete(ref OrderState state)
        {
            throw new InvalidOrderStateException("A new order must be submitted (pending) before it can be completed");
        }
        public override void Reject(ref OrderState state)
        {
            throw new InvalidOrderStateException("A new order must be submitted (pending) before it can be rejected");
        }
        public override void Submit(ref OrderState state)
        {
            //An order must have at least one order item
            if (!_orderHeader.OrderItems.Any())
            {
                throw new InvalidOrderStateException("A new order must have at least one item before it can be submitted.");
            }
            state = new OrderPending(_orderHeader);
        }
    }
}
