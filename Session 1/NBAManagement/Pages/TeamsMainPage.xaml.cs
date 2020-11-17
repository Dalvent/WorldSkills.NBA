using NBAManagement.Data;
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

namespace NBAManagement
{
    /// <summary>
    /// Interaction logic for TeamsMainPage.xaml
    /// </summary>
    public partial class TeamsMainPage : Page
    {
        public TeamsMainPage()
        {
            InitializeComponent();
            FillTeams();
        }

        public void FillTeams()
        {
            var teams = NBAContext.Instance.Team.ToList();

            AtlanicListView.ItemsSource = teams
                .Where(item => item.Division.Name == "Atlantic");
            CentralListView.ItemsSource = teams
                .Where(item => item.Division.Name == "Centra");
            NorthwestListView.ItemsSource = teams
                .Where(item => item.Division.Name == "Northwestern");
            PacificListView.ItemsSource = teams
                .Where(item => item.Division.Name == "Pacific");
            SoutheastListView.ItemsSource = teams
                .Where(item => item.Division.Name == "Southeastern");
            SouthwestListView.ItemsSource = teams
                .Where(item => item.Division.Name == "Southwestern");

        }
    }
}
