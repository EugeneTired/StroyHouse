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
using System.Data.Entity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ВПФки.Forms
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Basket : Window
    {
        public Basket()
        {
            InitializeComponent();
            UpdateOrder();
            var authUser = AuthUser.AuthPerson;
            cat.Items.Clear();
            cat.ItemsSource = Catalog.BusketOrder;
            if (SomeData.guestOrNot != 4)
            {
                User.Content = authUser.Surname + " " + authUser.Name;
                Entry.Content = "Выйти";
            }
            else
            {
                User.Content = "Гость";
                Entry.Content = "Войти";

            }
            using (var db = new StroyMatEntities())
            {
                noName.ItemsSource = db.Shipment.ToList();
            }
        }
        private void Entry_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void AddProd_Click(object sender, RoutedEventArgs e)
        {
            var prod = (sender as Button).DataContext as Product;
            Catalog.BusketOrder.Single(s => s.IDprod == prod.IDprod).Quantity++;
            cat.Items.Refresh();
            UpdateOrder();
        }

        private void NoAddProd_Click(object sender, RoutedEventArgs e)
        {
            var prod = (sender as Button).DataContext as Product;
            var qu = Catalog.BusketOrder.Single(s => s.IDprod == prod.IDprod);
            qu.Quantity--;
            if (qu.Quantity < 1)
                Catalog.BusketOrder.Remove(qu);
            cat.Items.Refresh();
            UpdateOrder();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {   
            try
            {
                if (AuthUser.AuthPerson == null)
                {
                    if(MessageBox.Show("Для подтверждения заказа нужно сначала войти в аккаунт, Да для перехода на форму входа", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning)==MessageBoxResult.Yes)
                    {
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }
                }
                else
                {
                    using (var db = new StroyMatEntities())
                    {
                        Order order = new Order
                        {
                            IDuser = AuthUser.AuthPerson.IDus,
                            IDship = (noName.SelectedItem as Shipment).IDship,
                            SaleDate = DateTime.Today,
                            Status = false,
                            ShipDate = DatePick.SelectedDate.Value
                        };
                        db.Order.Add(order);

                        foreach(var item in Catalog.BusketOrder)
                        {
                            db.ProOrd.Add(new ProOrd
                            {
                                IDord = order.IDord,
                                IDprod = item.IDprod,
                                AmountOrd = item.Quantity
                            });
                        }

                        db.SaveChanges();
                        MessageBox.Show("Заказ выполнен!","Успешно",MessageBoxButton.OK, MessageBoxImage.Information);// ; ; ; ; ; ; ; ; ; ; ; ; ;
                        Catalog.BusketOrder.Clear();
                        Catalog cot = new Catalog();
                        cot.Show();
                        this.Close();
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Выберите дату!");
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Catalog cat = new Catalog();
            cat.Show();
            this.Close();
        }
        private void UpdateOrder()
        {
            cat.Items.Refresh();
            if (Catalog.BusketOrder.Any()) OrderPrice.Text = $"Итоговая цена : {Catalog.BusketOrder.Sum(s => s.Price * s.Quantity):0.00} руб.";
            else OrderPrice.Text = string.Empty;

            Submit.IsEnabled = Catalog.BusketOrder.Any();
        }

        private void AdminMenu_Click(object sender, RoutedEventArgs e)
        {
            var admin = new AdminMenu();
            admin.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var odr = new Orders();
            odr.Show();
            this.Close();
        }
    }

 

}
