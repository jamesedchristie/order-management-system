using OMS.Controllers;
using OMS.Domain;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OMS.UI.Views
{
    /// <summary>
    /// Interaction logic for AddOrderView.xaml
    /// </summary>
    public partial class AddOrderView : Page
    {
        public AddOrderView(OrderHeader order = null)
        {
            try
            {
                if (order == null)
                {
                    DataContext = OrderController.Instance.CreateNewOrderHeader();
                }
                else
                {
                    DataContext = order;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            InitializeComponent();
        }        

        private void btnAddOrderItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrderItemView((OrderHeader)DataContext));
        }

        private void btnDeleteOrderItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                DataContext = OrderController.Instance.DeleteOrderItem((DataContext as OrderHeader).Id, ((e.Source as Button).DataContext as OrderItem).StockItemId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }       
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderController.Instance.SubmitOrder((DataContext as OrderHeader).Id);
                NavigationService.Navigate(new OrdersView());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDiscardOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderController.Instance.DeleteOrderHeaderAndOrderItems((DataContext as OrderHeader).Id);
                NavigationService.Navigate(new OrdersView());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
