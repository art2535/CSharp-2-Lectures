using Microsoft.Win32;
using ShopClassLibrary_C224;
using ShopClassLibrary1;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;

namespace ShopWpfApp_C224
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigatorOfPage.MainFrame = MainFraim;
            MainFraim.Navigate(new PageAllProducts());
        }

        private void buttonAllProduct_Click(object sender, RoutedEventArgs e)
        {
            MainFraim.Navigate(new PageAllProducts());
        }

        private void buttonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            MainFraim.Navigate(new PageProduct());
        }

        private void buttonLoadFromTxt_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenDialog("Текстовые файлы | *.txt | Все файлы | *.*");
            if (fileName == null)
                return;
            using (StreamReader reader = new StreamReader(fileName, true))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Product prod = new Product();
                    prod.Category = (Categories)Enum.Parse(typeof(Categories), reader.ReadLine());
                    prod.Name = reader.ReadLine();
                    prod.Price = int.Parse(reader.ReadLine());
                    prod.Count = double.Parse(reader.ReadLine());
                    prod.Unit = (Units)Enum.Parse(typeof(Units), reader.ReadLine());
                    prod.Description = reader.ReadLine();
                    prod.Delivery = bool.Parse(reader.ReadLine());
                    prod.ReceiptDate = DateTime.Parse(reader.ReadLine());
                    prod.ImagePath = reader.ReadLine();
                    DataBaseProduct.Stock.Add(prod);
                }
            }
            NavigatorOfPage.MainFrame.Navigate(new PageAllProducts());
        }

        private string OpenDialog(string filter)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Имя файла для открытия базы данных";
            openDialog.Filter = filter;
            openDialog.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (openDialog.ShowDialog() != true)
                return null;
            openDialog.Multiselect = false;
            openDialog.CheckFileExists = true;
            return openDialog.FileName;
        }

        private void buttonSaveToTxt_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenSaveDialog("Текстовые файлы | *.txt | Все файлы | *.*", "txt");
            if (fileName == null)
                return;
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                foreach (var prod in DataBaseProduct.Stock)
                {
                    writer.WriteLine(prod.Id);
                    writer.WriteLine(prod.Category);
                    writer.WriteLine(prod.Name);
                    writer.WriteLine(prod.Price);
                    writer.WriteLine(prod.Count);
                    writer.WriteLine(prod.Unit);
                    writer.WriteLine(prod.Description);
                    writer.WriteLine(prod.Delivery);
                    writer.WriteLine(prod.ReceiptDate);
                    writer.WriteLine(prod.ImagePath);
                }
            }
        }

        private string OpenSaveDialog(string filter, string defaultExt)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Имя файла для сохранения базы данных";
            saveDialog.Filter = filter;
            saveDialog.DefaultExt = defaultExt;
            saveDialog.FileName = "База данных";
            saveDialog.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            saveDialog.OverwritePrompt = true;
            if (saveDialog.ShowDialog() != true)
                return null;
            return saveDialog.FileName;
        }

        private void buttonLoadFromXml_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenDialog("XML-файлы | *.xml | Все файлы | *.*");
            if (fileName == null)
                return;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            XmlElement xmlElementStock = xmlDocument.DocumentElement;
            if (xmlElementStock == null)
                return;
            foreach (XmlElement xmlElementProduct in xmlElementStock)
            {
                Product product = new Product();
                XmlNode attributeName = xmlElementProduct.Attributes["name"];
                product.Name = attributeName.Value;
                foreach (XmlNode childNode in xmlElementProduct.ChildNodes)
                {
                    switch(childNode.Name)
                    {
                        case "Price":
                            product.Price = int.Parse(childNode.InnerText);
                            break;
                        case "Count":
                            product.Count = int.Parse(childNode.InnerText);
                            break;
                        case "Category":
                            product.Category = (Categories)Enum.Parse(typeof(Categories), childNode.InnerText);
                            break;
                        case "Unit":
                            product.Unit = (Units)Enum.Parse(typeof(Units), childNode.InnerText);
                            break;
                        case "Delivery":
                            product.Delivery = bool.Parse(childNode.InnerText);
                            break;
                        case "Description":
                            product.Description = childNode.InnerText;
                            break;
                        case "ReceiptData":
                            product.ReceiptDate = DateTime.Parse(childNode.InnerText);
                            break;
                        case "ImagePath":
                            product.ImagePath = childNode.InnerText;
                            break;
                    }
                }
                DataBaseProduct.Stock.Add(product);
            }
            NavigatorOfPage.MainFrame.Navigate(new PageAllProducts());
        }

        private void buttonSaveToXml_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenSaveDialog("XML-файлы | *.xml | Все файлы | *.*", "xml");
            if (fileName == null)
                return;
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode xmlNodeStock = xmlDocument.CreateElement("Stock");
            xmlDocument.AppendChild(xmlNodeStock);
            foreach (var product in DataBaseProduct.Stock)
            {
                XmlNode xmlNodeProduct = xmlDocument.CreateElement("Product");
                XmlAttribute xmlAttributeName = xmlDocument.CreateAttribute("Name");
                xmlAttributeName.Value = product.Name;
                xmlNodeProduct.Attributes.Append(xmlAttributeName);
                xmlNodeProduct.AppendChild(CreateElement(xmlDocument, "Price", product.Price.ToString()));
                xmlNodeProduct.AppendChild(CreateElement(xmlDocument, "Count", product.Count.ToString()));
                xmlNodeProduct.AppendChild(CreateElement(xmlDocument, "Unit", product.Unit.ToString()));
                xmlNodeProduct.AppendChild(CreateElement(xmlDocument, "Category", product.Category.ToString()));
                if (!string.IsNullOrEmpty(product.Description))
                    xmlNodeProduct.AppendChild(CreateElement(xmlDocument, "Description", product.Description.ToString()));
                xmlNodeProduct.AppendChild(CreateElement(xmlDocument, "Price", product.Delivery.ToString()));
                xmlNodeProduct.AppendChild(CreateElement(xmlDocument, "ReceiptDate", product.ReceiptDate.ToString()));
                xmlNodeProduct.AppendChild(CreateElement(xmlDocument, "Image", product.ImagePath.ToString()));
                xmlNodeStock.AppendChild(xmlNodeProduct);
            }
            xmlDocument.Save(fileName);
        }

        private XmlElement CreateElement(XmlDocument xmlDocument, string nameElement, string element)
        {
            XmlElement xmlElement = xmlDocument.CreateElement(nameElement);
            xmlElement.AppendChild(xmlDocument.CreateTextNode(element));
            return xmlElement;
        }

        private void buttonSaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenSaveDialog("Excel-файлы | *.xlsx | Все файлы | *.*", "xlsx");
            if (fileName == null)
                return;
            DataBaseProduct.Stock.SaveToExcel(fileName);
        }

        private void buttonSaveToWord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonLoadFromXmlSer_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenDialog("XML-файлы | *.xml | Все файлы | *.*");
            if (string.IsNullOrEmpty(fileName))
                return;
            if (DataBaseProduct.Stock.Count > 0)
            {
                var result = MessageBox.Show("База данных не пустая. Очистить ее?", "Очистить БД?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    DataBaseProduct.Stock.Clear();
                if (result == MessageBoxResult.No)
                    return;
            }
            DataBaseProduct.Stock.LoadFromXmlSer(fileName);
            NavigatorOfPage.MainFrame.Navigate(new PageAllProducts());
        }

        private void buttonSaveToXmlSer_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenSaveDialog("XML-файлы | *.xml | Все файлы | *.*", "xml");
            if (string.IsNullOrEmpty(fileName))
                return;
            DataBaseProduct.Stock.SaveToXmlSer(fileName);
        }

        private void buttonLoadFromJson_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenDialog("JSON-файлы | *.json | Все файлы | *.*");
            if (string.IsNullOrEmpty(fileName))
                return;
            DataBaseProduct.Stock.LoadFromJson(fileName);
        }

        private void buttonSaveToJson_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenSaveDialog("JSON-файлы | *.json | Все файлы | *.*", "json");
            if (string.IsNullOrEmpty(fileName))
                return;
            DataBaseProduct.Stock.SaveToJson(fileName);
        }
    }
}
