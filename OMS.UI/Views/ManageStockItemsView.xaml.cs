using OMS.Controllers;
using OMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OMS.UI.Views
{
    /// <summary>
    /// Interaction logic for ManageStockItemsView.xaml
    /// </summary>
    public partial class ManageStockItemsView : Page
    {
        public ManageStockItemsView()
        {
            InitializeComponent();
            dgStockItems.ItemsSource = StockItemController.Instance.GetStockItems();
        }

        private void btnUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            var item = (StockItem)dgStockItems.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Please select a stock item to update", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                NavigationService.Navigate(new UpdateItemView(item));
            }
        }

        private void btnAddNewItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddStockItemView());
        }

        private void btnNavBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersView());
        }
    }
}
