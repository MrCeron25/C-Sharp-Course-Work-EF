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
        private List<products> GetProducts()
        {
            List<products> products = (
                from product in Manager.Instance.Context.products
                select product
            ).ToList();
            return products;
        }

        public ProductEditingPage()
        {
            InitializeComponent();
            DataGrid.ItemsSource = GetProducts();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            ProductsEditingWindow window = new ProductsEditingWindow(GetProducts());
            if ((bool)window.ShowDialog())
            {

            }
        }
    }
}
