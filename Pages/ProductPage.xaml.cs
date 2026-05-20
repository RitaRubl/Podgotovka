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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            ProductList.ItemsSource = Core.Context.Product.ToList();
            var AllProviders = Core.Context.Provider.ToList();
            AllProviders.Insert(0, new Provider { Name = "Все поставщики" });
            FilterCmb.ItemsSource = AllProviders;

            if (Core.CurrentUser == null)
            {
                ButtonsFullPanel.Visibility = Visibility.Collapsed;
                return;
            }

            switch (Core.CurrentUser.RoleId) {
                case 1:
                    EditButtonsPanel.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    OrdersButton.Visibility = Visibility.Collapsed; 
                    break;
            }
        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SortCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
