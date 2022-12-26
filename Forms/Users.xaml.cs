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

namespace ВПФки.Forms
{

    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();
            LV.Items.Clear();
            using (var db = new StroyMatEntities())
            {
                var list = db.User.Include(s => s.Roles).ToList();
                LV.ItemsSource = list;
            }

            if (SomeData.guestOrNot != 4)
            {
                var authUser = AuthUser.AuthPerson;
                UserMenu.Header = "Выйти";
                User.Content = authUser.Surname + " " + authUser.Name;
                Entry.Content = "Выйти";

                if (SomeData.guestOrNot == 1)
                {
                    ContMenu.Visibility = Visibility.Visible;
                }
            }

            else
            {
                UserMenu.Header = "Войти";
                User.Content = "Гость";
                Entry.Content = "Войти";

            }
        }

        private void Entry_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var Cat = new Catalog();
            Cat.Show();
            this.Close();
        }
        private void Change_Click(object sender, RoutedEventArgs m)
        {
            var user = LV.SelectedItem as User;
            var redact = new RedactUser(user);
            redact.Show();
            this.Close();
        }
        private void Add_Click(object sender, RoutedEventArgs m)
        {
            var add = new AddUser();
            add.Show();
            this.Close();
        }
        private void Delete_Click(object sender, RoutedEventArgs m)
        {
            using (var db = new StroyMatEntities())
            {
                db.User.Attach(LV.SelectedItem as User);
                db.User.Remove(LV.SelectedItem as User);
                db.SaveChanges();
                LV.Items.Refresh();
            }
        }
        private void LogOff_Click(object sender, RoutedEventArgs m)
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
