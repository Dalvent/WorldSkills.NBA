namespace NBAManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PlayerStatistics : IComparable
    {
        public int Id { get; set; }

        public int MatchupId { get; set; }

        public int TeamId { get; set; }

        public int PlayerId { get; set; }

        public int IsStarting { get; set; }

        public decimal Min { get; set; }

        public int Point { get; set; }

        public int Assist { get; set; }

        public int FieldGoalMade { get; set; }

        public int FieldGoalMissed { get; set; }

        [Column("3-PointFieldGoalMade")]
        public int C3_PointFieldGoalMade { get; set; }

        [Column("3-PointFieldGoalMissed")]
        public int C3_PointFieldGoalMissed { get; set; }

        public int FreeThrowMade { get; set; }

        public int FreeThrowMissed { get; set; }

        public int Rebound { get; set; }

        public int OFFR { get; set; }

        public int DFFR { get; set; }

        public int Steal { get; set; }

        public int Block { get; set; }

        public int Turnover { get; set; }

        public int Foul { get; set; }

        public virtual PlayerInTeam ActualPlayerInTeam => Player.GetTeamBySeason(Matchup.Season);

        public virtual Matchup Matchup { get; set; }

        public virtual Player Player { get; set; }

        public virtual Team Team { get; set; }

        public int CompareTo(object obj)
        {
            return Matchup.Starttime.CompareTo(
                ((PlayerStatistics)obj).Matchup.Starttime
            );
        }
    }
}
