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
    /// Логика взаимодействия для AddNewMatchupForRegularSeasonPage.xaml
    /// </summary>
    public partial class AddNewMatchupForRegularSeasonPage : Page
    {
        public Matchup MatchupContext
        {
            get => (Matchup)DataContext;
            set => DataContext = value;
        }
        public AddNewMatchupForRegularSeasonPage()
        {
            InitializeComponent();
            MatchupContext = new Matchup();
            MatchupContext.Season = NBAContext.Instance.Season.ToList().Last();
            MatchupContext.MatchupTypeId = 1;
            MatchupContext.Status = -1;
            MatchupContext.Team_Away_Score = 0;
            MatchupContext.Team_Home_Score = 0;
            var teams = NBAContext.Instance.Team.ToList();
            teamAwayComboBox.ItemsSource = teams;
            teamHomeComboBox.ItemsSource = teams;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NBAContext.Instance.Matchup.Add(MatchupContext);
                NBAContext.Instance.SaveChanges();
                NavigationManager.GoBack();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить матч", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void teamAwayComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(teamHomeComboBox.SelectedItem == null)
                return;

            if(IsSelectedTeamsCoinsides())
            {
                teamAwayComboBox.SelectedIndex = -1;
                MessageBox.Show("Комнады не могут совпадать", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void teamHomeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(teamHomeComboBox.SelectedItem == null)
                return;

            if(IsSelectedTeamsCoinsides())
            {
                MessageBox.Show("Комнады не могут совпадать", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                teamHomeComboBox.SelectedIndex = -1;
                return;
            }
            var selectedMatchup = (Team)teamHomeComboBox.SelectedItem;
            MatchupContext.Location = selectedMatchup.Stadium;
            locationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
        }

        private bool IsSelectedTeamsCoinsides()
        {
            return teamAwayComboBox.SelectedItem == teamHomeComboBox.SelectedItem;
        }
    }
}
