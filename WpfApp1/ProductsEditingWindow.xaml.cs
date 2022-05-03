using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class ProductsEditingWindow : Window
    {
        private products SelectedProduct { get; set; }
        private properties SelectedProperty { get; set; }
        public ProductsEditingWindow()
        {
            InitializeComponent();
            Products.ItemsSource = Getter.GetProducts();
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Products.SelectedItem != null)
            {
                SelectedProduct = Products.SelectedItem as products;
                ChangeProduct.IsEnabled = true;
                DeleteProduct.IsEnabled = true;
                AddProperty.IsEnabled = true;

                ChangeProperty.IsEnabled = false;
                DeleteProperty.IsEnabled = false;

                Properties.ItemsSource = null;
                Properties.ItemsSource = Getter.GetProperties(SelectedProduct.product_id);
            }
        }

        private void Properties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedProperty = Properties.SelectedItem as properties;
            AddProperty.IsEnabled = true;
            DeleteProperty.IsEnabled = true;
            ChangeProperty.IsEnabled = true;
        }

        private void AddProperty_Click(object sender, RoutedEventArgs e)
        {
            WindowProperties window = new WindowProperties();
            window.Button.Content = "Добавить";
            if ((bool)window.ShowDialog())
            {
                properties property = new properties
                {
                    product_id = SelectedProduct.product_id,
                    attribute = window.Property.Text,
                    value = window.Value.Text
                };
                Manager.Instance.Context.properties.Add(property);
                Manager.Instance.Context.SaveChanges();
                Properties.ItemsSource = Getter.GetProperties(SelectedProduct.product_id);
            }
        }

        private void ChangeProperty_Click(object sender, RoutedEventArgs e)
        {
            WindowProperties window = new WindowProperties();
            window.Button.Content = "Изменить";
            window.Property.Text = SelectedProperty.attribute;
            window.Value.Text = SelectedProperty.value;
            if ((bool)window.ShowDialog())
            {
                properties property = (
                    from property_ in Manager.Instance.Context.properties
                    where property_.property_id == SelectedProperty.property_id
                    select property_
                ).ToList()[0];
                property.attribute = window.Property.Text;
                property.value = window.Value.Text;
                Manager.Instance.Context.SaveChanges();
                Properties.ItemsSource = Getter.GetProperties(SelectedProduct.product_id);
            }
        }

        private void DeleteProperty_Click(object sender, RoutedEventArgs e)
        {
            properties property = (
                from property_ in Manager.Instance.Context.properties
                where property_.property_id == SelectedProperty.property_id
                select property_
            ).ToList()[0];
            Manager.Instance.Context.properties.Remove(property);
            Manager.Instance.Context.SaveChanges();
            Properties.ItemsSource = Getter.GetProperties(SelectedProduct.product_id);

        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            WindowProducts window = new WindowProducts();
            window.Button.Content = "Добавить";
            if ((bool)window.ShowDialog())
            {
                products product = new products
                {
                    product_name = window.ProductName.Text,
                    unit_price = decimal.Parse(window.UnitPrice.Text)
                };
                Manager.Instance.Context.products.Add(product);
                Manager.Instance.Context.SaveChanges();
                Products.ItemsSource = Getter.GetProducts();
                Products.SelectedItem = null;
                ChangeProduct.IsEnabled = false;
                DeleteProduct.IsEnabled = false;
                MessageBox.Show("Продукт добавлен.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ChangeProduct_Click(object sender, RoutedEventArgs e)
        {
            WindowProducts window = new WindowProducts();
            window.Button.Content = "Изменить";
            window.ProductName.Text = SelectedProduct.product_name;
            window.UnitPrice.Text = SelectedProduct.unit_price.ToString();
            if ((bool)window.ShowDialog())
            {
                products product = (
                    from product_ in Manager.Instance.Context.products
                    where product_.product_id == SelectedProduct.product_id
                    select product_
                ).ToList()[0];
                product.product_name = window.ProductName.Text;
                product.unit_price = decimal.Parse(window.UnitPrice.Text);
                Manager.Instance.Context.SaveChanges();
                Products.ItemsSource = Getter.GetProducts();
                Products.SelectedItem = null;
                ChangeProduct.IsEnabled = false;
                DeleteProduct.IsEnabled = false;
                MessageBox.Show("Продукт изменён.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            List<properties> properties = (
                from property in Manager.Instance.Context.properties
                where property.product_id == SelectedProduct.product_id
                select property
            ).ToList();
            foreach (properties property in properties)
            {
                Manager.Instance.Context.properties.Remove(property);
            }

            List<order_details> order_details = (
                from order_details_ in Manager.Instance.Context.order_details
                where order_details_.product_id == SelectedProduct.product_id
                select order_details_
            ).ToList();
            foreach (order_details order_detail in order_details)
            {
                Manager.Instance.Context.order_details.Remove(order_detail);
            }

            products product = (
                from product_ in Manager.Instance.Context.products
                where product_.product_id == SelectedProduct.product_id
                select product_
            ).ToList()[0];
            Manager.Instance.Context.products.Remove(product);
            Manager.Instance.Context.SaveChanges();
            Products.ItemsSource = Getter.GetProducts();
            Properties.ItemsSource = null;
            Products.SelectedItem = null;
            ChangeProduct.IsEnabled = false;
            DeleteProduct.IsEnabled = false;
            MessageBox.Show("Продукт удалён.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
