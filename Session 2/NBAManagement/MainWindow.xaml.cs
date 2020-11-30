using System;
using System.IO;
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
using NBAManagement.Models;
using System.Reflection;
using System.Drawing;

namespace NBAManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string CurrentPageTitle
        {
            get
            {
                return mainFrame.Name;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigating += OnNavigating;
            FrameManager.TargetFrame = mainFrame;
            nbaFooterText.Text = GenerateNbaFooterText();

            var i = NBAContext.Instance.PlayerStatistics.ToArray();
        }

        public string GenerateNbaFooterText()
        {
            int nbaAge = DateTime.Now.Year - 1946;
            return $"The current season is {Season.LastSeason.Name}, and NBA already has a history of {nbaAge} years.";
        }

        public void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            switch(e.NavigationMode)
            {
            case NavigationMode.New:
                Header.Visibility = Visibility.Visible;
                break;
            case NavigationMode.Back:
                if(!FrameManager.CanGoBack)
                    Header.Visibility = Visibility.Hidden;
                break;
            default:
                break;
            }
        }
       
        private void Back(object sender, RoutedEventArgs e)
        {
            FrameManager.GoBack();
        }
    }
}
