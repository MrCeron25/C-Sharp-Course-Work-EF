using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    public class ProductsList
    {
        public long product_id { get; set; }
        public string product_name { get; set; }
        public long number_of_orders { get; set; }
        public decimal? unit_price { get; set; }
        public long? quantity { get; set; }
        public decimal? total_price { get; set; }
    }

    public partial class UserOrders : Page
    {
        private system User { get; set; }
        public UserOrders(system User)
        {
            InitializeComponent();
            this.User = User;
            Orders.ItemsSource = Getter.GetOrders(User.system_user_id);
        }

        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Orders.SelectedItem != null)
            {
                long OrderId = (Orders.SelectedItem as orders).order_id;
                List<ProductsList> Data = (
                    from o in Manager.Instance.Context.order_details
                    join p in Manager.Instance.Context.products on o.product_id equals p.product_id
                    where o.order_id == OrderId
                    select new ProductsList
                    {
                        product_id = p.product_id,
                        product_name = p.product_name,
                        number_of_orders = p.number_of_orders,
                        unit_price = o.unit_price,
                        quantity = o.quantity,
                        total_price = o.total_price
                    }
                ).ToList();
                Products.ItemsSource = Data;
                Characteristics.ItemsSource = null;
            }
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Products.SelectedItem != null)
            {
                ProductsList product = Products.SelectedItem as ProductsList;
                long ProductId = product.product_id;
                List<properties> properties = (
                    from property in Manager.Instance.Context.properties
                    where property.product_id == ProductId
                    select property
                ).ToList();
                Characteristics.ItemsSource = properties;
            }
        }
    }
}
