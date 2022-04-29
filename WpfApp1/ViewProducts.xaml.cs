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
    public partial class ViewProducts : Page
    {
        public class ProductFields
        {
            public string product_name { get; set; }
            public decimal? unit_price { get; set; }
            public long number_of_orders { get; set; }
        }

        private void GetData()
        {
            var data = (
                from product in Manager.Instance.Context.products
                select new ProductFields
                {
                    product_name = product.product_name,
                    unit_price = product.unit_price,
                    number_of_orders = product.number_of_orders
                }
            ).ToList();
            DataGrid.ItemsSource = data;
        }

        public ViewProducts()
        {
            InitializeComponent();
            GetData();
        }
    }
}
