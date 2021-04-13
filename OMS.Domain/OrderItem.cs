namespace OMS.Domain
{
    public class OrderItem
    {
        public OrderItem(OrderHeader order, int stockItemId, decimal price, string description, int quantity)
        {
            OrderHeader = order;
            OrderHeaderId = order.Id;
            StockItemId = stockItemId;
            Price = price;
            Description = description;
            Quantity = quantity;
        }

        public string Description { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public int OrderHeaderId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int StockItemId { get; set; }
        public decimal Total => Quantity * Price;
    }
}
