using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models
{
    public class TeamReport
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public Conference Conference
        {
            get => Division.Conference;
            set => Division.Conference = value;
        }
        public Division Division { get; set; }
        public double Points { get; set; }
        public double Rebounds { get; set; }
        public double Assists { get; set; }
        public double Steals { get; set; }
        public double Blocks { get; set; }
        public double Turnovers { get; set; }
        public double Fouls { get; set; }

        public static TeamReport CreateAvarange(Team team, MatchupTypeEnum matchupType)
        {
            var playerStatistics = team.PlayerStatistics
                .Where(ps => ps.Matchup.MatchupTypeEnum == matchupType);

            return new TeamReport()
            {
                Name = team.TeamName,
                Division = team.Division,
                Points = GetAvarangeFieldValue(playerStatistics, ps => ps.Point),
                Rebounds = GetAvarangeFieldValue(playerStatistics, ps => ps.Rebound),
                Assists = GetAvarangeFieldValue(playerStatistics, ps => ps.Assist),
                Steals = GetAvarangeFieldValue(playerStatistics, ps => ps.Steal),
                Blocks = GetAvarangeFieldValue(playerStatistics, ps => ps.Block),
                Turnovers = GetAvarangeFieldValue(playerStatistics, ps => ps.Turnover),
                Fouls = GetAvarangeFieldValue(playerStatistics, ps => ps.Foul)
            };
        }
        public static TeamReport CreateTotal(Team team, MatchupTypeEnum matchupType)
        {
            var playerStatistics = team.PlayerStatistics
                .Where(ps => ps.Matchup.MatchupTypeEnum == matchupType);

            return new TeamReport()
            {
                Name = team.TeamName,
                Division = team.Division,
                Points = GetTotalFieldValue(playerStatistics, ps => ps.Point),
                Rebounds = GetTotalFieldValue(playerStatistics, ps => ps.Rebound),
                Assists = GetTotalFieldValue(playerStatistics, ps => ps.Assist),
                Steals = GetTotalFieldValue(playerStatistics, ps => ps.Steal),
                Blocks = GetTotalFieldValue(playerStatistics, ps => ps.Block),
                Turnovers = GetTotalFieldValue(playerStatistics, ps => ps.Turnover),
                Fouls = GetTotalFieldValue(playerStatistics, ps => ps.Foul)
            };
        }
        public static IEnumerable<TeamReport> CreateAvarange(IEnumerable<Team> teams, MatchupTypeEnum matchupType, Func<TeamReport, double> getRankingField)
        {
            var teamReaports = teams.Select(t => CreateAvarange(t, matchupType)).ToList();
            teamReaports.OrderBy(tr => getRankingField(tr));
            SetRanks(teamReaports, getRankingField);
            return teamReaports;
        }
        public static IEnumerable<TeamReport> CreateTotal(IEnumerable<Team> teams, MatchupTypeEnum matchupType, Func<TeamReport, double> getRankingField)
        {
            var teamReaports = teams.Select(t => CreateTotal(t, matchupType)).ToList();
            teamReaports.OrderBy(tr => getRankingField(tr));
            SetRanks(teamReaports, getRankingField);
            return teamReaports;
        }
        private static double GetAvarangeFieldValue(IEnumerable<PlayerStatistics> statistics, Func<PlayerStatistics, double> getField)
        {
            double avarengeValue = 0;
            int statisticsCount = 0;
            foreach(var playerStatistics in statistics)
            {
                avarengeValue += getField(playerStatistics);
                statisticsCount++;
            }
            return avarengeValue / statisticsCount;
        }
        private static double GetTotalFieldValue(IEnumerable<PlayerStatistics> statistics, Func<PlayerStatistics, double> getField)
        {
            double totalValue = 0;
            foreach(var playerStatistics in statistics)
            {
                totalValue += getField(playerStatistics);
            }
            return totalValue;
        }

        private static void SetRanks(IEnumerable<TeamReport> teamReports, Func<TeamReport, double> getRankingField)
        {
            int currentRunk = 1;
            var lastSetTeamReport = teamReports.First();
            foreach(var teamReport in teamReports)
            {
                if(getRankingField(teamReport) != getRankingField(lastSetTeamReport))
                {
                    currentRunk++;
                }
                teamReport.Rank = currentRunk;
                lastSetTeamReport = teamReport;
            }
        }
    }
}
