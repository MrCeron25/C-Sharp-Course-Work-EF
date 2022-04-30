using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class UserOrders : Page
    {
        private void GetOrders()
        {
            List<orders> Data = (
                from order in Manager.Instance.Context.orders
                where order.buyer_id == User.system_user_id
                select order
            ).ToList();
            Orders.ItemsSource = Data;
        }

        private system User { get; set; }
        public UserOrders(system User)
        {
            InitializeComponent();
            this.User = User;
            GetOrders();
        }

        public class ProductsList
        {
            public long product_id { get; set; }
            public string product_name { get; set; }
            public long number_of_orders { get; set; }
            public decimal? unit_price { get; set; }
            public long? quantity { get; set; }
            public decimal? total_price { get; set; }
        }

        private void Orders_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Orders.SelectedItem != null)
            {
                long OrderId = (Orders.SelectedItem as orders).order_id;
                var Data = (
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
            }
        }

        private void Products_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Products.SelectedItem != null)
            {
                var product = Products.SelectedItem as ProductsList;
                long ProductId = product.product_id;
                List<properties> Data = (
                    from properties in Manager.Instance.Context.properties
                    where properties.product_id == ProductId
                    select properties
                ).ToList();
                Characteristics.ItemsSource = Data;
            }
        }
    }
}
