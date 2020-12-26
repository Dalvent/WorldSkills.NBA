using NBAManagement.Command;
using NBAManagement.Filter;
using NBAManagement.Models;
using NBAManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NBAManagement.ViewModels
{
    class PlayersMainViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<PlayerInTeam> players;
        private readonly ObservableCollection<Team> teams;
        private readonly ObservableCollection<Season> seasons;
        private PagedEnumerable<PlayerInTeam> filteredPlayers;
        private const int MAX_PAGE_SIZE = 4;
        private SeasonViewModel filterSeason;
        private TeamViewModel filterTeam;
        private string filterName;
        private char? firstCharInNameFilter;
        public PlayersMainViewModel()
        {
            players = new ObservableCollection<PlayerInTeam>(NBAContext.Instance.PlayerInTeam);
            teams = new ObservableCollection<Team>(NBAContext.Instance.Team);
            seasons = new ObservableCollection<Season>(NBAContext.Instance.Season);
            players.CollectionChanged += (sender, e) =>
            {
                UpdateEntities();
            };
            filteredPlayers = new PagedEnumerable<PlayerInTeam>(players, MAX_PAGE_SIZE);
            
            filterSeason = Seasons.FirstOrDefault();
            filterTeam = Teams.FirstOrDefault();
            //UpdateEntities();
            SetFirstCharInNameFilterAll = new ActionCommand(() => FirstCharInNameFilter = null);
            SetFirstCharInNameFilterA = new ActionCommand(() => FirstCharInNameFilter = 'A');
            SetFirstCharInNameFilterB = new ActionCommand(() => FirstCharInNameFilter = 'B');
            SetFirstCharInNameFilterC = new ActionCommand(() => FirstCharInNameFilter = 'C');
            SetFirstCharInNameFilterD = new ActionCommand(() => FirstCharInNameFilter = 'D');
            SetFirstCharInNameFilterE = new ActionCommand(() => FirstCharInNameFilter = 'E');
            SetFirstCharInNameFilterF = new ActionCommand(() => FirstCharInNameFilter = 'F');
            SetFirstCharInNameFilterG = new ActionCommand(() => FirstCharInNameFilter = 'G');
            SetFirstCharInNameFilterH = new ActionCommand(() => FirstCharInNameFilter = 'H');
            SetFirstCharInNameFilterI = new ActionCommand(() => FirstCharInNameFilter = 'I');
            SetFirstCharInNameFilterJ = new ActionCommand(() => FirstCharInNameFilter = 'J');
            SetFirstCharInNameFilterK = new ActionCommand(() => FirstCharInNameFilter = 'K');
            SetFirstCharInNameFilterL = new ActionCommand(() => FirstCharInNameFilter = 'L');
            SetFirstCharInNameFilterM = new ActionCommand(() => FirstCharInNameFilter = 'M');
            SetFirstCharInNameFilterN = new ActionCommand(() => FirstCharInNameFilter = 'N');
            SetFirstCharInNameFilterP = new ActionCommand(() => FirstCharInNameFilter = 'P');
            SetFirstCharInNameFilterQ = new ActionCommand(() => FirstCharInNameFilter = 'Q');
            SetFirstCharInNameFilterR = new ActionCommand(() => FirstCharInNameFilter = 'R');
            SetFirstCharInNameFilterS = new ActionCommand(() => FirstCharInNameFilter = 'S');
            SetFirstCharInNameFilterT = new ActionCommand(() => FirstCharInNameFilter = 'T');
            SetFirstCharInNameFilterU = new ActionCommand(() => FirstCharInNameFilter = 'U');
            SetFirstCharInNameFilterV = new ActionCommand(() => FirstCharInNameFilter = 'V');
            SetFirstCharInNameFilterW = new ActionCommand(() => FirstCharInNameFilter = 'W');
            SetFirstCharInNameFilterX = new ActionCommand(() => FirstCharInNameFilter = 'X');
            SetFirstCharInNameFilterY = new ActionCommand(() => FirstCharInNameFilter = 'Y');
            SetFirstCharInNameFilterZ = new ActionCommand(() => FirstCharInNameFilter = 'Z');
            GoFirstPageCommand = new ActionCommand(() => CurrentPageNum = 0);
            GoPriviusPageCommand = new ActionCommand(() => CurrentPageNum--);
            GoNextPageCommand = new ActionCommand(() => CurrentPageNum++);
            GoLastPageCommand = new ActionCommand(() => CurrentPageNum = PageCount);
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public ICommand SetFirstCharInNameFilterAll { get; }
        public ICommand SetFirstCharInNameFilterA { get; }
        public ICommand SetFirstCharInNameFilterB { get; }
        public ICommand SetFirstCharInNameFilterC { get; }
        public ICommand SetFirstCharInNameFilterD { get; }
        public ICommand SetFirstCharInNameFilterE { get; }
        public ICommand SetFirstCharInNameFilterF { get; }
        public ICommand SetFirstCharInNameFilterG { get; }
        public ICommand SetFirstCharInNameFilterH { get; }
        public ICommand SetFirstCharInNameFilterI { get; }
        public ICommand SetFirstCharInNameFilterJ { get; }
        public ICommand SetFirstCharInNameFilterK { get; }
        public ICommand SetFirstCharInNameFilterL { get; }
        public ICommand SetFirstCharInNameFilterM { get; }
        public ICommand SetFirstCharInNameFilterN { get; }
        public ICommand SetFirstCharInNameFilterP { get; }
        public ICommand SetFirstCharInNameFilterQ { get; }
        public ICommand SetFirstCharInNameFilterR { get; }
        public ICommand SetFirstCharInNameFilterS { get; }
        public ICommand SetFirstCharInNameFilterT { get; }
        public ICommand SetFirstCharInNameFilterU { get; }
        public ICommand SetFirstCharInNameFilterV { get; }
        public ICommand SetFirstCharInNameFilterW { get; }
        public ICommand SetFirstCharInNameFilterX { get; }
        public ICommand SetFirstCharInNameFilterY { get; }
        public ICommand SetFirstCharInNameFilterZ { get; }
        public ICommand GoFirstPageCommand { get; }
        public ICommand GoPriviusPageCommand { get; }
        public ICommand GoNextPageCommand { get; }
        public ICommand GoLastPageCommand { get; }
        public ICommand OpenPlayerDetailCommand { get; }

        public int CurrentPageNum
        {
            get => filteredPlayers.CurrentPageNum;
            set
            {
                filteredPlayers.CurrentPageNum = value;
                ChangePage();
            }
        }
        public int PageCount => filteredPlayers.PageCount;
        public int PlayersCount => filteredPlayers.ItemCount;
        public int PlayersOnPageCount => filteredPlayers.CurrentPageItemCount;
        public string FilterName
        {
            get => filterName;
            set
            {
                filterName = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(FilterName)));
                UpdateEntities();
            }
        }
        public char? FirstCharInNameFilter
        {
            get => firstCharInNameFilter;
            set
            {
                firstCharInNameFilter = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(FirstCharInNameFilter)));
                UpdateEntities();
            }
        }
        public SeasonViewModel FilterSeason
        {
            get => filterSeason;
            set
            {
                filterSeason = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(FilterSeason)));
                UpdateEntities();
            }
        }
        public TeamViewModel FilterTeam
        {
            get => filterTeam;
            set
            {
                filterTeam = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(FilterTeam)));
                UpdateEntities();
            }
        }
        public IEnumerable<TeamViewModel> Teams => teams.ToList().Select(t => new TeamViewModel(t));
        public IEnumerable<SeasonViewModel> Seasons => seasons.ToList().Select(s => new SeasonViewModel(s));
        public IEnumerable<PlayersMainPlayerItemViewModel> PlayersOnPage
        {
            get
            {
                return filteredPlayers.Filtered
                    .Select(pit => new PlayersMainPlayerItemViewModel(pit));
            }
        }
        private void UpdateEntities()
        {
            filteredPlayers = new PagedEnumerable<PlayerInTeam>(players, MAX_PAGE_SIZE)
            {
                Original = players
                .Where(pit => IsPassFilter(pit))
                .ToList()
            };
            ChangePage();
        }
        private void ChangePage()
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPageNum)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PageCount)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PlayersCount)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PlayersOnPageCount)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PlayersOnPage)));
        }
        private bool IsPassFilter(PlayerInTeam playerInTeam)
        {
            return IsPassSeasonFilter(playerInTeam)
                && IsPassTeamFilter(playerInTeam)
                && IsPassNameFilter(playerInTeam)
                && IsPassFirstCharInNameFilter(playerInTeam);
        }

        private bool IsPassSeasonFilter(PlayerInTeam playerInTeam)
        {
            if(filterSeason == null)
                return true;

            return playerInTeam.Season == filterSeason.Model;
        }
        private bool IsPassTeamFilter(PlayerInTeam playerInTeam)
        {
            if(filterTeam == null)
                return true;

            return playerInTeam.Team == filterTeam.Model;
        }
        private bool IsPassNameFilter(PlayerInTeam playerInTeam)
        {
            if(filterName == null)
                return true;

            return (playerInTeam.Player.FirstName).Contains(filterName) 
                || (playerInTeam.Player.LastName).Contains(filterName);
        }
        private bool IsPassFirstCharInNameFilter(PlayerInTeam playerInTeam)
        {
            if(!firstCharInNameFilter.HasValue)
                return true;

            return playerInTeam.Player.FirstName.FirstOrDefault() == firstCharInNameFilter
                    || playerInTeam.Player.LastName.FirstOrDefault() == firstCharInNameFilter;
        }
        public class PlayersMainPlayerItemViewModel
        { 
            public PlayerInTeamViewModel PlayerInTeamViewModel { get; }
            public PlayersMainPlayerItemViewModel(PlayerInTeam playerInTeam)
            {
                PlayerInTeamViewModel = new PlayerInTeamViewModel(playerInTeam);
                OpenPlayerDetailCommand = new ActionCommand(() => NavigationManager.OpenPage(new PlayerDetailView(PlayerInTeamViewModel.Model)));
            }
            public ICommand OpenPlayerDetailCommand { get; }
            public string ShirtNumber => PlayerInTeamViewModel.ShirtNumber;
            public string FullName => PlayerInTeamViewModel.FullName;
            public string TeamName => PlayerInTeamViewModel.TeamName;
            public string Position => PlayerInTeamViewModel.PositionName.Abbr;
            public decimal Weight => PlayerInTeamViewModel.Weight;
            public decimal Height => PlayerInTeamViewModel.Height;
            public int Experience => PlayerInTeamViewModel.YearExperience;
            public string Country => PlayerInTeamViewModel.County;
        }
    }
}
