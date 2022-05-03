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
    public partial class AdminMenuPage : Page
    {
        private system User { get; set; }
        public AdminMenuPage(system User)
        {
            InitializeComponent();
            this.User = User;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new LoginPage());
            Manager.Instance.MenuFrame.Navigate(new MainMenuPage());
        }

        private void ProductEditing_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Navigate(new ProductEditingPage());
        }
    }
}
