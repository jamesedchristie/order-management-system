using OMS.Controllers;
using OMS.Domain;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OMS.UI.Views
{
    /// <summary>
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : Page
    {
        public OrdersView()
        {
            InitializeComponent();
            try
            {
                dgOrders.ItemsSource = OrderController.Instance.GetOrderHeaders();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrderView());
        }

        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            var order = (OrderHeader)((Button)e.Source).DataContext;
            NavigationService.Navigate(new OrderDetailView(order));
        }

        private void btnManageStock_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManageStockItemsView());
        }
    }
}
