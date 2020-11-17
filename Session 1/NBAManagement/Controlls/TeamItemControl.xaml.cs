using NBAManagement.Pages;
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

namespace NBAManagement.Controlls
{
    /// <summary>
    /// Interaction logic for TeamItemControl.xaml
    /// </summary>
    public partial class TeamItemControl : UserControl
    {
        public TeamItemControl()
        {
            InitializeComponent();
        }

        private void OpenRoasterInfo(object sender, RoutedEventArgs e)
        {
            NavigateToDetailPage(TeamDetailPage.Tab.Roster);
        }

        private void OpenMatchupInfo(object sender, RoutedEventArgs e)
        {
            NavigateToDetailPage(TeamDetailPage.Tab.Matchup);
        }

        private void OpenFirstLineUp(object sender, RoutedEventArgs e)
        {
            NavigateToDetailPage(TeamDetailPage.Tab.FirstLineUp);
        }

        private void NavigateToDetailPage(TeamDetailPage.Tab startTab)
        {
            var page = new TeamDetailPage(startTab);
            page.DataContext = DataContext;
            FrameManager.OpenPage(page);
        }
    }
}
