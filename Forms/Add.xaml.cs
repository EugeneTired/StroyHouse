using Microsoft.Win32;
using ВПФки.Extra;
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

namespace ВПФки.Forms
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
            using (var db = new StroyMatEntities())
            {
                Category.ItemsSource = db.Category.ToList();
                Suppliers.ItemsSource = db.Suppliers.ToList();
                Unit.ItemsSource = db.Units.ToList();
            }
        }
        private string FileNamePath { get; set; }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Name.Text == "" || Weight.Text == "" || Description.Text =="" || Unit.SelectedItem.ToString() =="" || Category.SelectedItem.ToString()=="" || Suppliers.SelectedItem.ToString() =="" || Price.Text =="")
            {
                MessageBox.Show("Заполните все поля!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    using (var db = new StroyMatEntities())
                    {
                        byte[] image = null;
                        if (!string.IsNullOrWhiteSpace(FileNamePath))
                            image = File.ReadAllBytes(FileNamePath);
                        var prod = new Product();
                        prod.Name = Name.Text;
                        prod.Weight = decimal.Parse(Weight.Text);
                        prod.Descr = Description.Text;
                        prod.IDunit = (Unit.SelectedItem as Units).IDunit;
                        prod.IDcateg = (Category.SelectedItem as Category).IDcateg;
                        prod.IDsup = (Suppliers.SelectedItem as Suppliers).IDsup;
                        prod.Price = /*)*/decimal.Parse(Price.Text);/*)*/
                        prod.Image = image;
                        db.Product.Add(prod);
                        db.SaveChanges();
                    }
                    Catalog catalog = new Catalog();
                    catalog.Show();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка при вводе данных!","Внимание",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var Catalog = new Catalog();
            Catalog.Show();
            this.Close();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ImageBox.Source = null;
            FileNamePath = string.Empty;
        }
    }
}
