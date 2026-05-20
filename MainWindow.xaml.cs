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

namespace Podgotovka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new Pages.AuthPage());


            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        public void UpdateInfo()
        {
            if (Core.CurrentUser != null)
            {
                UserOn.Text =
                    $"{Core.CurrentUser.Surname} " +
                    $"{Core.CurrentUser.Name} " +
                    $"{Core.CurrentUser.Patronymic}";
            }

            else
            {
                UserOn.Text = "Гость";
            }
        }
    }
}
