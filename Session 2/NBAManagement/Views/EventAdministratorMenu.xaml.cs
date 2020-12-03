using NBAManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для EventAdministratorMenu.xaml
    /// </summary>
    public partial class EventAdministratorMenu : Page
    {
        public EventAdministratorMenu(Admin admin)
        {
            InitializeComponent();
        }

        private void seasonsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.OpenPage(new ManageSeasonsPage());
        }

        private void machupsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.OpenPage(new ManageMachupsPage());
        }

        private void teamsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.OpenPage(new ManageTeamsPage());
        }

        private void playersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.OpenPage(new ManagePlayerPage());
        }
    }
}
