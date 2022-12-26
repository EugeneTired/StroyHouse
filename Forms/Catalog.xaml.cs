using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ВПФки.Forms;

namespace ВПФки
{
    internal static class AuthUser
    {
        public static User AuthPerson { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        public Catalog()
        {
            InitializeComponent();
            var authUser = AuthUser.AuthPerson;
            LV.Items.Clear();
            ContMenu.Visibility = Visibility.Collapsed;
            AdminMenu.Visibility = Visibility.Collapsed;
            if (SomeData.guestOrNot != 4)
            {
                UserMenu.Header = "Выйти";
                User.Content = authUser.Surname + " " + authUser.Name;
                Entry.Content = "Выйти";

                if (SomeData.guestOrNot == 1)
                {
                    AdminMenu.Visibility = Visibility.Visible;
                    ContMenu.Visibility = Visibility.Visible;
                }
            }

            else
            {
                UserMenu.Header = "Войти";
                User.Content = "Гость";
                Entry.Content = "Войти";

            }

            if (BusketOrder.Count == 0)
            {
                BaskAmountTXT.Visibility = Visibility.Collapsed;
            }
            else
            {
                BaskAmountTXT.Text = BusketOrder.Sum(s => s.Quantity).ToString();
            }

            SearchAndDestroy.Text = "";
            using (var db = new StroyMatEntities())
            {
                LV.ItemsSource = db.Product.Include(s => s.Suppliers).ToList();
                var list = new List<Category>();
                list.Add(new Category { Name = "Очистить" });
                list.AddRange(db.Category.ToList());
                CBcat.ItemsSource = list;
            }
        }

        //Фильтр
        public async Task CategFilt()
        {
            using (var db = await Task.Run(() => new StroyMatEntities()))
            {
                LV.ItemsSource = await Task.Run(() => db.Product.Include(s => s.Category).Include(s => s.Suppliers).Include(s => s.Units).ToList().Where(x => x.Name.ToLower().Contains(SearchAndDestroy.Text.ToLower())));
                var cat = CBcat.SelectedItem as Category;
                if (cat == null) return;
                if (CBcat.SelectedIndex > 0) LV.ItemsSource = LV.ItemsSource.Cast<Product>().ToList().Where(s => s.IDcateg == cat.IDcateg);
            }
            switch (PriceSort.SelectedIndex)
            {
                case 0:
                    LV.ItemsSource = LV.ItemsSource.Cast<Product>().OrderBy(s => s.Price);
                    break;
                case 1:
                    LV.ItemsSource = LV.ItemsSource.Cast<Product>().OrderByDescending(s => s.Price);
                    break;
            }
        }

        // Число у корзины
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            BaskAmountTXT.Visibility = Visibility.Visible;
            BaskAmountTXT.Clear();

            var prod = (sender as Button).DataContext as Product;
            AddBask(prod);

            BaskAmountTXT.Text = BusketOrder.Sum(s => s.Quantity).ToString();
        }

        public void AddBask(Product product)
        {
            if (BusketOrder.SingleOrDefault(s => s.IDprod == product.IDprod) == null)
            BusketOrder.Add(product);
            var bask = BusketOrder.SingleOrDefault(s => s.IDprod == product.IDprod);
            bask.Quantity++;
        }

        public static MegaGlist<Product> BusketOrder = new MegaGlist<Product>();

        private int BInt = 0;

        private void CBcat_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (LV != null) CategFilt();
        }

        private void SearchAndDestroy_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (LV != null) CategFilt();
        }

        //Label click
        private void Entry_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var entry = new MainWindow();
            entry.Show();
            this.Close();
        }

        //Basket
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var basket = new Basket();
            basket.Show();
            this.Close();
        }

        //REDACT
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var product = LV.SelectedItem as Product;
            var redact = new Redact(product);
            redact.Show();
            this.Close();
        }

       //ADD
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var add = new Add();
            add.Show();
            this.Close();
        }

        //DELETE
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            using (var db = new StroyMatEntities())
            {
                db.Product.Attach(LV.SelectedItem as Product);
                db.Product.Remove(LV.SelectedItem as Product);
                db.SaveChanges();
            }
            CategFilt();
            //var product = LV.SelectedItem as Product;

        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
            SomeData.guestOrNot = 0;
        }

        private void AdminMenu_Click(object sender, RoutedEventArgs e)
        {
            var adminMenu = new AdminMenu();
            adminMenu.Show();
            this.Close();
        }
    }
}