using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ВПФки
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            using(var db = new StroyMatEntities())
            {
                LV.ItemsSource = db.Product.ToList();
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
