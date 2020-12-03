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
    /// Логика взаимодействия для TechnicalAdministratorMenu.xaml
    /// </summary>
    public partial class TechnicalAdministratorMenu : Page
    {
        public TechnicalAdministratorMenu(Admin admin)
        {
            InitializeComponent();
        }

        private void executionsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Manage Executions – Future Add-on", "The feature would be a future add-on to the current system.",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void teamReportButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.OpenPage(new TeamReaportPage());
        }
    }
}
