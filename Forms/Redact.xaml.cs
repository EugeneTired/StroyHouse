using Microsoft.Win32;
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
using ВПФки.Extra;

namespace ВПФки.Forms
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Redact : Window
    {
        public Redact(Product prod)
        {
            InitializeComponent();
            Unit.Text = prod.Units.Unit;
            Weight.Text = prod.Weight.ToString();
            Name.Text = prod.Name;
            Price.Text = prod.Price.ToString();
            Category.Text = prod.Category.Name;
            Description.Text = prod.Descr;
            Suppliers.Text = prod.Suppliers.Name.ToString();
            ImageBox.Source = prod.Image.ToImageSource();
            tempImage = prod.Image;

            using (var db = new StroyMatEntities())
            {
                Category.ItemsSource = db.Category.ToList();
                Suppliers.ItemsSource = db.Suppliers.ToList();
                Unit.ItemsSource = db.Units.ToList();
                Category.SelectedItem = (Category.ItemsSource as List<Category>).Single(s => s.IDcateg == prod.IDcateg);
                Suppliers.SelectedItem = (Suppliers.ItemsSource as List<Suppliers>).Single(s => s.IDsup == prod.IDsup);
                Unit.SelectedItem = (Unit.ItemsSource as List<Units>).Single(s => s.IDunit == prod.IDunit);
            }

            Product = prod;
        }

        private string FileNamePath { get; set; }

        private byte[] tempImage { get; set; }

        private Product Product { get; set; }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" || Weight.Text == "" || Description.Text == "" || Unit.SelectedItem.ToString() == "" || Category.SelectedItem.ToString() == "" || Suppliers.SelectedItem.ToString() == "" || Price.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                using (var db = new StroyMatEntities())
                {
                    var prod = db.Product.Single(s => s.IDprod == Product.IDprod);
                    prod.Name = Name.Text;
                    prod.Weight = decimal.Parse(Weight.Text);
                    prod.Descr = Description.Text;
                    prod.IDunit = (Unit.SelectedItem as Units).IDunit;
                    prod.IDcateg = (Category.SelectedItem as Category).IDcateg;
                    prod.IDsup = (Suppliers.SelectedItem as Suppliers).IDsup;
                    prod.Price = /*)*/decimal.Parse(Price.Text);/*)*/
                    if (ImageBox.Source == null) prod.Image = null;
                    else if (!string.IsNullOrWhiteSpace(FileNamePath)) prod.Image = File.ReadAllBytes(FileNamePath);
                    db.SaveChanges();
                }
                Catalog catalog = new Catalog();
                catalog.Show();
                this.Close();
            }
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var Catalog = new Catalog();
            Catalog.Show();
            this.Close();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ImageBox.Source = null;
        }
        private void Button_click(object sender, RoutedEventArgs e)
        {
            var openFile = new OpenFileDialog()
            {
                Filter = "Image (png, jpg, jpeg, bmp) | *.png; *.jpg; *.jpeg; *.bmp"
            };
            if (openFile.ShowDialog() == false) return;
            var image = openFile.FileName;
            if (image == null) return;

            ImageBox.Source = File.ReadAllBytes(image).ToImageSource();
            FileNamePath = image;
            ImageBox.Visibility = Visibility.Visible;
            BtnClear.IsEnabled = true;
        }
    }
}
