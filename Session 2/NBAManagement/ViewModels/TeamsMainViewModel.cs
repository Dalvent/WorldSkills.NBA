using NBAManagement.Command;
using NBAManagement.Models;
using NBAManagement.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.ViewModels
{
    class TeamsMainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Team> teams;
        private TeamsMainViewModel()
        {
        }

        public static TeamsMainViewModel Create()
        {
            var viewModel = new TeamsMainViewModel();
            try
            {
                var teams = NBAContext.Instance.Team;
                viewModel.teams = new ObservableCollection<Team>(teams);
                viewModel.PropertyChanged += viewModel.OnTeamsChanged;
            }
            catch
            {
                throw;
            }
            return viewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<TeamsMainItemViewModel> AtlanicTeams => GetTeamsViewModelsOfDivision("Atlantic");
        public IEnumerable<TeamsMainItemViewModel> CentralTeams => GetTeamsViewModelsOfDivision("Centra");
        public IEnumerable<TeamsMainItemViewModel> NorthwestTeams => GetTeamsViewModelsOfDivision("Northwestern");
        public IEnumerable<TeamsMainItemViewModel> PacificTeams => GetTeamsViewModelsOfDivision("Pacific");
        public IEnumerable<TeamsMainItemViewModel> SoutheastTeams => GetTeamsViewModelsOfDivision("Southeastern");
        public IEnumerable<TeamsMainItemViewModel> SouthwestTeams => GetTeamsViewModelsOfDivision("Southwestern");
    

        private IEnumerable<TeamsMainItemViewModel> GetTeamsViewModelsOfDivision(string division)
        {
            return teams.Where(t => t.Division.Name == division).Select(t => new TeamsMainItemViewModel(t));
        }
        private void OnTeamsChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AtlanicTeams)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CentralTeams)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(NorthwestTeams)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PacificTeams)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(SoutheastTeams)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(SouthwestTeams)));
        }
        public class TeamsMainItemViewModel
        {
            private readonly Team model;
            public TeamsMainItemViewModel(Team model)
            {
                this.model = model;
                OpenRoasterInfo = new ActionCommand(OnOpenRoasterInfo);
                OpenMatchupInfo = new ActionCommand(OnOpenMatchupInfo);
                OpenFirstLineUp = new ActionCommand(OnOpenFirstLineUp);
            }
            public byte[] Logo => model.Logo;
            public string Name => model.TeamName;
            public ActionCommand OpenRoasterInfo { get; }
            public ActionCommand OpenMatchupInfo { get; }
            public ActionCommand OpenFirstLineUp { get; }

            private void OnOpenRoasterInfo()
            {
                NavigateToDetailPage(TeamDetailView.Tab.Roster);
            }

            private void OnOpenMatchupInfo()
            {
                NavigateToDetailPage(TeamDetailView.Tab.Matchup);
            }

            private void OnOpenFirstLineUp()
            {
                NavigateToDetailPage(TeamDetailView.Tab.FirstLineUp);
            }

            private void NavigateToDetailPage(TeamDetailView.Tab startTab)
            {
                var page = new TeamDetailView(model, startTab);
                NavigationManager.OpenPage(page);
            }
        }
    }
}
