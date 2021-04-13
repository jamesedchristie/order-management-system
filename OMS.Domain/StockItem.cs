namespace OMS.Domain
{
    public class StockItem
    {
        public StockItem(int id, string name, decimal price, int inStock)
        {
            Id = id;
            Name = name;
            Price = price;
            InStock = inStock;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        
    }
}
