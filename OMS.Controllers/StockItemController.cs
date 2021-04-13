using OMS.DAL;
using OMS.Domain;
using System.Collections.Generic;

namespace OMS.Controllers
{
    public class StockItemController
    {
        private readonly OrderRepo orderRepo = new OrderRepo();
        private readonly StockRepo stockRepo = new StockRepo();
        private static StockItemController _instance;

        private StockItemController() { }

        public static StockItemController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StockItemController();
                }
                return _instance;
            }
        }

        public IEnumerable<StockItem> GetStockItems()
        {
            return stockRepo.GetStockItems();
        }

        public void UpdateStockItem(StockItem item)
        {
            stockRepo.UpdateStockItem(item.Id, item.Name, item.Price, item.InStock);
        }

        public void AddStockItem(string name, decimal price, int inStock)
        {
            stockRepo.InsertStockItem(name, price, inStock);
        }
    }
}
