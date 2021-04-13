namespace OMS.Domain
{
    public abstract class OrderState
    {
        protected OrderHeader _orderHeader;
        public OrderState(OrderHeader orderHeader)
        {
            _orderHeader = orderHeader;
        }
        public abstract OrderStates State { get; }
        public abstract void Submit(ref OrderState state);
        public abstract void Complete(ref OrderState state);
        public abstract void Reject(ref OrderState state);
    }
}
