namespace OMS.Domain
{
    public class OrderComplete : OrderState
    {
        public OrderComplete(OrderHeader orderHeader) : base(orderHeader) { }
        public override OrderStates State => OrderStates.Complete;
        public override void Complete(ref OrderState state)
        {
            throw new InvalidOrderStateException("A completed order cannot be completed again");
        }
        public override void Reject(ref OrderState state)
        {
            throw new InvalidOrderStateException("A completed order cannot be rejected");
        }
        public override void Submit(ref OrderState state)
        {
            throw new InvalidOrderStateException("A completed order cannot be submitted again");
        }
    }
}
