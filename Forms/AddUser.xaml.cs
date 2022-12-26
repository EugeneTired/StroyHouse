using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private string FileNamePath { get; set; }

        public AddUser()
        {
            InitializeComponent();
            using (var db = new StroyMatEntities())
            {
                RoleBox.ItemsSource = db.Roles.ToList();
            }
        }
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
                    var user = new User();
                    user.Name = NameBox.Text;
                    user.Surname = SurnameBox.Text;
                    user.Patron = PatronBox.Text;
                    user.Password = PasswordBox.Text;
                    user.Login = LoginBox.Text;
                    user.Role = (RoleBox.SelectedItem as Roles).IDrole;

                    db.User.Add(user);
                    db.SaveChanges();
                }
                Catalog catalog = new Catalog();
                catalog.Show();
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
