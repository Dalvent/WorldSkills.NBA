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

namespace NBAManagement.Views
{
    /// <summary>
    /// Interaction logic for TeamDetailPage.xaml
    /// </summary>
    public partial class TeamDetailPage : Page
    {
        private readonly Team currentTeam;
        public enum Tab 
        {
            Roster, 
            Matchup,
            FirstLineUp
        }
        public TeamDetailPage(Team team, Tab startTab)
        {
            InitializeComponent();

            mainTabControl.SelectedIndex = (int)startTab;
            currentTeam = team;
            DataContext = currentTeam;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            seasonComboBox.ItemsSource = NBAContext.Instance.Season.ToList();
            seasonComboBox.SelectedIndex = 0;
            FilterBySelectedSeason();
        }

        private void setSeason_Click(object sender, RoutedEventArgs e)
        {
            FilterBySelectedSeason();
        }

        private void FilterBySelectedSeason()
        {
            var selectedSeason = (Season)seasonComboBox.SelectedItem;

            var seasonMutchups = new SeasonParticipantFilter<Matchup>(selectedSeason)
                .Use(currentTeam.Matchup);
            matchupDataGrid.ItemsSource = seasonMutchups;

            var seasonPlayersInTeams = new SeasonParticipantFilter<PlayerInTeam>(selectedSeason)
                .Use(currentTeam.PlayerInTeam);

            roasterDataGrid.ItemsSource = seasonPlayersInTeams;
            lineupGrid.DataContext = seasonPlayersInTeams;

            cList.PlayersInTeam = seasonPlayersInTeams;
            pfList.PlayersInTeam = seasonPlayersInTeams;
            sgList.PlayersInTeam = seasonPlayersInTeams;
            sfList.PlayersInTeam = seasonPlayersInTeams;
            pgList.PlayersInTeam = seasonPlayersInTeams;
        }
    }
}
