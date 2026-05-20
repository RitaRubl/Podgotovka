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

namespace Podgotovka.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTxt.Text;
            string password = PasswordTxt.Password;

            if (string.IsNullOrWhiteSpace(login)) {
                MessageBox.Show("Введите корректный пароль");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите корректный логин");
                return;
            }

            var user = Core.Context.User.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user == null) { 
                MessageBox.Show("Пользователя с такими данными не найдено", "GG Bro" ,MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Core.CurrentUser = user;
            MessageBox.Show("Успешный вход!");
            NavigationService.Navigate(new ProductPage());

        }

        private void GuestEnter_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductPage());
        }
    }
}
