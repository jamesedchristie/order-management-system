using OMS.Controllers;
using OMS.Domain;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OMS.UI.Views
{
    /// <summary>
    /// Interaction logic for AddOrderItemView.xaml
    /// </summary>
    public partial class AddOrderItemView : Page
    {
        private OrderHeader _order;
        public IEnumerable<StockItem> StockItems { get; private set; }
        public int Quantity { get; set; } = 1;
        public AddOrderItemView(OrderHeader order)
        {
            try
            {
                _order = order;
                StockItems = StockItemController.Instance.GetStockItems();
                DataContext = this;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            InitializeComponent();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            var item = (StockItem)dgStockItems.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Please select a stock item", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (Quantity < 1)
            {
                MessageBox.Show("Quantity must be greater than zero (0)", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (Quantity > item.InStock)
            {
                MessageBox.Show($"There is currently not enough items in stock.\nRequest: {Quantity}\nIn Stock: {item.InStock}\nYour order may be rejected.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            try
            {
                var order = OrderController.Instance.UpsertOrderItem(_order.Id, item.Id, Quantity);
                NavigationService.Navigate(new AddOrderView(order));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnNavBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrderView(_order));
        }
    }
}
