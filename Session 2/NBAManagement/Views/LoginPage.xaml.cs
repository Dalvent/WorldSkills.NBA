using Microsoft.Win32;
using NBAManagement.Models;
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

namespace NBAManagement.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private string JobNumer
        {
            get => jobnumberBox.Text;
            set
            {
                jobnumberBox.Text = value;
            }
        }

        private string Password
        {
            get => passwordBox.Password;
            set
            {
                passwordBox.Password = value;
            }
        }

        // Login: NBA001
        // Password: abc123&
        private Admin loginedAdmin;
        public LoginPage()
        {
            InitializeComponent();
            var lastRememberedAdmin = GetLastRememberedAdmin();
            JobNumer = lastRememberedAdmin.Jobnumber;
            Password = lastRememberedAdmin.Passwords;
            InitializeRegistry();
        }

        private void technicalAdminButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.OpenPage(new TechnicalAdministratorMenu(loginedAdmin));
        }

        private void evnetAdminButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.OpenPage(new EventAdministratorMenu(loginedAdmin));
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.GoBack();
        }


        /// <summary>
        /// Берет логин и пароль последнего сохраненного админа или создает новый объект админа.
        /// </summary>
        /// <returns></returns>
        private Admin GetLastRememberedAdmin()
        {
            var admin = new Admin();
            var nbaSubKey = Registry.CurrentUser.OpenSubKey("NBAManagmentLoginData");
            if(nbaSubKey != null)
            {
                admin.Jobnumber = (string)nbaSubKey.GetValue("JOBNUMBER");
                admin.Passwords = (string)nbaSubKey.GetValue("PASSWORD");
            }
            return admin;
        }

        private void RememberUserWritedAdmin()
        {
            var loginDataKey = Registry.CurrentUser.OpenSubKey("NBAManagmentLoginData", true);
            loginDataKey.SetValue("JOBNUMBER", JobNumer);
            loginDataKey.SetValue("PASSWORD", Password);
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loginedAdmin = NavigationManager.Login(JobNumer, Password);
            }
            catch(LoginInvalidException)
            {
                MessageBox.Show("Такого пользователя нет в системе", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(rememberMeComboBox.IsChecked == true)
                RememberUserWritedAdmin();

            chooseAdminTypePoput.IsOpen = true;
        }

        private void InitializeRegistry()
        {
            Registry.CurrentUser.CreateSubKey("NBAManagmentLoginData", true);
        }

        /// <summary>
        /// Провека, являются ли введенные пользователем данные верными.
        /// </summary>
        /// <param name="entityAdmin">Если админ найден, возращает его сущность из БД.</param>
        /// <returns></returns>
        private bool IsUserLoginValid(out Admin entityAdmin)
        {
            entityAdmin = NBAContext.Instance.Admin
                .Where(a => a.Jobnumber == JobNumer && a.Passwords == Password)
                .FirstOrDefault();
            return entityAdmin != null;
        }

        private void closeChooseAdminTypePoput_Click(object sender, RoutedEventArgs e)
        {
            chooseAdminTypePoput.IsOpen = false;
        }
    }
}
