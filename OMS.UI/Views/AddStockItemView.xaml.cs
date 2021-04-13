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
    /// Interaction logic for AddStockItemView.xaml
    /// </summary>
    public partial class AddStockItemView : Page
    {        
        public AddStockItemView()
        {
            InitializeComponent();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = txtItemName.Text;
                decimal price;
                int inStock;
                if (name == "")
                {
                    throw new Exception("Please add a name for the stock item");
                }
                try
                {
                    price = decimal.Parse(txtItemPrice.Text);
                }
                catch (FormatException)
                {
                    throw new Exception("Price given not in correct format");
                }
                if (price < 0)
                {
                    throw new Exception("Price must be higher than zero (0)");
                }
                try
                {
                    inStock = int.Parse(txtItemInStock.Text);
                }
                catch (FormatException)
                {
                    throw new Exception("In Stock must be a whole number in numeric format");
                }
                if (inStock < 0)
                {
                    throw new Exception("In Stock quantity must be higher than zero (0)");
                }
                StockItemController.Instance.AddStockItem(name, price, inStock);
                NavigationService.Navigate(new ManageStockItemsView());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManageStockItemsView());
        }
    }
}
