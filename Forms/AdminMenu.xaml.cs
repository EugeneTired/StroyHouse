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
using System.Windows.Shapes;

namespace ВПФки.Forms
{
    /// <summary>
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void Cat_Click(object sender, RoutedEventArgs e)
        {
            var cat = new Catalog();
            cat.Show();
            this.Close();
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            var users = new Users();
            users.Show();
            this.Close();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            var ord = new Orders();
            ord.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
