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
    public partial class UserMenuPage : Page
    {
        private system User { get; set; }
        public UserMenuPage(system User)
        {
            InitializeComponent();
            this.User = User;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Content = new LoginPage();
            Manager.Instance.MenuFrame.Content = new MainMenuPage();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Content = new ViewProducts();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Content = new UserOrders(User);
        }

        private void MakeAnOrder_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Content = new MakeUserOrder(User);
        }
    }
}
