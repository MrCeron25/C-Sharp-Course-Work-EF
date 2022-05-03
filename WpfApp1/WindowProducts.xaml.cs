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
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class WindowProducts : Window
    {
        public WindowProducts()
        {
            InitializeComponent();
        }

        private void TextBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal number = -1;
            bool IsNumber = decimal.TryParse(UnitPrice.Text, out number);
            if (
                IsNumber &&
                number > 0 &&
                !string.IsNullOrEmpty(ProductName.Text)
            )
            {
                Button.IsEnabled = true;
            }
            else
            {
                Button.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
