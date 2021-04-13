using OMS.Controllers;
using OMS.Domain;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OMS.UI.Views
{
    /// <summary>
    /// Interaction logic for OrderDetailView.xaml
    /// </summary>
    public partial class OrderDetailView : Page
    {
        public OrderDetailView(OrderHeader order)
        {
            DataContext = order;
            InitializeComponent();            
        }
        private void btnNavDetailsToOrders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersView());
        }

        private void btnProcessOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = (OrderHeader)DataContext;
                if (order.State == OrderStates.New)
                {
                    DataContext = OrderController.Instance.SubmitOrder(order.Id);
                }
                if (order.State == OrderStates.Pending)
                {
                    DataContext = OrderController.Instance.ProcessOrder(order.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
