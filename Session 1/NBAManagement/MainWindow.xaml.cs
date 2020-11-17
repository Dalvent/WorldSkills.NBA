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
using NBAManagement.Data;
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
