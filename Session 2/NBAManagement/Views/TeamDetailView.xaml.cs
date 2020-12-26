using NBAManagement.Models;
using NBAManagement.Filter;
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
using NBAManagement.ViewModels;

namespace NBAManagement.Views
{
    /// <summary>
    /// Interaction logic for TeamDetailPage.xaml
    /// </summary>
    public partial class TeamDetailView : Page
    {
        public enum Tab 
        {
            Roster, 
            Matchup,
            FirstLineUp
        }
        public TeamDetailView(Team team, Tab startTab)
        {
            InitializeComponent();

            MainTabControl.SelectedIndex = (int)startTab;
            DataContext = new TeamDetailViewModel(team);
        }
    }
}
