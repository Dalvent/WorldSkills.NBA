using NBAManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NBAManagement.ViewModels
{
    public class TeamDetailViewModel : INotifyPropertyChanged
    {
        private readonly Team model;
        public TeamDetailViewModel(Team model)
        {
            this.model = model;
            this.filterSeason = Seasons.FirstOrDefault();
        }

        public byte[] Logo => model.Logo;
        public string Name => model.TeamName;
        public string DivisionName => model.Division.Name;

        public IEnumerable<SeasonViewModel> Seasons => NBAContext.Instance.Season.ToList().Select(s => new SeasonViewModel(s));
        private SeasonViewModel filterSeason;
        public SeasonViewModel FilterSeason
        {
            get => filterSeason;
            set
            {
                filterSeason = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(FilterSeason)));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(TeamMatchups)));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(SmallForwardPlayers)));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CenterPlayers)));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PowerForwardPlayers)));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PointGuardPlayers)));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ShootingGuardPlayers)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerable<PlayerInTeamViewModel> Players
        {
            get
            {
                return model.PlayerInTeam
                    .Where(pit => pit.Season == filterSeason.Model)
                    .Select(pit => new PlayerInTeamViewModel(pit))
                    .ToList();
            }
        }

        public IEnumerable<TeamDetailMatchupViewModel> TeamMatchups
        {
            get
            {
                var matchups = new List<Matchup>();
                matchups.AddRange(model.MatchupHome
                    .ToList());
                matchups.AddRange(model.MatchupHome
                    .ToList());

                return matchups
                    .Where(m => m.Season == filterSeason.Model)
                    .Select(m => new TeamDetailMatchupViewModel(m, model));
            }
        }

        public IEnumerable<PlayerInTeamViewModel> SmallForwardPlayers => PlayersOfPositionAbrr("SF");
        public IEnumerable<PlayerInTeamViewModel> CenterPlayers => PlayersOfPositionAbrr("С");
        public IEnumerable<PlayerInTeamViewModel> PowerForwardPlayers => PlayersOfPositionAbrr("PF");
        public IEnumerable<PlayerInTeamViewModel> PointGuardPlayers => PlayersOfPositionAbrr("PG");
        public IEnumerable<PlayerInTeamViewModel> ShootingGuardPlayers => PlayersOfPositionAbrr("SG");
        private IEnumerable<PlayerInTeamViewModel> PlayersOfPositionAbrr(string abrr)
        {
            var test = Players.Where(p => p.PositionName.Abbr.Trim() == abrr);
            return test;
        }

        public class TeamDetailMatchupViewModel
        {
            private readonly Team choosenTeam;
            public Matchup Model { get; }

            public TeamDetailMatchupViewModel(Matchup model, Team choosenTeam)
            {
                this.Model = model;
                this.choosenTeam = choosenTeam;
            }

            public DateTime StartDateTime => Model.Starttime;
            public string MatchupTypeName => Model.MatchupType.Name;
            public Team Opponent
            {
                get
                {
                    return Model.TeamAway == choosenTeam ? Model.TeamHome : Model.TeamAway;
                }
            }
            public string Location
            {
                get
                {
                    if(!String.IsNullOrWhiteSpace(Model.Location))
                        return Model.Location;

                    return Model.TeamHome.Stadium;
                }
            }
            public string Status
            {
                get
                {
                    switch(Model.Status)
                    {
                    case -1: return "Not start";
                    case 0: return "Running";
                    case 1: return "Finished";
                    default: throw new NotImplementedException();
                    }
                }
            }
            public string Result => $"{Model.Team_Away_Score}-{Model.Team_Home_Score}";
        }
    }
}
