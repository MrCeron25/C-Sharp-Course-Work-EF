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
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private static bool CheckPassword(string password)
        {
            bool res = false;
            char[] specialCharactersArray = "%!@#$%^&*()?/>.<,:;'\\|}]{[_~`+=-\"".ToCharArray();
            //min 6 chars OR At least 1 upper case letter OR At least 1 lower case letter
            if (password.Length < 8 || !password.Any(char.IsUpper) || !password.Any(char.IsLower))
            {
                res = false;
            }
            else if (password.Any(specialCharactersArray.Contains)) // At least 1 special char
            {
                res = true;
            }
            return res;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (
                !string.IsNullOrEmpty(Name.Text) &&
                !string.IsNullOrEmpty(Surname.Text) &&
                !string.IsNullOrEmpty(Login.Text) &&
                !string.IsNullOrEmpty(Password.Password)
            )
            {
                if (CheckPassword(Password.Password))
                {
                    List<system> logiins = (
                        from user in Manager.Instance.Context.system
                        where user.login == Login.Text
                        select user
                    ).ToList();
                    if (logiins.Count > 0)
                    {
                        MessageBox.Show("Логин занят.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        //using (var db = new course_work_EFEntities())
                        //{
                        //    var qwe = db.buyer.Create();
                        //    //buyer NewBuyer = new buyer
                        //    //{
                        //    //name = Name.Text,
                        //    //    surname = Surname.Text,
                        //    //    sex = Sex.Text.Substring(0, 1)
                        //    //};
                        //    qwe.buyer_id = 1;
                        //    qwe.name = Name.Text;
                        //    qwe.surname = Surname.Text;
                        //    qwe.sex = Sex.Text.Substring(0, 1);
                        //    db.buyer.Add(qwe);
                        //    db.SaveChanges();
                        //}

                        buyer NewBuyer = new buyer
                        {
                            name = Name.Text,
                            surname = Surname.Text,
                            sex = Sex.Text.Substring(0, 1)
                        };
                        Manager.Instance.Context.buyer.Add(NewBuyer);



                        List<buyer> buyer = (
                            from buyers in Manager.Instance.Context.buyer.OrderByDescending(s => s.buyer_id).Take(1)
                            select buyers
                        ).ToList();
                        long LastBuyerId = buyer[0].buyer_id + 1;
                        system NewSystem = new system
                        {
                            login = Login.Text,
                            password = Password.Password,
                            is_admin = false,
                            buyer_id = LastBuyerId
                        };
                        Manager.Instance.Context.system.Add(NewSystem);

                        

                        Manager.Instance.Context.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Слишком лёгкий пароль.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
