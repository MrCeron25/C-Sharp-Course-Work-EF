using System.Windows;
using System.Windows.Controls;

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
            Manager.Instance.MainFrame.Content = new LoginPage();
            Manager.Instance.MenuFrame.Content = new MainMenuPage();
        }

        private void ProductEditing_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Content = new ProductEditingPage();
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainFrame.Content = new StatisticPage();
        }
    }
}
