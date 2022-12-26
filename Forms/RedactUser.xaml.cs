using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
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
    /// Логика взаимодействия для RedactUser.xaml
    /// </summary>
    public partial class RedactUser : Window
    {
        public RedactUser(User user)
        {
            InitializeComponent();
            NameBox.Text = user.Name;
            PatronBox.Text = user.Patron.ToString();
            SurnameBox.Text = user.Surname;
            LoginBox.Text = user.Login.ToString();
            PasswordBox.Text = user.Password;

            using (var db = new StroyMatEntities())
            {
                RoleBox.ItemsSource = db.Roles.ToList();
                RoleBox.SelectedItem = (RoleBox.ItemsSource as List<Roles>).Single(s => s.IDrole == user.Role);
            }
            User = user;
        }
        private string FileNamePath { get; set; }

        private User User { get; set; }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "" || PatronBox.Text == "" || SurnameBox.Text == "" || RoleBox.SelectedItem.ToString() == "" || LoginBox.Text == "" || PasswordBox.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                using (var db = new StroyMatEntities())
                {
                    byte[] image = null;
                    if (!string.IsNullOrWhiteSpace(FileNamePath))
                        image = File.ReadAllBytes(FileNamePath);
                    var user = db.User.Single(s => s.IDus == User.IDus);
                    user.Name = NameBox.Text;
                    user.Surname = SurnameBox.Text;
                    user.Patron = PatronBox.Text;
                    user.Password = PasswordBox.Text;
                    user.Login = LoginBox.Text;
                    user.Role = (RoleBox.SelectedItem as Roles).IDrole;
                    db.SaveChanges();
                }
                var adminMenu = new AdminMenu();
                adminMenu.Show();
                this.Close();
            }
        }

        private void Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var Catalog = new Catalog();
            Catalog.Show();
            this.Close();
        }
    }
}
