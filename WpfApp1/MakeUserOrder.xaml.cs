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
using static WpfApp1.UserOrders;

namespace WpfApp1
{
    public partial class MakeUserOrder : Page
    {
        private system User { get; set; }
        private decimal? TotalPrice { get; set; }
        private void UpdateProducts()
        {
            var data = (
                from product in Manager.Instance.Context.products
                select new ProductsList
                {
                    product_id = product.product_id,
                    product_name = product.product_name,
                    unit_price = product.unit_price
                }
            ).ToList();
            if (data.Count > 0)
            {
                Products.ItemsSource = data;
            }
        }

        public MakeUserOrder(system User)
        {
            InitializeComponent();
            this.User = User;
            UpdateProducts();
            TotalPrice = 0;
        }

        private void UpdateTotalPrice(decimal? price)
        {
            TotalPrice += price;
            TotalPriceText.Text = TotalPrice.ToString();
            CheckBuy();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Products.SelectedItem != null)
            {
                ProductsList Product = Products.SelectedItem as ProductsList;

                bool InItems = false;

                foreach (ProductsList ProductInItems in OrderDetails.Items)
                {
                    if (Product.product_name == ProductInItems.product_name)
                    {
                        OrderDetails.Items.Remove(ProductInItems);
                        TotalPrice -= ProductInItems.total_price;

                        Product.quantity += long.Parse(ProductCount.Text);
                        Product.total_price = Product.quantity * Product.unit_price;
                        OrderDetails.Items.Add(Product);
                        InItems = true;
                        break;
                    }
                }

                if (!InItems)
                {
                    Product.quantity = long.Parse(ProductCount.Text);
                    Product.total_price = Product.quantity * Product.unit_price;
                    OrderDetails.Items.Add(Product);
                }

                UpdateTotalPrice(Product.total_price);
                ProductCount.Text = "";
                Products.SelectedItem = null;
            }
        }

        private void CheckBuy()
        {
            if (
                OrderDetails.Items.Count > 0 &&
                TotalPrice > 0
            )
            {
                Buy.IsEnabled = true;
            }
            else
            {
                Buy.IsEnabled = false;
            }
        }

        private void CheckAddButton()
        {
            long number;
            bool IsNumber = long.TryParse(ProductCount.Text, out number);
            if (
                Products.SelectedItem != null &&
                IsNumber &&
                number > 0
            )
            {
                Add.IsEnabled = true;
            }
            else
            {
                Add.IsEnabled = false;
            }
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckAddButton();
        }

        private void ProductCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckAddButton();
        }

        private void OrderDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (
                !DeleteOrderDetails.IsEnabled &&
                OrderDetails.SelectedItem != null
            )
            {
                DeleteOrderDetails.IsEnabled = true;
            }
        }

        private void DeleteOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDetails.SelectedItem != null)
            {
                ProductsList Product = OrderDetails.SelectedItem as ProductsList;
                UpdateTotalPrice(-Product.total_price);
                OrderDetails.Items.Remove(Product);
                DeleteOrderDetails.IsEnabled = false;
                OrderDetails.SelectedItem = null;
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            orders NewOrder = new orders
            {
                buyer_id = User.system_user_id,
                order_date = DateTime.Now
            };
            Manager.Instance.Context.orders.Add(NewOrder);
            Manager.Instance.Context.SaveChanges();
            foreach (ProductsList product in OrderDetails.Items)
            {
                order_details NewOrderDetails = new order_details
                {
                    order_id = NewOrder.order_id,
                    product_id = product.product_id,
                    unit_price = product.unit_price,
                    quantity = product.quantity,
                    total_price = product.total_price
                };
                Manager.Instance.Context.order_details.Add(NewOrderDetails);
            }
            Manager.Instance.Context.SaveChanges();
            OrderDetails.Items.Clear();
            Buy.IsEnabled = false;
            TotalPriceText.Text = "0,0000";
            MessageBox.Show("Покупка прошла успешно.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
