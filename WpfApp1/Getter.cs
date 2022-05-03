using System.Collections.Generic;
using System.Linq;

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
    }
}
