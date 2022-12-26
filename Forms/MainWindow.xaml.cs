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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ВПФки.Forms;

namespace ВПФки
{
    static class SomeData
    {
        public static int guestOrNot { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private int _countAuth;


        private bool CheckCaptcha { get; set; }



        private async void CaptchaGenerate()
        {
            CaptchaText.Visibility = Visibility.Visible;
            CaptchaTextBox.Visibility = Visibility.Visible;
            //CaptchaRefresh.Visibility = Visibility.Visible;

            CaptchaTextBox.Text = "";


            const string captchaText =
                "1234567890" +
                "abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRTSUVWXYZ" +
                "!?&%$#@";

 
            var random = new Random();


            var text = "";


            for (var i = 0; i < 6; i++)
            {
                text += captchaText[random.Next(0, captchaText.Length)].ToString();
            }

            CaptchaText.Text = text;

            CheckCaptcha = true;


            await Task.Delay(120 * 1000);

            if (CaptchaText.Text == text)
            {

                CheckCaptcha = false;
            }
        }

        // Кнопка авторизации
        private void AuthBtn(object sender, RoutedEventArgs e)
        {
            if (_countAuth >= 3)
            {
         
                if (CheckCaptcha)
                {

                    if (CaptchaTextBox.Text != CaptchaText.Text)
                    {
                        MessageBox.Show("Капча введена не верно.");
                        CaptchaGenerate();
                        //CaptchaRefresh.Visibility = Visibility.Visible;
                        Sleep();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Время проверки капчи вышло");
                    CaptchaGenerate();
                    Sleep();
                    return;
                }
            }


            if (string.IsNullOrWhiteSpace(LoginBox.Text) | string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Поля не могут быть пустыми");
                if (++_countAuth >= 3)
                {

                    CaptchaGenerate();
                }
                Sleep();
                return;
            }

            var log = LoginBox.Text;
            var pass = PasswordBox.Password;
            using(var db = new StroyMatEntities())
            {
                var user = db.User.FirstOrDefault(s => s.Login == log && s.Password == pass);
                if(user != null)
                {
                    AuthUser.AuthPerson = user;
                    
                    Window wnd = null;
                    switch (user.Role)
                    {
                        case 1: SomeData.guestOrNot = 1; wnd = new AdminMenu(); break;
                        case 2: SomeData.guestOrNot = 2; wnd = new Catalog(); break;
                        case 3: SomeData.guestOrNot = 3; wnd = new Catalog(); break;
                    }
                    wnd.Show();
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("Неправильный логин или пароль");
       
            if (++_countAuth >= 3)
            {

                CaptchaGenerate();
            }
            Sleep();
            return;
        }
        private async void Sleep()
        {
            EnterButton.IsEnabled = false;
            await Task.Delay(1500);
            EnterButton.IsEnabled = true;
        }

        //Кнопка гостя
        private void GuestClick(object sender, RoutedEventArgs e)
        {   
            SomeData.guestOrNot = 4;
            var cat = new Catalog();
            cat.Show();
            this.Close();
            //throw new Exception("Дальше не идy!");
        }
        
        private void CaptchaRefresh_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        { 
            CaptchaGenerate();
        }
        private void Button_ClickMin(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(418);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var reg = new Registration();
            reg.Show();
            this.Close();
        }
    }
}
