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
    /// Логика взаимодействия для ManageTeamsPage.xaml
    /// </summary>
    public partial class ManageTeamsPage : Page
    {
        public FilteredEnumerable<Team> FilteredTeams { get; set; }
        const string CONFERENCE = "CONFERENCE";
        const string DIVISION = "DIVISION";
        const string NAME = "NAME";
        public ManageTeamsPage()
        {
            InitializeComponent();
            FilteredTeams = new FilteredEnumerable<Team>(NBAContext.Instance.Team.ToList());
            conferenceComboBox.ItemsSource = WithAllStringFirst(NBAContext.Instance.Conference.ToList());
            divisionComboBox.ItemsSource = WithAllStringFirst(NBAContext.Instance.Division.ToList());
            conferenceComboBox.SelectedIndex = 0;
            divisionComboBox.SelectedIndex = 0;
            DataContext = FilteredTeams;
        }

        private IEnumerable<object> WithAllStringFirst(IEnumerable<object> collection)
        {
            var result = new List<object>();
            result.Add("All");
            result.AddRange(collection);
            return result;
        }

        private void UpdateBindings()
        {
            var dataContext = DataContext;
            DataContext = null;
            DataContext = dataContext;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            FilteredTeams.SetFilter(NAME, new StringContainsFilter<Team>(nameTextBox.Text, t => t.TeamName));
            UpdateBindings();
        }

        private void addNewTeamButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add a new team – Future Add-on", "The  feature would be a future add-on to the current system.", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void conferenceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(conferenceComboBox.SelectedItem != null &&
                conferenceComboBox.SelectedItem as string != "All")
            {
                FilteredTeams.SetFilter(CONFERENCE, new TeamConferenceFilter((Conference)conferenceComboBox.SelectedItem));
            }
            else
            {
                FilteredTeams.SetFilter(CONFERENCE, new NullObjectFilter<Team>());
            }
            UpdateBindings();
        }

        private void divisionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(divisionComboBox.SelectedItem != null &&
                divisionComboBox.SelectedItem as string != "All")
            {
                FilteredTeams.SetFilter(DIVISION, new TeamDivisionFilter((Division)divisionComboBox.SelectedItem));
            }
            else
            {
                FilteredTeams.SetFilter(DIVISION, new NullObjectFilter<Team>());
            }
            UpdateBindings();
        }
    }
}
