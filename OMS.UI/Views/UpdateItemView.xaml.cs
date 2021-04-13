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
    /// Interaction logic for UpdateItemView.xaml
    /// </summary>
    public partial class UpdateItemView : Page
    {
        StockItem _item;
        public UpdateItemView(StockItem item)
        {
            _item = item;
            DataContext = _item;
            InitializeComponent();
        }

        private void btnNavBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManageStockItemsView());
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (_item.Name == "")
                {
                    throw new Exception("Please add a name for the stock item");
                }
                if (_item.Price < 0)
                {
                    throw new Exception("Price must be higher than zero (0) and given in numeric format");
                }
                if (_item.InStock < 0)
                {
                    throw new Exception("In Stock quantity must be higher than zero (0) and given in numeric format");
                }
                StockItemController.Instance.UpdateStockItem(_item);
                NavigationService.Navigate(new ManageStockItemsView());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
