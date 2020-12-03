using NBAManagement.Models;
using NBAManagement.Views;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace NBAManagement
{
    static class NavigationManager
    {
        public static void Init(Frame targetFrame)
        {
            TargetFrame = targetFrame;
        }

        public static Frame TargetFrame { get; private set; }
        public static bool IsLogin => CurrentUser != null;
        public static Admin CurrentUser { get; private set; } 
        public static bool CanGoBack => TargetFrame.CanGoBack;
        public static void OpenPage(Page page) 
        {
            TargetFrame.Navigate(page);
        }
        public static Admin Login(string jobnumber, string passsword)
        {
            var entityAdmin = NBAContext.Instance.Admin
                .Where(a => a.Jobnumber == jobnumber && a.Passwords == passsword)
                .FirstOrDefault();

            if(entityAdmin == null)
                throw new LoginInvalidException("Password or jobnumber is invalid.");

            CurrentUser = entityAdmin;

            return entityAdmin;
        }
        public static void GoBack()
        {
            TargetFrame.GoBack();
        }

        public static void Logout()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
