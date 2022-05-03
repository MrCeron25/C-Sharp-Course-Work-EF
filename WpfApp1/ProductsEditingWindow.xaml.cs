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
    public partial class ProductsEditingWindow : Window
    {
        private products SelectedItem { get; set; }
        public ProductsEditingWindow(List<products> products)
        {
            InitializeComponent();
            Products.ItemsSource = products;
        }

        private List<properties> GetProperties(long product_id)
        {
            List<properties> properties = (
                from property in Manager.Instance.Context.properties
                where property.product_id == product_id
                select property
            ).ToList();
            return properties;
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Products.SelectedItem != null)
            {
                SelectedItem = Products.SelectedItem as products;
                ProductName.IsEnabled = true;
                UnitPrice.IsEnabled = true;
                AddProperty.IsEnabled = true;
                ProductName.Text = SelectedItem.product_name;
                UnitPrice.Text = SelectedItem.unit_price.ToString();

                Properties.ItemsSource = null;
                Properties.ItemsSource = GetProperties(SelectedItem.product_id);
            }
        }

        private void CheckSaveProductButton()
        {
            decimal number = -1;
            bool IsNumber = decimal.TryParse(UnitPrice.Text, out number);
            if (
                Products.SelectedItem != null &&
                IsNumber &&
                number > 0 &&
                !string.IsNullOrEmpty(ProductName.Text)
            )
            {
                SaveProduct.IsEnabled = true;
            }
            else
            {
                SaveProduct.IsEnabled = false;
            }
        }

        private void ProductName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckSaveProductButton();
        }

        private void UnitPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckSaveProductButton();
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (Products.SelectedItem != null)
            {
                SelectedItem = Products.SelectedItem as products;
                products product = Manager.Instance.Context.products.Where(c => c.product_id == SelectedItem.product_id).FirstOrDefault();
                product.product_name = ProductName.Text;
                product.unit_price = decimal.Parse(UnitPrice.Text);
                Manager.Instance.Context.SaveChanges();
                MessageBox.Show("Изменения сохранены.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                Products.SelectedItem = null;
                ProductName.Text = "";
                UnitPrice.Text = "";
            }
        }

        private void Properties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddProperty.IsEnabled = true;
            DeleteProperty.IsEnabled = true;
        }

        private void AddProperty_Click(object sender, RoutedEventArgs e)
        {
            WindowProperties window = new WindowProperties();
            window.Button.Content = "Добавить";
            if ((bool)window.ShowDialog())
            {
                properties property = new properties
                {
                    product_id = SelectedItem.product_id,
                    attribute = window.Property.Text,
                    value = window.Value.Text
                };
                Manager.Instance.Context.properties.Add(property);
                Manager.Instance.Context.SaveChanges();
                Properties.ItemsSource = GetProperties(SelectedItem.product_id);
            }
        }
    }
}
