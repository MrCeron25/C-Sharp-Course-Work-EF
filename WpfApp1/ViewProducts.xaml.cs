using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using static WpfApp1.UserOrders;

namespace WpfApp1
{
    public partial class ViewProducts : Page
    {
        public ViewProducts()
        {
            InitializeComponent();
            Products.ItemsSource = Getter.GetProductsList();
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Products.SelectedItem != null)
            {
                ProductsList product = Products.SelectedItem as ProductsList;
                Characteristics.ItemsSource = Getter.GetProperties(product.product_id);
            }
        }
    }
}
