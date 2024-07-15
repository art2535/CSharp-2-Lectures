using Microsoft.Win32;
using ShopClassLibrary_C224;
using ShopClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace ShopWpfApp_C224
{
    /// <summary>
    /// Логика взаимодействия для PageProduct.xaml
    /// </summary>
    public partial class PageProduct : Page
    {
        Product product;
        bool isEditProduct;
        public PageProduct()
        {
            InitializeComponent();
            product = new Product();
            isEditProduct = false;
            DataContext = product;
            comboCategory.ItemsSource = Enum.GetNames(typeof(Categories)).ToList();
            comboUnit.ItemsSource = Enum.GetNames(typeof(Units)).ToList();
        }

        public PageProduct(Product selectedProduct) : this()
        {
            product = selectedProduct;
            DataContext = product;
            isEditProduct = true;
            buttonAddProduct.Content = "Сохранить";
        }

        private void buttonOpenImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображение (png, jpg, bmp) | *.png; *.jpg; *.bmp" + "| Все файлы | *.*";
            openFileDialog.Title = "Выберите файл с изображением товара";
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (openFileDialog.ShowDialog() != true)
                return;
            product.ImagePath = openFileDialog.FileName;
            image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
        }

        private void buttonsaveImagePath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fileName = System.IO.Path.GetFileName(product.ImagePath);
                var newPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\\Photos";
                var newFileName = System.IO.Path.Combine(newPath, fileName);
                System.IO.File.Copy(product.ImagePath, newFileName, true);
                image.Source = new BitmapImage(new Uri(newFileName));
                product.ImagePath = newFileName;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                product.ImagePath = @"Images/nophoto,jpg";
            }
        }

        private void buttonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (!isEditProduct)
                DataBaseProduct.Stock.Add(product);
            NavigatorOfPage.MainFrame.Navigate(new PageAllProducts());
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigatorOfPage.MainFrame.CanGoBack)
                NavigatorOfPage.MainFrame.GoBack();
            else
                NavigatorOfPage.MainFrame.Navigate(new PageAllProducts());
        }
    }
}
