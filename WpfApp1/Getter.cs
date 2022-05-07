using System.Collections.Generic;
using System.Linq;
using static WpfApp1.UserOrders;

namespace WpfApp1
{
    static class Getter
    {
        public static List<properties> GetProperties(long product_id)
        {
            List<properties> properties = (
                from property in Manager.Instance.Context.properties
                where property.product_id == product_id
                select property
            ).ToList();
            return properties;
        }

        public static List<products> GetProducts()
        {
            List<products> products = (
                from product in Manager.Instance.Context.products
                select product
            ).ToList();
            return products;
        }

        public static List<ProductsList> GetProductsList()
        {
            List<ProductsList> productsList = (
                from product in Manager.Instance.Context.products
                select new ProductsList
                {
                    product_id = product.product_id,
                    product_name = product.product_name,
                    unit_price = product.unit_price,
                    number_of_orders = product.number_of_orders
                }
            ).ToList();
            return productsList;
        }

        public static List<orders> GetOrders(long SystemUserId)
        {
            List<orders> orders = (
                from order in Manager.Instance.Context.orders
                where order.buyer_id == SystemUserId
                select order
            ).ToList();
            return orders;
        }
    }
}
