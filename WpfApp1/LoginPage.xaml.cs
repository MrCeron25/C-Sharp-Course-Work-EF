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
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (
                !string.IsNullOrEmpty(Login.Text) &&
                !string.IsNullOrEmpty(Password.Password)
            )
            {
                List<system> LoginLists = (
                    from user in Manager.Instance.Context.system
                    where (user.login == Login.Text) && (user.password == Password.Password)
                    select user
                ).ToList();
                if (LoginLists.Count == 0)
                {
                    MessageBox.Show("Неверный логин или пароль.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    system user = LoginLists[0];
                    Login.Text = "";
                    Password.Password = "";
                    if (user.is_admin)
                    {
                        Manager.Instance.MainFrame.Content = new ProductEditingPage();
                        Manager.Instance.MenuFrame.Content = new AdminMenuPage(user);
                    }
                    else
                    {
                        Manager.Instance.MainFrame.Content = new UserPage();
                        Manager.Instance.MenuFrame.Content = new UserMenuPage(user);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
