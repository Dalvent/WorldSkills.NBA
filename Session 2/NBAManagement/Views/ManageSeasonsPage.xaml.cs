using NBAManagement.Filter;
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
    /// Логика взаимодействия для ManageSeasonsPage.xaml
    /// </summary>
    public partial class ManageSeasonsPage : Page
    {
        private enum UserChoosedMatchupType
        {
            All,
            Preseason,
            Regular,
            Post
        }
        private UserChoosedMatchupType choosedType;
        private Season choosedSeason;
        public ManageSeasonsPage()
        {
            InitializeComponent();
            seasonComboBox.ItemsSource = NBAContext.Instance.Season.ToArray();
            matchupTypeComboBox.ItemsSource = new string[]
            {
                "All",
                "Preseason",
                "Regular",
                "Post"
            };
            seasonComboBox.SelectedIndex = 0;
            matchupTypeComboBox.SelectedIndex = 0;
        }


        private void UpdateSeasons()
        {
            seasonDataGrid.ItemsSource = choosedSeason.SeasonDetails;
        }

        private void UpdateMatchups()
        {
            var matchups = choosedSeason.Matchup;

            if(!(choosedType == UserChoosedMatchupType.All))
            {
                switch(choosedType)
                {
                case UserChoosedMatchupType.Preseason:
                    matchups = matchups.Where(m => m.MatchupTypeEnum == MatchupTypeEnum.Preseason).ToList();
                    break;
                case UserChoosedMatchupType.Regular:
                    matchups = matchups.Where(m => m.MatchupTypeEnum == MatchupTypeEnum.Regular).ToList();
                    break;
                case UserChoosedMatchupType.Post:
                    matchups = matchups.Where(m => m.MatchupTypeEnum == MatchupTypeEnum.Post).ToList();
                    break;
                }
            }

            mutchupsDataGrid.ItemsSource = matchups;
        }

        private void seasonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            choosedSeason = (Season)seasonComboBox.SelectedItem;
            UpdateSeasons();
            UpdateMatchups();
        }

        private void matchupTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            choosedType = (UserChoosedMatchupType)matchupTypeComboBox.SelectedIndex;
            UpdateMatchups();
        }
    }
}
