using System;

namespace OMS.Domain
{
    public class OrderPending : OrderState
    {
        public OrderPending(OrderHeader orderHeader) : base(orderHeader) { }
        public override OrderStates State => OrderStates.Pending;
        public override void Complete(ref OrderState state)
        {
            state = new OrderComplete(_orderHeader);
        }
        public override void Reject(ref OrderState state)
        {
            state = new OrderRejected(_orderHeader);
        }
        public override void Submit(ref OrderState state)
        {
            throw new InvalidOrderStateException("A pending order cannot be submitted again");
        }
    }
}
