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
            FilterCmb.SelectedIndex = 0;

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

        private void Filter()
        {
            var filtredproducts = Core.Context.Product.ToList();

            if (!string.IsNullOrWhiteSpace(SearchTxt.Text))
            {
                filtredproducts = filtredproducts
                    .Where(p => p.Name.ToLower().Contains(SearchTxt.Text.ToLower()) ||
                                p.Description.ToLower().Contains(SearchTxt.Text.ToLower()) ||
                                p.Producer.Name.ToLower().Contains(SearchTxt.Text.ToLower()) ||
                                p.Provider.Name.ToLower().Contains(SearchTxt.Text.ToLower())
                    ).ToList();
            }

            if (SortCmb.SelectedIndex == 1) {
                filtredproducts = filtredproducts.OrderBy(p => p.Discount).ToList();
            }
            else if (SortCmb.SelectedIndex == 2)
            {
                filtredproducts = filtredproducts.OrderByDescending(p => p.Discount).ToList();
            }


            if (FilterCmb != null && FilterCmb.SelectedIndex != 0)
            {
                filtredproducts = filtredproducts.Where(p => p.Provider == FilterCmb.SelectedItem as Provider).ToList();
            }

            if (ProductList != null)
            {
                ProductList.ItemsSource = filtredproducts;
            }

        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void SortCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void FilterCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void ProductList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductList.SelectedItem is Product selectedProduct)
            {
                NavigationService.Navigate(new AddEditProductPage());
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditProductPage());
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductList.SelectedItem is Product selectedproduct)
            {
                var messageresult = MessageBox.Show("Вы точно хотите удалить этот товар?", "Удалить", MessageBoxButton.YesNo);

                if (messageresult == MessageBoxResult.Yes) { 
                    if (Core.Context.ProductInOrder.Any(p => p.ProductId == selectedproduct.Id)){
                        MessageBox.Show("Нельзя удалить этот товар", "Удалить", MessageBoxButton.OK);
                        return;
                    }
                }
                Core.Context.Product.Remove(selectedproduct);
                Core.Context.SaveChanges();
                Filter();
            }
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
