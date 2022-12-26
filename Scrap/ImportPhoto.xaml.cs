using System;
using System.Collections.Generic;
using System.IO;
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

namespace ВПФки
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }
        public void ImportImages(object sender, RoutedEventArgs e)
        {
            using (var db = new StroyMatEntities())
            {
                var ImageImport = Directory.GetFiles(@"D:\Все\Всё\Курсовая 4 курс\ВПФки\ВПФки\images");
                foreach (var product in db.Product)
                {
                    var img = ImageImport.FirstOrDefault(imgs => imgs.ToLower().Contains(product.Name.ToLower().Trim()));
                    if(img != null)
                        product.Image = File.ReadAllBytes(img);
                }
                db.SaveChanges();
            }
        }
    }
}
