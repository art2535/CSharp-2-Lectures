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
using ShopClassLibrary_C224;
using ShopClassLibrary1;

namespace ShopWpfApp_C224
{
    /// <summary>
    /// Логика взаимодействия для PageAllProducts.xaml
    /// </summary>
    public partial class PageAllProducts : Page
    {
        public PageAllProducts()
        {
            InitializeComponent();            
            GridAllProducts.ItemsSource = DataBaseProduct.Stock;
            GridAllProducts.MaxHeight = (NavigatorOfPage.MainFrame.DesiredSize.Height > 400) ? NavigatorOfPage.MainFrame.DesiredSize.Height : 400;
            comboFilterCategory.ItemsSource = DataBaseProduct.GetCategories();
            comboFilterCategory.SelectedIndex = 0;
        }

        private void buttonEditProduct_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = GridAllProducts.SelectedItem as Product;
            NavigatorOfPage.MainFrame.Navigate(new PageProduct(selectedProduct));
        }

        private void buttonDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = GridAllProducts.SelectedItem as Product;
            DataBaseProduct.Stock.Remove(selectedProduct);
        }

        private void comboFilterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboFilterCategory.SelectedIndex == 0)
                GridAllProducts.ItemsSource = DataBaseProduct.Stock;
            else
                GridAllProducts.ItemsSource = DataBaseProduct.Stock.FilterCategory(comboFilterCategory.SelectedValue.ToString());
            GridAllProducts.Items.Refresh();
        }

        private void textFilterName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
