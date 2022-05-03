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

namespace WpfApp1
{
    public partial class ProductEditingPage : Page
    {

        public ProductEditingPage()
        {
            InitializeComponent();
            DataGrid.ItemsSource = Getter.GetProducts();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            ProductsEditingWindow window = new ProductsEditingWindow();
            window.ShowDialog();
        }
    }
}
