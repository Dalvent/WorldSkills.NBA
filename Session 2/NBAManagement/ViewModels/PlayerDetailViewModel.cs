using NBAManagement.Command;
using NBAManagement.Models;
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
    enum ChartXPlayerStatistic
    {
        Point,
        Rebound,
        Assists,
        Steals,
        Blocks
    }
    class PlayerDetailViewModel : INotifyPropertyChanged
    {
        public PlayerInTeamViewModel Model { get; }
        private readonly ObservableCollection<PlayerStatistics> playerStatistics;
        private IEnumerable<PlayerStatistics> playerStatisticsInDateRange;
        public DateTime? biginingIntervalDate;
        public DateTime? endIntervalDate;
        public PlayerDetailViewModel(PlayerInTeam playerInTeamViewModel)
        {
            Model = new PlayerInTeamViewModel(playerInTeamViewModel);
            CurrentChartXPlayerStatistic = ChartXPlayerStatistic.Point;
            SetPointStatistic = new ActionCommand(() => CurrentChartXPlayerStatistic = ChartXPlayerStatistic.Point);
            SetReboundStatistic = new ActionCommand(() => CurrentChartXPlayerStatistic = ChartXPlayerStatistic.Rebound);
            SetAssistsStatistic = new ActionCommand(() => CurrentChartXPlayerStatistic = ChartXPlayerStatistic.Assists);
            SetStealsStatistic  = new ActionCommand(() => CurrentChartXPlayerStatistic = ChartXPlayerStatistic.Steals);
            SetBlocksStatistic = new ActionCommand(() => CurrentChartXPlayerStatistic = ChartXPlayerStatistic.Blocks);
            Search = new ActionCommand(() => 
            { 
                UpdatePlayerStatisticsInDateRange();
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ChartPoints)));
            });
            
            playerStatistics = new ObservableCollection<PlayerStatistics>(Model.Model.Player.PlayerStatistics.ToList());

            BiginingIntervalDate = DateTime.Now.AddYears(-10);
            EndIntervalDate = DateTime.Now;
            UpdatePlayerStatisticsInDateRange();
        }

        public ICommand SetPointStatistic { get; }
        public ICommand SetReboundStatistic { get; }
        public ICommand SetAssistsStatistic { get; }
        public ICommand SetStealsStatistic { get; }
        public ICommand SetBlocksStatistic { get; }
        public ICommand Search { get; }
        public int YearExperience => Model.YearExperience;
        public double CurrentSeasonPPG => CurrentSeasonAverageOfField(s => s.Point);
        public double CurrentSeasonAPG => CurrentSeasonAverageOfField(s => s.Assist);
        public double CurrentSeasonRPG => CurrentSeasonAverageOfField(s => s.Rebound);
        public double CareerPPG => CareerAverageOfField(s => s.Point);
        public double CareerAPG => CareerAverageOfField(s => s.Assist);
        public double CareerRPG => CareerAverageOfField(s => s.Rebound);
        public DateTime? BiginingIntervalDate
        {
            get => biginingIntervalDate;
            set
            {
                biginingIntervalDate = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(BiginingIntervalDate)));
            }
        }
        public DateTime? EndIntervalDate
        {
            get => endIntervalDate;
            set
            {
                endIntervalDate = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(EndIntervalDate)));
            }
        }
        public string ShowChartXPlayerStatistic => currentChartXPlayerStatistic.ToString();
        public double AvarangeOfChartStatistic
        {
            get
            {
                double sum = 0.0;
                var charPointsX = ChartPoints.Select(cp => cp.X).ToList();
                foreach(int charPointX in charPointsX)
                {
                    sum += charPointX;
                }
                return sum / charPointsX.Count;
            }
        }
        private ChartXPlayerStatistic currentChartXPlayerStatistic;
        private ChartXPlayerStatistic CurrentChartXPlayerStatistic
        {
            get => currentChartXPlayerStatistic;
            set
            {
                currentChartXPlayerStatistic = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ChartPoints)));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ShowChartXPlayerStatistic)));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AvarangeOfChartStatistic)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private double CareerAverageOfField(Func<PlayerStatistics, int> getField)
        {
            double result = 0.0;
            var statistics = playerStatistics.ToList();
            foreach(var statistic in statistics)
            {
                result += getField(statistic);
            }
            return result / statistics.Count;
        }
        private double CurrentSeasonAverageOfField(Func<PlayerStatistics, int> getField)
        {
            double result = 0.0;
            var statistics = playerStatistics
                .Where(ps => ps.Matchup.Season == Season.LastSeason)
                .ToList();
            foreach(var statistic in statistics)
            {
                result += getField(statistic);
            }
            return result / statistics.Count;
        }

        private void UpdatePlayerStatisticsInDateRange()
        {
            if(!(BiginingIntervalDate.HasValue && EndIntervalDate.HasValue))
            {
                playerStatisticsInDateRange = new List<PlayerStatistics>();
            }
            else
            {
                playerStatisticsInDateRange = new ObservableCollection<PlayerStatistics>(playerStatistics
                    .Where(ps => ps.Matchup.Starttime >= BiginingIntervalDate && ps.Matchup.Starttime <= EndIntervalDate)
                    .ToList());
            }

            CurrentChartXPlayerStatistic = ChartXPlayerStatistic.Point;
        }
        private int GetChartXPlayerStatisticField(PlayerStatistics playerStatistics)
        {
            switch(currentChartXPlayerStatistic)
            {
            case ChartXPlayerStatistic.Point: return playerStatistics.Point;
            case ChartXPlayerStatistic.Rebound: return playerStatistics.Rebound;
            case ChartXPlayerStatistic.Assists: return playerStatistics.Assist;
            case ChartXPlayerStatistic.Steals: return playerStatistics.Steal;
            case ChartXPlayerStatistic.Blocks: return playerStatistics.Block;
            default: throw new NotImplementedException();
            }
        }
        public IEnumerable<PlayerStatisticCharPoint> ChartPoints
        {
            get
            {
                return playerStatisticsInDateRange
                    .Select(ps => new PlayerStatisticCharPoint(
                        GetChartXPlayerStatisticField(ps),
                        ps.Matchup.Starttime)
                    );
            }
        }
    }
}
