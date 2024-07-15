using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_C224.MainWindow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Ответь!", 
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Close();
        }

        private void buttonBold_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
                buttonBold.IsChecked = checkBold.IsChecked;
            if (sender is ToggleButton)
                checkBold.IsChecked = buttonBold.IsChecked;
            if (buttonBold.IsChecked == true)
                labelTitle.FontWeight = FontWeights.Bold;
            else
                labelTitle.FontWeight = FontWeights.Normal;
        }

        private void buttonPitalic_Checked(object sender, RoutedEventArgs e)
        {
            labelTitle.FontStyle = FontStyles.Italic;
            textLong.FontStyle = FontStyles.Italic;
        }

        private void buttonPitalic_Unchecked(object sender, RoutedEventArgs e)
        {
            labelTitle.FontStyle = FontStyles.Normal;
            textLong.FontStyle = FontStyles.Normal;
        }
        private void radioRight_Click(object sender, RoutedEventArgs e)
        {
            labelTitle.HorizontalAlignment = HorizontalAlignment.Right;
        }

        private void radioLeft_Click(object sender, RoutedEventArgs e)
        {
            labelTitle.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void radioCenter_Click(object sender, RoutedEventArgs e)
        {
            labelTitle.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void radioTop_Click(object sender, RoutedEventArgs e)
        {
            labelTitle.VerticalAlignment = VerticalAlignment.Top;
        }

        private void radioDown_Click(object sender, RoutedEventArgs e)
        {
            labelTitle.VerticalAlignment = VerticalAlignment.Bottom;
        }

        private void radioCenterVer_Click(object sender, RoutedEventArgs e)
        {
            labelTitle.VerticalAlignment = VerticalAlignment.Center;
        }

        private void buttonPassword_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "Open Sesam")
                MessageBox.Show("Заходи!");
            else
                MessageBox.Show("Не пущу!");
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                buttonPassword_Click(sender, e);
            }

        }

        private void buttonListBox_Click(object sender, RoutedEventArgs e)
        {
            var select = listBoxSimple.SelectedItem;
            if (select != null)
            {
                if (select is ListBoxItem)
                    MessageBox.Show($"Выбран: {(select as ListBoxItem).Content}" +
                        $" с индексом {listBoxSimple.SelectedIndex}");
                else
                    MessageBox.Show($"Выбран: textBox с текстом - {(select as TextBlock).Text}");
            }
            else
                MessageBox.Show("Выбери что-то");
        }

        private void buttonAddList_Click(object sender, RoutedEventArgs e)
        {
            //listBoxSimple.Items.Clear();
            //string[] sourse = { "1111", "22222222", "третий" };
            //listBoxSimple.ItemsSource = sourse;
            listBoxSimple.Items.Add(new ListBoxItem() { Content = textBoxForList.Text });
            listBoxSimple.Items.Insert(1, new ListBoxItem() { Content = "33333" });
            //listBoxSimple.Items.Remove(textBoxForList.Text);
            //listBoxSimple.Items.RemoveAt(0);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            listBoxSimple.SelectedIndex = 2;
            textTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void combobox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (combobox1.SelectedIndex == -1) // ничего не выбрано
            if (combobox1.SelectedItem == null)
            {
                MessageBox.Show(combobox1.Text);
                return;
            }
            if (combobox1.SelectedItem is TextBlock)
            {
                MessageBox.Show($"Выбран элемент {combobox1.SelectedIndex} с текстом {textBlockCombo.Text}");
                return;
            }
            MessageBox.Show($"Выбран элемент {combobox1.SelectedIndex}.");
            //combobox1.SelectedIndex = 2;
        }
    }
}
