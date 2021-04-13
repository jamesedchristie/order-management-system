namespace OMS.Domain
{
    public class OrderRejected : OrderState
    {
        public OrderRejected(OrderHeader orderHeader) : base(orderHeader) { }
        public override OrderStates State => OrderStates.Rejected;
        public override void Complete(ref OrderState state)
        {
            throw new InvalidOrderStateException("A rejected order cannot be completed");
        }
        public override void Reject(ref OrderState state)
        {
            throw new InvalidOrderStateException("A rejected order cannot be rejected again");
        }
        public override void Submit(ref OrderState state)
        {
            throw new InvalidOrderStateException("A rejected order cannot be submitted again");
        }
    }
}
