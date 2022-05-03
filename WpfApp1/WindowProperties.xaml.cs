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
    public partial class WindowProperties : Window
    {
        public WindowProperties()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CheckButton()
        {
            if (
                !string.IsNullOrEmpty(Property.Text) &&
                !string.IsNullOrEmpty(Value.Text)
            )
            {
                Button.IsEnabled = true;
            }
            else
            {
                Button.IsEnabled = false;
            }
        }

        private void Property_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckButton();
        }

        private void Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckButton();
        }
    }
}
