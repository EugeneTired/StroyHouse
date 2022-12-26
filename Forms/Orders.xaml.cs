using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ВПФки.Extra;

namespace ВПФки.Forms
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        public Orders()
        {
            InitializeComponent();
            Icat.Items.Clear();
            var authUser = AuthUser.AuthPerson;
            AdminMenu.Visibility = Visibility.Collapsed;
            if (SomeData.guestOrNot != 4)
            {
                User.Content = authUser.Surname + " " + authUser.Name;
                Entry.Content = "Выйти";

                if (SomeData.guestOrNot == 1)
                {
                    AdminMenu.Visibility = Visibility.Visible;
                }
            }

            using (var db = new StroyMatEntities())
            {
                var ordsr = db.Order.Where(s => s.IDuser == authUser.IDus);
                var ords = db.Order.Include(s => s.User).Where(s => s.IDuser == authUser.IDus).Select(s => new OrderClass
                {
                    Order = s
                }).ToList();
                Icat.ItemsSource = ords;
            }
            if (SomeData.guestOrNot != 4)
            {
                User.Content = authUser.Surname + " " + authUser.Name;
                Entry.Content = "Выйти";

                if (SomeData.guestOrNot == 1)
                {
                    AdminMenu.Visibility = Visibility.Visible;
                }
            }

            else
            {
                User.Content = "Гость";
                Entry.Content = "Войти";

            }
        }

        private void Entry_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var cat = new Catalog();
            cat.Show();
            this.Close();
        }

        private void Mega_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AdminMenu_Click(object sender, RoutedEventArgs e)
        {
            var adminMenu = new AdminMenu();
            adminMenu.Show();
            this.Close();
        }
    }
}
