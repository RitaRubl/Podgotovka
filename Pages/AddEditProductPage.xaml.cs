using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Podgotovka.Pages
{
    public partial class AddEditProductPage : Page
    {
        private Product CurrentProduct;
        private bool _isNew;

        public List<Producer> Producers { get; set; }
        public List<Provider> Providers { get; set; }
        public List<Category> Categories { get; set; }

        public AddEditProductPage()
        {
            InitializeComponent();
            _isNew = true;
            CurrentProduct = new Product();
            LoadData();
            DataContext = this;
        }

        public AddEditProductPage(Product product)
        {
            InitializeComponent();
            _isNew = false;
            CurrentProduct = product ?? new Product();
            LoadData();
            DataContext = this;
            FillFields();
        }

        private void LoadData()
        {
            try
            {
                Producers = Core.Context.Producer.ToList();
                Providers = Core.Context.Provider.ToList();
                Categories = Core.Context.Category.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки справочников:\n" + ex.Message, "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillFields()
        {
            if (CurrentProduct == null) return;

            NameBox.Text = CurrentProduct.Name ?? "";
            ArticleBox.Text = CurrentProduct.Article ?? "";
            DescriptionBox.Text = CurrentProduct.Description ?? "";
            PriceBox.Text = CurrentProduct.Price.ToString("0.##");
            DiscountBox.Text = CurrentProduct.Discount.ToString("0.##");
            AmountBox.Text = CurrentProduct.AmountInStock.ToString();
            PhotoBox.Text = CurrentProduct.Photo ?? "";
            UnitBox.Text = CurrentProduct.UnitId.ToString();

            ProducerBox.SelectedValue = CurrentProduct.ProducerId;
            ProviderBox.SelectedValue = CurrentProduct.ProviderId;
            CategoryBox.SelectedValue = CurrentProduct.CategoryId;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentProduct.Name = NameBox.Text.Trim();
                CurrentProduct.Article = ArticleBox.Text.Trim();
                CurrentProduct.Description = DescriptionBox.Text.Trim();
                CurrentProduct.Photo = string.IsNullOrWhiteSpace(PhotoBox.Text)
                    ? "/Res/picture.png"
                    : PhotoBox.Text.Trim();

                CurrentProduct.UnitId = 1;

                decimal.TryParse(PriceBox.Text, out decimal price);
                CurrentProduct.Price = price;

                decimal.TryParse(DiscountBox.Text, out decimal discount);
                CurrentProduct.Discount = discount;

                int.TryParse(AmountBox.Text, out int amount);
                CurrentProduct.AmountInStock = amount;

                if (ProducerBox.SelectedValue is int producerId)
                    CurrentProduct.ProducerId = producerId;

                if (ProviderBox.SelectedValue is int providerId)
                    CurrentProduct.ProviderId = providerId;

                if (CategoryBox.SelectedValue is int categoryId)
                    CurrentProduct.CategoryId = categoryId;

                if (string.IsNullOrWhiteSpace(CurrentProduct.Name) ||
                    string.IsNullOrWhiteSpace(CurrentProduct.Article) ||
                    string.IsNullOrWhiteSpace(CurrentProduct.Description))
                {
                    MessageBox.Show("Заполните обязательные поля: Название, Артикул и Описание!",
                                  "Валидация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (CurrentProduct.Price <= 0)
                {
                    MessageBox.Show("Цена должна быть больше 0!", "Валидация");
                    return;
                }

                if (CurrentProduct.ProducerId == 0 ||
                    CurrentProduct.ProviderId == 0 ||
                    CurrentProduct.CategoryId == 0)
                {
                    MessageBox.Show("Выберите Производителя, Поставщика и Категорию!", "Валидация");
                    return;
                }

                if (_isNew)
                    Core.Context.Product.Add(CurrentProduct);

                Core.Context.SaveChanges();

                MessageBox.Show("Товар успешно сохранён!", "Успех",
                               MessageBoxButton.OK, MessageBoxImage.Information);

                NavigationService.Navigate(new ProductPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении:\n" +
                              (ex.InnerException?.InnerException?.Message ?? ex.Message),
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}