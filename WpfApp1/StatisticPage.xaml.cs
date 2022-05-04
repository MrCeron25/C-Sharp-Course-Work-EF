using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    public partial class StatisticPage : Page
    {
        public class Statistic
        {
            public int Year { get; set; }
            public decimal? Sum { get; set; }

            public override string ToString()
            {
                return $@"{Year} {Sum}";
            }
        }


        private List<Statistic> GetStatistics()
        {
            List<Statistic> statistics = (
                from order in Manager.Instance.Context.orders
                join order_detail in Manager.Instance.Context.order_details on order.order_id equals order_detail.order_id
                select new { order.order_date, order_detail.total_price } into x
                group x by new { x.order_date } into g
                select new Statistic
                {
                    Year = g.Key.order_date.Year,
                    Sum = g.Sum(i => i.total_price)
                }

            ).ToList();
            return statistics;
        }

        public StatisticPage()
        {
            InitializeComponent();
            Statistics.ItemsSource = GetStatistics();
        }

        private void SaveStatistics_Click(object sender, RoutedEventArgs e)
        {
            string PathDirectory = Directory.GetCurrentDirectory();
            string FileName = "Statistic.txt";
            using (FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (Statistic statistic in GetStatistics())
                    {
                        sw.WriteLine(statistic);
                    }
                }
            }
        }
    }
}
